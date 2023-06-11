using System.Collections;
using UnityEngine;

namespace Arden.Enemy
{
    public class EnemyMover
    {
        private EnemyController enemyController;
        
        private Transform enemyTransform;

        private float moveSpeed;
        private bool isWaiting;
        
        private Vector2 patrolStart;
        private Vector2 patrolEnd;
        
        enum PatrolState {PatrolStart,PatrolEnd}
        private PatrolState currentPatrolState;

        private float targetPointX;
        public float EnemyDirection;
        
        #region Properties
        
        PatrolState ReverseState
        {
            get
            {
                PatrolState wantedState = currentPatrolState;
                if (currentPatrolState == PatrolState.PatrolEnd) wantedState = PatrolState.PatrolStart;
                else if (currentPatrolState == PatrolState.PatrolStart) wantedState = PatrolState.PatrolEnd;

                return wantedState;
            }
            
        }
        #endregion
      
        public EnemyMover(EnemyController _enemyController,float _moveSpeed,Transform[] patrols) 
        {
            moveSpeed = _moveSpeed;

            patrolStart = patrols[0].position;
            patrolEnd = patrols[1].position;
            currentPatrolState = PatrolState.PatrolStart;
            SetTarget(PatrolState.PatrolStart);
            
            
            enemyTransform = _enemyController.transform;
            enemyController = _enemyController;
            enemyController.DestroyPatrolMethods();
        }

        
       
        #region Patrol Methods

        public void DoPatrol()
        {
            CheckEnemyIsInCorner();
            
            if (!isWaiting)
            {
                Vector2 _target = new Vector2(targetPointX,  enemyTransform.position.y);

                enemyTransform.position = Vector3.MoveTowards(enemyTransform.position,_target,moveSpeed*Time.deltaTime);
                
                if (_target.x - enemyTransform.position.x > 0) EnemyDirection = 1;
                if (_target.x - enemyTransform.position.x < 0) EnemyDirection = -1;
            }
        }

        void CheckEnemyIsInCorner()
        {
            if (Mathf.Approximately(enemyTransform.position.x, targetPointX))
            {
                enemyController.StartCoroutine(WaitInSide(ReverseState));
            }
        }
        
        IEnumerator WaitInSide(PatrolState _side)
        {
            EnemyDirection = 0;
            
            isWaiting = true;
            yield return new WaitForSeconds(1);
            if(isWaiting) SetTarget(_side);
            isWaiting = false;
        }
        
        void SetTarget(PatrolState _side)
        {
            currentPatrolState = _side;
            targetPointX = currentPatrolState switch
            {
                PatrolState.PatrolStart => patrolEnd.x,
                PatrolState.PatrolEnd => patrolStart.x,
                _ => 0
            };
        }

        #endregion

        #region Chase Methods

        public void DoChase(Transform _player,float _attackRange)
        {

            CheckEnemyIsOnPlayer(_player,_attackRange);
            
           
            Vector2 _target = new Vector2(_player.transform.position.x,  enemyTransform.position.y);
            enemyTransform.position = Vector3.MoveTowards(enemyTransform.position,_target,moveSpeed*Time.deltaTime);
            
            if (_target.x - enemyTransform.position.x > 0) EnemyDirection = 1;
            if (_target.x - enemyTransform.position.x < 0) EnemyDirection = -1;
            
        }
        
        void CheckEnemyIsOnPlayer(Transform _player,float _attackRange)
        {
            float distanceX = Mathf.Abs(enemyTransform.position.x - _player.position.x);
            
            
            float distanceY = Mathf.Abs(enemyTransform.position.y - _player.position.y);
            
            if (distanceX <= _attackRange && distanceY <= 1f)
            {
                EnemyDirection = 0;
                
                enemyController.ChangeState(enemyController.enemyAttackState);
            }
        }

        #endregion
       
    }
}
