using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Arden.Enemy
{
    public class EnemyAttackManager
    {
        private EnemyController enemyController;

         float attackRange;
         float attackWaitTime;
         float parryStartTime;
         private float parryEndTime;
         float recoverTime;


         private Transform attackPoint;
         private LayerMask playerLayer;
         
         private ParticleSystem parryVFX;
         
         private bool isParryable;
         private bool isParried;
         internal bool isAttacking;
         
        public EnemyAttackManager(EnemyController _enemyController,AttackProperties _attackProperties,float _attackRange)
        {
            enemyController = _enemyController;

            attackRange =_attackRange;
            attackWaitTime = _attackProperties.attackWaitTime;
            parryStartTime = _attackProperties.attackParryStartTime;
            parryEndTime = _attackProperties.attackParryEndTime;

            attackPoint = _attackProperties.attackPoint;
            playerLayer = _attackProperties.playerLayer;
            
            recoverTime = _attackProperties.attackRecoverTime;
            parryVFX = _attackProperties.parryVFX;

        }

        public IEnumerator StartAttack()
        {
            if(isAttacking) yield break;
            
            isAttacking = true;
            enemyController.EnemyAnimationManager.PlayAttackAnimation();
            enemyController.EnemyAnimationManager.StopEnemyAnimation();
            yield return new WaitForSeconds(attackWaitTime);

            if (!isParried)
            {
               DedectAttackablePlayer();
            }

            yield return new WaitForSeconds(recoverTime);
            isParried = false;
            isAttacking = false;
            enemyController.ChangeState(enemyController.enemyIdleState);
            
        }

        void DedectAttackablePlayer()
        {
            if(!enemyController.IsStateEqual(enemyController.enemyAttackState)) return;
            
            Collider2D _player = Physics2D.OverlapCircle(attackPoint.position, attackRange,playerLayer);
            
            if (_player != null)
            {
               _player.GetComponent<Player.PlayerStatManager>().TakeDamage();
            }
        }
        public IEnumerator StartParry()
        {
            isParryable = false;
            yield return new WaitForSeconds(parryStartTime);
            isParryable = true;
            yield return new WaitForSeconds(parryEndTime);
            isParryable = false;
        }

        public void GetParried()
        {
            if(!isParryable) return;
            if(isParried) return;
            
            Debug.Log("Parried");
            
            PlayParryVFX();
            isParried = true;
        }

        public void PlayParryVFX()
        {
            if(parryVFX.isPlaying) return;

            parryVFX.transform.localScale = new Vector3(enemyController.EnemyDirection, 1, 1);
            parryVFX.Play();
            enemyController.EnemySoundManager.PlayParrySound();
        }
    }
    [System.Serializable]
    public class AttackProperties
    {
        public float attackWaitTime;
        public float attackParryStartTime;
        public float attackParryEndTime;
        public float attackRecoverTime;

        public Transform attackPoint;
        public LayerMask playerLayer;
        
        public ParticleSystem parryVFX;
    }
}
