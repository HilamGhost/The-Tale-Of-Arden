using System.Collections;
using System.Collections.Generic;
using Arden.Enemy;
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

        public override void OnStateTriggerEnter(Collider2D collision)
        {
            if (collision.TryGetComponent(out EnemyController enemyController))
            {
                enemyController.EnemyAttackManager.GetParried();
            }
        }
    }
}
