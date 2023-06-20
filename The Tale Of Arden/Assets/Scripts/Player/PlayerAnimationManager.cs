using UnityEngine;

namespace Arden.Player
{
    public class PlayerAnimationManager
    {
        private Animator playerAnimatior;

        private bool isGrounded;
        private bool isMoving;
        private bool isFloating;

        private bool isPushing;
        private bool isPulling;
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
        public void PlaySecondAttackAnimation()
        {
            playerAnimatior.SetTrigger("Attack Second");
        }

        public void PlayDeathAnimation()
        {
            playerAnimatior.SetTrigger("Die");
        }
        public void PlayTrigger(string _trigger)
        {
            playerAnimatior.SetTrigger(_trigger);
        }
        public void PlayBoolAnimations()
        {
            playerAnimatior.SetBool("IsMoving",isMoving);
            playerAnimatior.SetBool("IsGrounded",isGrounded);
            playerAnimatior.SetBool("IsFloating",isFloating);
            
            playerAnimatior.SetBool("IsPushing",isPushing);
            playerAnimatior.SetBool("IsPulling",isPulling);
            
       
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
        public bool IsPushing
        {
            set
            {
                isPushing = value;
            }
        }
        public bool IsPulling
        {
            set
            {
                isPulling = value;
            }
        }
    }
}
