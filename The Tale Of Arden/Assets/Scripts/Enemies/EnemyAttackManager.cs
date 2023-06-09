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
         float parryTime;
         float recoverTime;
         
        public EnemyAttackManager(EnemyController _enemyController,AttackProperties _attackProperties,float _attackRange)
        {
            enemyController = _enemyController;

            attackRange =_attackRange;
            attackWaitTime = _attackProperties.attackWaitTime;
            parryTime = _attackProperties.attackParryTime;
            recoverTime = _attackProperties.attackRecoverTime;
            
        }

        public IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(attackWaitTime);
            Debug.Log("Attack Starts");
            if (enemyController.EnemyDedector.CanAttackToPlayer(attackRange))
            {
                Debug.Log("Hit!");
            }

            yield return new WaitForSeconds(recoverTime);
            Debug.Log("Attack Ends");
            
            enemyController.ChangeState(enemyController.enemyIdleState);
        }
    }
    [System.Serializable]
    public class AttackProperties
    {
        public float attackWaitTime;
        public float attackParryTime; 
        public float attackRecoverTime;
    }
}
