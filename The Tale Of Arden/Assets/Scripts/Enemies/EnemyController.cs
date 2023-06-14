using System;
using System.Collections;
using Arden.Enemy.State;
using UnityEngine;

namespace Arden.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("States")] 
        private IEnemyState currentState;
        internal EnemyIdleState enemyIdleState;
        internal EnemyChaseState enemyChaseState;
        internal EnemyAttackState enemyAttackState;
        internal EnemyHitState enemyHitState;

        [Header("OtherScripts")] 
        private EnemyMover enemyMover;
        private EnemyDedector enemyDedector;
        private EnemyAttackManager enemyAttackManager;
        private EnemyAnimationManager enemyAnimationManager;
        private EnemyStatManager enemyStatManager;
        private EnemySoundManager enemySoundManager;
        
        [Header("Properties")] 
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform[] patrols;
        [SerializeField] internal float attackRange;
        [SerializeField] private AttackProperties attackProperties;
        [SerializeField] private EnemyStatProperties statProperties;
        

        [Header("Dedection")] 
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float playerDedectionRange;

        
        #region Properties
        public EnemyMover EnemyMover => enemyMover;
        public EnemyDedector EnemyDedector => enemyDedector;
        public EnemyAttackManager EnemyAttackManager => enemyAttackManager;
        public EnemyAnimationManager EnemyAnimationManager => enemyAnimationManager;
        public EnemyStatManager EnemyStatManager => enemyStatManager;
        public EnemySoundManager EnemySoundManager => enemySoundManager;
        public float VelocityDirection =>enemyMover.EnemyDirection;
        public float EnemyDirection => transform.localScale.x;
        #endregion
        
        void Start()
        {
            MakeStateAssignment();
            MakeComponentAssignment();
        }
        
        void Update()
        {
            Debug.Log($"{transform.name} is in {currentState}");
            currentState.OnEnemyStateUpdate();
        }

        private void FixedUpdate()
        {
            currentState.OnEnemyStateFixed();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            currentState.OnEnemyStateTriggerEnter(col);
        }
        public void ChangeState(IEnemyState wantedState)
        {
            if (currentState != wantedState)
            {
                currentState.OnEnemyStateExit();
                currentState = wantedState;
                currentState.OnEnemyStateStart(); 

            }
        }

        public bool IsStateEqual(IEnemyState wantedState) => currentState == wantedState;
        void MakeStateAssignment()
        {
            enemyIdleState = new EnemyIdleState(this);
            enemyChaseState = new EnemyChaseState(this);
            enemyAttackState = new EnemyAttackState(this);
            enemyHitState = new EnemyHitState(this);
            currentState = enemyIdleState;
            
            currentState.OnEnemyStateStart();
        }

        void MakeComponentAssignment()
        {
            enemyMover = new EnemyMover(this, moveSpeed,patrols);
            enemyDedector = new EnemyDedector(this, playerDedectionRange, playerLayer);
            enemyAttackManager = new EnemyAttackManager(this, attackProperties,attackRange);
            enemyAnimationManager = new EnemyAnimationManager(this);
            enemyStatManager = new EnemyStatManager(this, statProperties);
            enemySoundManager = GetComponent<EnemySoundManager>();

        }


        #region Patrol Methods

        protected internal void DestroyPatrolMethods()
        {
            foreach (var _patrol in patrols)
            {
                Destroy(_patrol.gameObject);
            }
            
        }

        #endregion

        #region Attack Methods

        public void Attack()
        {
            StartCoroutine(enemyAttackManager.StartParry());
            StartCoroutine(enemyAttackManager.StartAttack());
            
           
        }
        

        #endregion

        #region Damage Methods

        public void TakeDamage()
        {
            enemyStatManager.TakeDamage();
        }
        

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position,playerDedectionRange);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(attackProperties.attackPoint.position,attackRange);
        }

        #endregion
    }
}
