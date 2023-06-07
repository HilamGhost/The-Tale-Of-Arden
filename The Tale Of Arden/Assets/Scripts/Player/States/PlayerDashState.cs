using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden.Player.State
{
    public class PlayerDashState : PlayerState
    {
        PlayerStateManager playerState;
        PlayerController playerContoller;
        
        public PlayerDashState(PlayerStateManager _stateManager)
        {
            playerState = _stateManager;
            
        }
    }
}
