using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden.Enemy
{
    public class EnemyAnimationManager
    {
        private EnemyController enemyController;
        private Animator animator;
        private float moveDirection = 1;
        public EnemyAnimationManager(EnemyController enemyController)
        {
            this.enemyController = enemyController;
            animator = this.enemyController.GetComponent<Animator>();
        }

        public void SetEnemyDirection()
        {
            if (Mathf.Approximately(enemyController.VelocityDirection,0)) return;
            

            moveDirection = enemyController.VelocityDirection * -1;
            enemyController.transform.localScale = new Vector3(moveDirection, 1, 1);
            

        }

        public void SetEnemyMoveAnimation()
        {
            bool isMoving = !Mathf.Approximately(enemyController.VelocityDirection,0);
            animator.SetBool("isMoving",isMoving);
            
        }

        public void StopEnemyAnimation()
        {
            animator.SetBool("isMoving",false);
        }

        public void PlayAttackAnimation()
        {
            animator.SetTrigger("Attack");
        }
        public void CancelAttackAnimation()
        {
            animator.SetTrigger("CancelAttack");
        }
    }
}
