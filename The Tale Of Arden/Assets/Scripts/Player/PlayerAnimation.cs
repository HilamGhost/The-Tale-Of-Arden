using UnityEngine;

namespace Arden.Player
{
    public class PlayerAnimation
    {
        private Animator playerAnimatior;

        private bool isGrounded;
        private bool isMoving;
        public PlayerAnimation(Animator _animator)
        {
            playerAnimatior = _animator;
        }

        public void PlayJumpAnimation()
        {
            playerAnimatior.SetTrigger("Jump");
            Debug.Log("Jump");
        }
        public void PlayDashAnimation()
        {
            playerAnimatior.SetTrigger("Dash");   
            Debug.Log("Dash");
        }
        
        public void PlayBoolAnimations()
        {
            playerAnimatior.SetBool("IsMoving",isMoving);
            playerAnimatior.SetBool("IsGrounded",isGrounded);
            
            Debug.Log($"{isGrounded} {isMoving}");
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
