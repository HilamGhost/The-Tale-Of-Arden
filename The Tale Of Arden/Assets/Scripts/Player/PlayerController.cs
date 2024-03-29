using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Arden.Player.Physics;

namespace Arden.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerAnimationManager _playerAnimationManager;
        PlayerStateManager playerStateManager;
        private PlayerSoundManager playerSoundManager;
        private PlayerHoldManager playerHoldManager;
        
        
        GroundChecker groundChecker;
        Rigidbody2D playerRB;
        
        public enum GroundState { IsGrounded, IsJumping, InAir, PrepareJump };

        [Header("Situations")]
        public GroundState groundState;


        [Header("Player Controller")]
        [SerializeField] bool isMoving;
        [SerializeField] float playerSpeed;
        [SerializeField] float jumpSpeed;

        [SerializeField] bool isJumpCutted;
        
        bool isGrounded;
        private bool isInGround;
        private float moveDirection;
        [Header("Dash Stats")]
        [SerializeField] bool canDash;
        [SerializeField] float dashSpeed = 10;
        [SerializeField] float dashTime = 0.1f;
        [SerializeField] float dashCooldown = 1;
        float _dashCooldownReset;

        [Header("Gravity Values")]
        [SerializeField] private HoldProperties holdProperties;
        private HoldableObject holdableObject;
        
        [Header("Forgive Mechanics")]
        [SerializeField] float groundedRemember = 0.35f;
        float groundedRemember_reset;

        [SerializeField] float jumpPressedRemember = 0.35f;
        float jump_buffer_time_reset;

        [SerializeField, Range(0, 1)] float cutJumpHeight;
        [SerializeField, Range(0, 25)] float minJumpRange;
        

        [Header("Gravity Values")]
        [SerializeField, Range(0, 10)] private float gravityScale;
        [SerializeField, Range(0, 20)] private float gravityFallScale;
        [SerializeField, Range(0, 30)] private float maxFallSpeed;
        
        


        #region Properties
        public bool IsMoving => isMoving;
        public bool IsGrounded => isGrounded;

        public float MoveDirection => moveDirection;

        public Vector2 DashDirection
        {
            get
            {
                Vector2 _dashDirection = (transform.localScale.x > 0) ? new Vector2(1, 0) : new Vector2(-1, 0);
                return _dashDirection;
            }
        }
        public bool InAir => groundState != GroundState.IsGrounded;
        #endregion

        void Start()
        {
            playerRB = GetComponent<Rigidbody2D>();
            groundChecker = GetComponentInChildren<GroundChecker>();

            _playerAnimationManager = PlayerParent.PlayerAnimationManager;
            
            playerSoundManager = PlayerParent.PlayerSoundManager;
            playerStateManager = PlayerParent.PlayerStateManager;

            playerHoldManager = new PlayerHoldManager(this,holdProperties);

            groundedRemember_reset = groundedRemember;
            jump_buffer_time_reset = jumpPressedRemember;
            _dashCooldownReset = dashCooldown;
            jumpPressedRemember = 0;
        }
        private void Update()
        {

            ChangeJumpState();
            SetTimers();
            
           

            isGrounded = groundChecker.IsGrounded;
            isInGround = groundChecker.IsInGround;
            
            if (isGrounded)
            {
                groundedRemember = groundedRemember_reset;
            }
            
            _playerAnimationManager.PlayBoolAnimations();
            
            _playerAnimationManager.IsGrounded = isGrounded;
            _playerAnimationManager.IsFloating = groundState == GroundState.InAir;
        }

        void FixedUpdate()
        {
            
            if (groundedRemember > 0 && jumpPressedRemember > 0) Jump();
            if (isJumpCutted) CutJump();

        }

        #region Jump
        void Jump()
        {
            playerRB.AddForce(new Vector2(playerRB.velocity.x, jumpSpeed),ForceMode2D.Impulse);
            groundedRemember = 0;
            jumpPressedRemember = 0;
            
            _playerAnimationManager.PlayJumpAnimation();
            
            groundState = GroundState.IsJumping;
        }
      
        void CutJump()
        {
            if (playerRB.velocity.y > minJumpRange)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * cutJumpHeight);
            }
        }
        
        public void GetJumpInput(InputAction.CallbackContext _context)
        {
            if (_context.started && groundState == GroundState.IsGrounded)
            {
                groundState = GroundState.PrepareJump;

            }
            else
            if (_context.started && groundState != GroundState.IsGrounded)
            {
                jumpPressedRemember = jump_buffer_time_reset;
                isJumpCutted = false;
            }
            else
            if (_context.canceled)
            {
                isJumpCutted = true;
            }
        }

        void ChangeJumpState()
        {
            switch (groundState)
            {
                case GroundState.PrepareJump:
                    
                    jumpPressedRemember = jump_buffer_time_reset;
                    isJumpCutted = false;
                    
                    
                    groundState = GroundState.IsJumping;

                    break;
                case GroundState.IsJumping:

                    if (isJumpCutted) 
                        groundState = GroundState.InAir;

                    if (Mathf.Approximately(groundedRemember, groundedRemember_reset))
                    {
                        groundState = GroundState.IsGrounded;
                    
                    }


                    break;

                case GroundState.IsGrounded:
                    
                    
                    if (isGrounded)
                    {
                        groundedRemember = groundedRemember_reset;
                    }
                       
                    else
                    {
                        if (groundedRemember <= 0) groundState = GroundState.InAir;
                    }
                    break;

                case GroundState.InAir:

                    if (Mathf.Approximately(groundedRemember, groundedRemember_reset))
                    {
                        groundState = GroundState.IsGrounded;
                    }
                    
                    break;
            }
        }
        #endregion
        #region Movement

        public void MovePlayer(float _input)
        {
            SetMoveDirection(_input);
            
            float _moveValue = _input * playerSpeed; 
            Vector2 _movementVector = Vector2.right * _moveValue + Vector2.up * playerRB.velocity.y;
            playerRB.velocity = _movementVector;

        }

        public void SetMoveDirection(float _input)
        {
            moveDirection = _input;
            isMoving = (!Mathf.Approximately(moveDirection ,0) && !Mathf.Approximately(playerRB.velocity.x,0));
            
            
            var oldScaleAbs = new Vector3(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y),
                Mathf.Abs(transform.localScale.z));
            
            if (moveDirection > 0) transform.localScale = oldScaleAbs;
            if (moveDirection < 0) transform.localScale = new Vector3(-oldScaleAbs.x, oldScaleAbs.y, oldScaleAbs.z);

            _playerAnimationManager.IsMoving = isMoving;
        }
        public void ResetRigidbodyVelocity()
        {
            playerRB.velocity = new Vector2(0, 0);
            isMoving = false;
            moveDirection = 0;
            _playerAnimationManager.IsMoving = false;
        }
        #endregion
        
        //It will be more detailed and more User Friendly
        #region Gravity

        void SetGravityScale(float _gravityScale)
        {
            playerRB.gravityScale = _gravityScale;
        }

        public void SetGravityScaleToZero()
        {
            playerRB.gravityScale = 0;
        } 
        public void SetGravityScaleToNormal()
        {
            SetGravityScale(gravityFallScale);
        } 
        public void SetGrativyScale()
        {
            // If Player is falling
            if (playerRB.velocity.y < 0)
            {
                SetGravityScale(gravityFallScale);
                playerRB.velocity = new Vector2(playerRB.velocity.x, Mathf.Max(playerRB.velocity.y, -maxFallSpeed));
            }
            else
            {
                SetGravityScale(gravityScale);
            }
        }
        

        #endregion

        #region Dash Methods

        public void CheckCanDash() 
        {
            
            if (!canDash)
            {
                dashCooldown += Time.deltaTime;
                if (dashCooldown >= _dashCooldownReset)
                {
                    canDash = true;
                    dashCooldown = _dashCooldownReset;
                }
            }
        }
        public void StartDash() 
        {
            
            if (canDash) 
            {
                StartCoroutine(Dash());
                dashCooldown = 0;
                canDash = false;
            }       
            
        }
        IEnumerator Dash() 
        {
            
            playerStateManager.ChangeState(playerStateManager.DashState);
            
            Dash(DashDirection, dashSpeed);
            _playerAnimationManager.PlayDashAnimation();
            yield return new WaitForSeconds(dashTime);

            playerStateManager.ChangeState(playerStateManager.IdleState);
        }
        public void Dash(Vector2 _direction,float _dashSpeed) 
        {
            playerRB.velocity = Vector2.zero;

            Vector2 _moveDirection = _dashSpeed * _direction;
            playerRB.velocity = _moveDirection;
        }

        #endregion

        #region Hold Methods

        public void CheckHoldableObject()
        {
            AddHoldableObject(playerHoldManager.FindHoldableObject(transform.position+holdProperties.holdOffset, Vector3.right*transform.localScale.x));
        }

        public void ToggleHoldMode(bool _holdMode)
        {
            if (_holdMode)
            {
                playerHoldManager.ConnectHoldObject(holdableObject);
                return;
            }
            
            playerHoldManager.RemoveHoldObject();
        }
        public void AddHoldableObject(HoldableObject _holdableObject) => holdableObject = _holdableObject;

        public void HoldObject(float _input)
        {
            SetHoldDirection(_input);
            playerHoldManager.HoldObject(_input);
        }

        public void SetHoldDirection(float _input)
        {
            _playerAnimationManager.IsPushing = _input*transform.localScale.x > 0;
            _playerAnimationManager.IsPulling = _input*transform.localScale.x < 0;
            
        }

        public void ResetHoldAnimations()
        {
            _playerAnimationManager.IsPushing = false;
            _playerAnimationManager.IsPulling = false;
            
            _playerAnimationManager.PlayTrigger("Cancel Hold");
        }
        public bool CheckHoldObjectAvaliable() => holdableObject;

        #endregion

        public void AddKnockout(Vector2 _knockoutValue) => playerRB.AddForce(_knockoutValue, ForceMode2D.Impulse);
        public void MakePlayerMatter(bool _isMatter) =>  GetComponent<Collider2D>().isTrigger = !_isMatter;
        
        public void MakePlayerDynamic(bool _isDynamic) => playerRB.isKinematic = !_isDynamic;

        #region Animation/Cutscene Methods

        public void ChangeStateToIdle() => playerStateManager.ChangeState(playerStateManager.IdleState);

        public void ChangeStateToCutscene()
        {
            playerStateManager.ChangeState(playerStateManager.CutsceneState);
            Debug.Log("A");
        } 
        

        #endregion


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position+holdProperties.holdOffset,transform.position+holdProperties.holdOffset+Vector3.right*holdProperties.holdDedectionRange*transform.localScale.x);
            
        }

        void SetTimers()
        {
            if (jumpPressedRemember > 0) jumpPressedRemember -= Time.deltaTime;
            else jumpPressedRemember = 0;

            if (groundedRemember > 0) groundedRemember -= Time.deltaTime;
            else groundedRemember = 0;
        }

        
        
    }
}

