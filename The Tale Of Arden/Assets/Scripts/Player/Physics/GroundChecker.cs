using UnityEngine;

namespace Arden.Player.Physics
{
    public class GroundChecker : MonoBehaviour
    {
        bool isGrounded;
        bool isInGround;
        public LayerMask groundLayer;
        public LayerMask inGroundLayer;
        public bool IsGrounded => isGrounded;
        public bool IsInGround => isInGround;

        private void OnTriggerStay2D(Collider2D collision)
        {
            isGrounded = collision != null && (((1 << collision.gameObject.layer) & groundLayer) != 0);
            isInGround = collision != null && (((1 << collision.gameObject.layer) & inGroundLayer) != 0);

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            isGrounded = false;
            isInGround = false;
        }
    }
}
