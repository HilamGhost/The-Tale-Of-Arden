using System.Collections;
using UnityEngine;

namespace Arden.Player
{
    public class PlayerAttackManager : MonoBehaviour
    {
        private PlayerAnimationManager _playerAnimationManager;
        private PlayerStateManager playerStateManager;
        private PlayerSoundManager playerSoundManager;
        
        
        [Header("Hit Point")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float range;
        [Header("Time")]
        [SerializeField] private float hitTime;
        [Space(100)] 
        [SerializeField] private LayerMask enemyLayer;
        
        void Start()
        {
            _playerAnimationManager = PlayerParent.PlayerAnimationManagerManager;
            
            playerSoundManager = PlayerParent.PlayerSoundManager;
            playerStateManager = PlayerParent.PlayerStateManager;
            
        }

        public void Attack()
        {
            StartCoroutine(StartAttack());
        }
        IEnumerator StartAttack()
        {
            _playerAnimationManager.PlayAttackAnimation();
            yield return new WaitForSeconds(hitTime);
            Collider2D[] _enemiesList = Physics2D.OverlapCircleAll(attackPoint.position, range);

        }
        public bool CanDoSecondAttack()
        {
            return true;
        }
    }
}
