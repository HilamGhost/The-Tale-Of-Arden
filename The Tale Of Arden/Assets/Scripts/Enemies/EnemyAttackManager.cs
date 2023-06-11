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
         
         private ParticleSystem parryVFX;
         
         private bool isParryable;
         private bool isParried;
         
        public EnemyAttackManager(EnemyController _enemyController,AttackProperties _attackProperties,float _attackRange)
        {
            enemyController = _enemyController;

            attackRange =_attackRange;
            attackWaitTime = _attackProperties.attackWaitTime;
            parryStartTime = _attackProperties.attackParryStartTime;
            parryEndTime = _attackProperties.attackParryEndTime;
            
            recoverTime = _attackProperties.attackRecoverTime;
            parryVFX = _attackProperties.parryVFX;

        }

        public IEnumerator StartAttack()
        {
            enemyController.EnemyAnimationManager.StopEnemyAnimation();
            yield return new WaitForSeconds(attackWaitTime);
            Debug.Log("Attack Starts");
            
            if (enemyController.EnemyDedector.CanAttackToPlayer(attackRange)&& !isParried)
            {
                enemyController.EnemyDedector.PlayerTransform.GetComponent<Player.PlayerStatManager>().TakeDamage();
            }

            yield return new WaitForSeconds(recoverTime);
            Debug.Log("Attack Ends");
            isParried = false;
            
            enemyController.ChangeState(enemyController.enemyIdleState);
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
        }
    }
    [System.Serializable]
    public class AttackProperties
    {
        public float attackWaitTime;
        public float attackParryStartTime;
        public float attackParryEndTime;
        public float attackRecoverTime;

        public ParticleSystem parryVFX;
    }
}
