using UnityEngine;

namespace Arden.Enemy.State
{
    public class EnemyHitState : IEnemyState
    {
        private EnemyController enemyController;
       
        public EnemyHitState(EnemyController _enemyController)
        {
            enemyController = _enemyController;
        }
        
        public void OnEnemyStateStart()
        {
            enemyController.StartCoroutine(enemyController.EnemyStatManager.WaitOnStun());
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
