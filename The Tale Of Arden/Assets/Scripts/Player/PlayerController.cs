using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using Arden.Player.Physics;

namespace Arden.Player
{
    public class PlayerController : MonoBehaviour
    {
        PlayerStateManager playerStateManager;
        private PlayerSoundManager playerSoundManager;
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
        
        [Header("Forgive Mechanics")]
        [SerializeField] float groundedRemember = 0.35f;
        float groundedRemember_reset;

        [SerializeField] float jumpPressedRemember = 0.35f;
        float jump_buffer_time_reset;

        [SerializeField, Range(0, 1)] float cutJumpHeight;
        [SerializeField, Range(0, 10)] float minJumpRange;

        [SerializeField, Range(0, 2)] private float playerSmoothTime;

        [Header("Gravity Values")]
        [SerializeField, Range(0, 10)] private float gravityScale;
        [SerializeField, Range(0, 20)] private float gravityFallScale;
        [SerializeField, Range(0, 30)] private float maxFallSpeed;
        
        [Header("Camera")]
        [SerializeField] CinemachineVirtualCamera virtualCamera;
        


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
            playerSoundManager = PlayerParent.PlayerSoundManager;
            playerStateManager = PlayerParent.PlayerStateManager;

            groundedRemember_reset = groundedRemember;
            jump_buffer_time_reset = jumpPressedRemember;
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

        }

        void FixedUpdate()
        {
            
            if (groundedRemember > 0 && jumpPressedRemember > 0) Jump();
            if (isJumpCutted) CutJump();

        }

        #region Jump
        void Jump()
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpSpeed);
            groundedRemember = 0;
            jumpPressedRemember = 0;

            groundState = GroundState.IsJumping;
        }
      
        void CutJump()
        {
            if (playerRB.velocity.y > minJumpRange)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * -cutJumpHeight);
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
            if (_context.canceled && groundState != GroundState.IsGrounded)
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
        }
        public void ResetRigidbodyVelocity()
        {
            playerRB.velocity = new Vector2(0, 0);
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

        #region State Methods
        private void ChangeStateToIdle() => playerStateManager.ChangeState(PlayerParent.PlayerStateManager.IdleState);

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

        
        public void MakePlayerMatter(bool _isMatter) =>  GetComponent<Collider2D>().isTrigger = !_isMatter;
        
        public void MakePlayerDynamic(bool _isDynamic) => playerRB.isKinematic = !_isDynamic;

        public void AddForceToPlayer(Vector2 _force) => playerRB.AddForce(_force,ForceMode2D.Impulse);
        
        
        void SetTimers()
        {
            if (jumpPressedRemember > 0) jumpPressedRemember -= Time.deltaTime;
            else jumpPressedRemember = 0;

            if (groundedRemember > 0) groundedRemember -= Time.deltaTime;
            else groundedRemember = 0;
        }

        
        
    }
}

