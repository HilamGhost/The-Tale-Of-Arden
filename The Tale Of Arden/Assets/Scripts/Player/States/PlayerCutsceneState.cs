using UnityEngine;
using UnityEngine.InputSystem;

namespace Arden.Player.State
{
    public class PlayerCutsceneState : PlayerState
    {
        PlayerController playerController;
        private PlayerStateManager playerStateManager;
        private PlayerAttackManager playerAttackManager;

        
        
        public PlayerCutsceneState(PlayerStateManager _playerStateManager) 
        {
            playerStateManager = _playerStateManager;
            
            playerController = PlayerParent.PlayerController;
            playerAttackManager = PlayerParent.PlayerAttackManager;

        }
        
    }
}
