using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arden.Player
{
    public class PlayerAttackManager : MonoBehaviour
    {
        private PlayerAnimationManager _playerAnimationManager;
        private PlayerStateManager playerStateManager;
        private PlayerSoundManager playerSoundManager;
        private PlayerController playerController;
        
        
        [Header("Hit Point")]
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float range;

        [Header("Knockout Range")]
        [SerializeField] private Vector2 swingKnockout;
        [SerializeField] private Vector2 hitKnockout;
        
        [Header("First AttackTime")]
        [SerializeField] private float hitTime;
        [SerializeField] private float knockoutTime;
        [SerializeField] private float recoverTime;
        [SerializeField] private float secondCanHitTime;
        
        [Header("Second AttackTime")]
        [SerializeField] private float secondAttackHitTime;
        [SerializeField] private float secondAttackKnockoutTime;
        [SerializeField] private float secondAttackRecoverTime;
        private bool canSecondAttack;
        private bool doSecondAttack;
        
        [Space(100)] 
        [SerializeField] private LayerMask enemyLayer;
        
        void Start()
        {
            _playerAnimationManager = PlayerParent.PlayerAnimationManagerManager;
            
            playerSoundManager = PlayerParent.PlayerSoundManager;
            playerStateManager = PlayerParent.PlayerStateManager;
            playerController = PlayerParent.PlayerController;

        }

        public void Attack()
        {
            StartCoroutine(StartAttack(hitTime,knockoutTime,recoverTime));
            playerStateManager.ChangeState(playerStateManager.AttackState);
            _playerAnimationManager.PlayAttackAnimation();
            StartCoroutine(OpenSecondAttack());
        }

       

        IEnumerator OpenSecondAttack()
        {
            canSecondAttack = false;
            yield return new WaitForSeconds(secondCanHitTime);
            canSecondAttack = true;
        }
        
        IEnumerator StartAttack(float _hitTime,float _knockoutTime,float _recoverTime)
        {
            yield return new WaitForSeconds(_hitTime);
            playerController.AddKnockout(new Vector2(swingKnockout.x*transform.localScale.x,swingKnockout.y));
            
            Collider2D[] _enemiesList = Physics2D.OverlapCircleAll(attackPoint.position, range,enemyLayer);
            if (_enemiesList != null)
            {
                HitEnemy(_enemiesList);
            }

            yield return new WaitForSeconds(_knockoutTime);
            
            
            playerController.ResetRigidbodyVelocity();

            
            
            if (doSecondAttack)
            {
                StartCoroutine(StartAttack(secondAttackHitTime,secondAttackKnockoutTime,secondAttackRecoverTime));
                _playerAnimationManager.PlaySecondAttackAnimation();
                doSecondAttack = false;
                canSecondAttack = false;
                yield break;
            }
            
            yield return new WaitForSeconds(_recoverTime);
            playerStateManager.ChangeState(playerStateManager.IdleState);

        }

        public void HitEnemy(Collider2D[] _enemyList)
        {
            playerController.AddKnockout(hitKnockout*-transform.localScale.x);
            
            foreach (var _enemy in _enemyList)
            {
                Debug.Log($"{_enemy}");
            }
            
        }
        public bool CanDoSecondAttack()
        {
            return canSecondAttack;
        }

        public void SetSecondHitFalse()
        {
            canSecondAttack = false;
            doSecondAttack = false;
        }

        public void AttackSecond() => doSecondAttack = true;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(attackPoint.position,range);
        }
    }
}
