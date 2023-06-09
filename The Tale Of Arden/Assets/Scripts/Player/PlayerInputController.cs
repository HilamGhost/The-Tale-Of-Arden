using UnityEngine;
using UnityEngine.InputSystem;

namespace Arden.Player
{
    [CreateAssetMenu(fileName = "New Player Input Controller", menuName = "Player/Create New Player Input Controller")]
    public class PlayerInputController : SingletonScriptableObject<PlayerInputController>
    {
        PlayerStateManager playerStateManager;

        public void AssignInputManager(PlayerStateManager _currentPlayerStateManager)
        {
            playerStateManager = _currentPlayerStateManager;
        }

        public void OnMove(InputAction.CallbackContext _context)
        {
            playerStateManager.CurrentState.OnMove(_context);
        }

        public void OnHold(InputAction.CallbackContext _context)
        {
            playerStateManager.CurrentState.OnHold(_context);
        }

        public void OnJump(InputAction.CallbackContext _context)
        {
            playerStateManager.CurrentState.OnJump(_context);
        }

        public void OnDash(InputAction.CallbackContext _context)
        {
            playerStateManager.CurrentState.OnDash(_context);
        }

        public void OnAttack(InputAction.CallbackContext _context)
        {
            playerStateManager.CurrentState.OnAttack(_context);
        }
    }
}
