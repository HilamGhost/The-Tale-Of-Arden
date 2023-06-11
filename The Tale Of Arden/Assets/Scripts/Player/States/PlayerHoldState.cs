using UnityEngine;
using UnityEngine.InputSystem;

namespace Arden.Player.State
{
    public class PlayerHoldState : PlayerState
    {
        PlayerController playerController;
        private PlayerStateManager playerStateManager;
        private PlayerAttackManager playerAttackManager;

        
        
        public PlayerHoldState(PlayerStateManager _playerStateManager) 
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
            playerController.ToggleHoldMode(true);
        }
        public override void OnStateUpdate()
        {
            playerController.ResetRigidbodyVelocity();
            playerController.SetGrativyScale();
        }
        public override void OnStateFixedUpdate()
        {
            playerController.HoldObject(playerStateManager.horizontalInput);
        }
        public override void OnStateExit()
        {
            playerController.ResetRigidbodyVelocity();
            playerController.ToggleHoldMode(false);
            playerController.ResetHoldAnimations();
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
            if (_context.canceled)
            {
                playerStateManager.ChangeState(playerStateManager.IdleState);
            }
        }

        #endregion

    }
}
