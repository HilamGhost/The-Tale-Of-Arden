using UnityEngine;

namespace Arden.Enemy
{
    public class EnemyDedector
    {
        private EnemyController enemyController;
        private Transform enemy;
        private Transform playerTransform;

        private LayerMask playerLayer;
        private float enemyDedectionRange;

        public Transform PlayerTransform => playerTransform;
        public EnemyDedector(EnemyController _enemyController,float _range,LayerMask _layerMask)
        {
            enemyController = _enemyController;
            enemy = enemyController.transform;
            
            enemyDedectionRange = _range;
            playerLayer = _layerMask;
        }

        public bool CheckPlayerIsNear()
        {
            Collider2D player = Physics2D.OverlapCircle(enemy.position,enemyDedectionRange,playerLayer);
            if (player != null)
            {
                DedectPlayer(player.transform);
                return true;
            }
            if (playerTransform != null)
            {
                DedectPlayer(null);
                return false;
            }
            
            return false;
        }

        public bool CanAttackToPlayer(float _attackRange)
        {
            float distanceX = Mathf.Abs(enemyController.transform.position.x - playerTransform.position.x);

            return playerTransform && distanceX <= _attackRange;
        }

        void DedectPlayer(Transform player)
        {
            playerTransform = player;
        }
        
    }
}
