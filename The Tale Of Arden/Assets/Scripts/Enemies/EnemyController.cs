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

        [Header("OtherScripts")] 
        private EnemyMover enemyMover;
        private EnemyDedector enemyDedector;
        private EnemyAttackManager enemyAttackManager;
        private EnemyAnimationManager enemyAnimationManager;
        
        [Header("Properties")] 
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform[] patrols;
        [SerializeField] internal float attackRange;
        [SerializeField] private AttackProperties attackProperties;
        

        [Header("Dedection")] 
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private float playerDedectionRange;

        [Header("Visual")] 
        [SerializeField] private GameObject enemyAlert;
        
        #region Properties
        public EnemyMover EnemyMover => enemyMover;
        public EnemyDedector EnemyDedector => enemyDedector;
        public EnemyAttackManager EnemyAttackManager => enemyAttackManager;
        public EnemyAnimationManager EnemyAnimationManager => enemyAnimationManager;
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
        void MakeStateAssignment()
        {
            enemyIdleState = new EnemyIdleState(this);
            enemyChaseState = new EnemyChaseState(this);
            enemyAttackState = new EnemyAttackState(this);
            currentState = enemyIdleState;
            
            currentState.OnEnemyStateStart();
        }

        void MakeComponentAssignment()
        {
            enemyMover = new EnemyMover(this, moveSpeed,patrols);
            enemyDedector = new EnemyDedector(this, playerDedectionRange, playerLayer);
            enemyAttackManager = new EnemyAttackManager(this, attackProperties,attackRange);
            enemyAnimationManager = new EnemyAnimationManager(this);

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
            enemyAnimationManager.PlayAttackAnimation();
           
        }
        

        #endregion

        #region Visual Methods

        public void ToggleAlert() => StartCoroutine(ToggleAlertVision());
        IEnumerator ToggleAlertVision()
        {
            enemyAlert.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            enemyAlert.SetActive(false);
        }

        #endregion

        #region Gizmos

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position,playerDedectionRange);
        }

        #endregion
    }
}
