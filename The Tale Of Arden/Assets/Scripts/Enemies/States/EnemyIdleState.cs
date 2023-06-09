using UnityEngine;

namespace Arden.Enemy.State
{
    public class EnemyIdleState : IEnemyState
    {
        private EnemyController enemyController;
        public EnemyIdleState(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }
        public void OnEnemyStateStart()
        {
            
        }

        public void OnEnemyStateUpdate()
        {
            enemyController.EnemyMover.DoPatrol();
            if (enemyController.EnemyDedector.CheckPlayerIsNear())
            {
                enemyController.ChangeState(enemyController.enemyChaseState);
            }
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
