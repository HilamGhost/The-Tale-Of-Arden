using UnityEngine;
using UnityEngine.InputSystem;

namespace Arden.Player.State
{
    public class PlayerIdleState : PlayerState
    {
        PlayerController playerController;
        private PlayerStateManager playerStateManager;
        private PlayerAttackManager playerAttackManager;

        
        
        public PlayerIdleState(PlayerStateManager _playerStateManager) 
        {
            playerStateManager = _playerStateManager;
            
            playerController = PlayerParent.PlayerController;
            playerAttackManager = PlayerParent.PlayerAttackManager;

            if(playerStateManager == null) Debug.Log("Player State Manager Atanmadı");
            else Debug.Log("Player State Manager Atandı!");
            
        }
        
        #region State Methods
        public override void OnStateStart()
        {
            playerController.ResetRigidbodyVelocity();
        }
        public override void OnStateUpdate()
        {
            playerController.SetGrativyScale();
            playerController.CheckCanDash();
            playerController.CheckHoldableObject();
        }
        public override void OnStateFixedUpdate()
        {
            
            playerController.MovePlayer(playerStateManager.horizontalInput);
           
        }
        public override void OnStateExit()
        {
            playerController.ResetRigidbodyVelocity();
           
        }

        public override void OnStateCollideEnter(Collision2D collision)
        {
           
        }

        public override void OnStateCollideExit(Collision2D collision)
        {
            
        }

        public override void OnStateTriggerEnter(Collider2D collision)
        {
          

        }

        public override void OnStateTriggerExit(Collider2D collision)
        {

        }
        #endregion
        
        #region Input Methods
        public override void OnMove(InputAction.CallbackContext _context)
        {
            playerStateManager.horizontalInput = _context.ReadValue<float>();
        }
        public override void OnHold(InputAction.CallbackContext _context)
        {
            if (_context.started && playerController.CheckHoldObjectAvaliable())
            {
                playerStateManager.ChangeState(playerStateManager.HoldState);
            }

        }
        public override void OnJump(InputAction.CallbackContext _context)
        {
            playerController.GetJumpInput(_context);
        }

        public override void OnDash(InputAction.CallbackContext context)
        {
           if(context.started) playerController.StartDash();
        }

        public override void OnAttack(InputAction.CallbackContext _context)
        {
            if (_context.started && playerController.IsGrounded)
            {
                playerAttackManager.Attack();
            }
        }
        #endregion

    }
}
