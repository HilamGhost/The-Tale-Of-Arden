using UnityEngine;
using UnityEngine.InputSystem;


namespace Arden.Player.State
{
    public class PlayerAttackState : PlayerState
    { 
        PlayerController playerController;
        private PlayerStateManager playerStateManager;
        private PlayerAttackManager playerAttackManager;
        
        
        public PlayerAttackState(PlayerStateManager _playerStateManager) 
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
           
        }
        public override void OnStateFixedUpdate()
        {

        }
        public override void OnStateExit()
        {
            playerAttackManager.SetSecondHitFalse();
        }
        #endregion
        
        #region Input Methods
        public override void OnMove(InputAction.CallbackContext _context)
        {
            playerStateManager.horizontalInput = _context.ReadValue<float>();
        }
        public override void OnHold(InputAction.CallbackContext _context)
        {


        }
        public override void OnJump(InputAction.CallbackContext _context)
        {
            
        }

        public override void OnDash(InputAction.CallbackContext context)
        {
          
        }

        public override void OnAttack(InputAction.CallbackContext _context)
        {
            if (_context.started && playerAttackManager.CanDoSecondAttack())
            {
                playerAttackManager.AttackSecond();
            }
        }
        #endregion
    }
}
