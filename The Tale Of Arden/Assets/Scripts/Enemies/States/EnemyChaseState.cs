using UnityEngine;

namespace Arden.Enemy.State
{
    public class EnemyChaseState : IEnemyState
    {
        private EnemyController enemyController;
        
        public EnemyChaseState(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }
        public void OnEnemyStateStart()
        {
            
        }

        public void OnEnemyStateUpdate()
        {
            if (!enemyController.EnemyDedector.CheckPlayerIsNear())
            {
                enemyController.ChangeState(enemyController.enemyIdleState);
            }

            enemyController.EnemyMover.DoChase(enemyController.EnemyDedector.PlayerTransform,
                enemyController.attackRange);
        }

        public void OnEnemyStateFixed()
        {
            
        }

        public void OnEnemyStateExit()
        {
           
        }

        public void OnEnemyStateTriggerEnter(Collider2D col)
        {
            
        }
    }
}
