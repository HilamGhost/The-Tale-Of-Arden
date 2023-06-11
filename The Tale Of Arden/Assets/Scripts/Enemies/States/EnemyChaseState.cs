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
            enemyController.ToggleAlert();
        }

        public void OnEnemyStateUpdate()
        {
            if (!enemyController.EnemyDedector.CheckPlayerIsNear())
            {
                enemyController.ChangeState(enemyController.enemyIdleState);
                return;
            }

            enemyController.EnemyMover.DoChase(enemyController.EnemyDedector.PlayerTransform,
                enemyController.attackRange);
            
            enemyController.EnemyAnimationManager.SetEnemyDirection();
            enemyController.EnemyAnimationManager.SetEnemyMoveAnimation();
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
