using UnityEngine;
using Arden.Player.State;

namespace Arden.Player
{
    public class PlayerStateManager
    {
       private PlayerIdleState playerIdleState;
       private PlayerDashState playerDashState;
       private PlayerAttackState playerAttackState;
       private PlayerHoldState _holdState;

        PlayerState currentState;

        #region Properties
        public PlayerState CurrentState => currentState;
        public PlayerIdleState IdleState => playerIdleState;
        public PlayerDashState DashState => playerDashState;
        public PlayerAttackState AttackState => playerAttackState;
        public PlayerHoldState HoldState => _holdState;
        #endregion
        
        public float horizontalInput;
        public float verticalInput;
        
        public PlayerStateManager()
        {

            playerIdleState = new PlayerIdleState(this);
            playerDashState = new PlayerDashState(this);
            playerAttackState = new PlayerAttackState(this);
            _holdState = new PlayerHoldState(this);

            currentState = playerIdleState;
            
        }

        public void OnStateUpdate() 
        {
            currentState.OnStateUpdate();
        }
        public void OnStateFixedUpdate()
        {
            currentState.OnStateFixedUpdate();
        }

        public void OnStateCollideEnter(Collision2D collision)
        {
            currentState.OnStateCollideEnter(collision);
        }
        public void OnStateCollideExit(Collision2D collision)
        {
            currentState.OnStateCollideExit(collision);
        }
        public void OnStateTriggerEnter(Collider2D other) 
        {
            currentState.OnStateTriggerEnter(other);
        }
        public void OnStateTriggerExit(Collider2D other)
        {
            currentState.OnStateTriggerExit(other);
        }

        /// <summary>
        /// Changes the player's state
        /// </summary>
        /// <param name="wantedState"> You must use this format: player.PlayerState.IState</param>
        public void ChangeState(PlayerState wantedState)
        {
            if (currentState != wantedState)
            {
                Debug.Log($"Player State changed to {wantedState} ");
                currentState.OnStateExit();
                currentState = wantedState;
                currentState.OnStateStart();

            }
        }

        /// <summary>
        /// Is the wanted state equal player's state
        /// </summary>
        /// <param name="wantedState">You must use this format: player.PlayerState.IState</param>
        public bool IsPlayerStateEqual(PlayerState wantedState)
        {
            if (currentState == wantedState) return true;
            
            return false;
        } 
    }
}
