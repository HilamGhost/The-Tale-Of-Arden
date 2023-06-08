using UnityEngine;

namespace Arden.Player
{
    public class PlayerAnimationManager
    {
        private Animator playerAnimatior;

        private bool isGrounded;
        private bool isMoving;
        private bool isFloating;
        public PlayerAnimationManager(Animator _animator)
        {
            playerAnimatior = _animator;
        }

        public void PlayJumpAnimation()
        {
            playerAnimatior.SetTrigger("Jump");
        }
        public void PlayDashAnimation()
        {
            playerAnimatior.SetTrigger("Dash");
        }
        public void PlayAttackAnimation()
        {
            playerAnimatior.SetTrigger("Attack");
        }
        public void PlayBoolAnimations()
        {
            playerAnimatior.SetBool("IsMoving",isMoving);
            playerAnimatior.SetBool("IsGrounded",isGrounded);
            playerAnimatior.SetBool("IsFloating",isFloating);
        }
        public bool IsFloating
        {
            set
            {
                isFloating = value;
            }
        }
        public bool IsMoving
        {
            set
            {
                isMoving = value;
            }
        }
        public bool IsGrounded
        {
            set
            {
                isGrounded = value;
            }
        }
    }
}
