using UnityEngine;

namespace Arden.Enemy.State
{
    public class EnemyAttackState : IEnemyState
    {
        private EnemyController enemyController;
       
        public EnemyAttackState(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }
        
        public void OnEnemyStateStart()
        {
            enemyController.Attack();
        }

        public void OnEnemyStateUpdate()
        {
            enemyController.EnemyMover.EnemyDirection = 0;
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
