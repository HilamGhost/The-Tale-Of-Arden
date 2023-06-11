using System;
using System.Collections;
using UnityEngine;

namespace Arden.Enemy
{
    public class EnemyStatManager
    {
        private EnemyController enemyController;
        private EnemyStatProperties enemyStatProperties;
        public EnemyStatManager(EnemyController _enemy,EnemyStatProperties _stat)
        {
            enemyController = _enemy;
            enemyStatProperties = _stat;
        }

        public void TakeDamage()
        {
            if (enemyStatProperties.health - 1 <= 0)
            {
                Die();
                return;
            }

            if (enemyController.IsStateEqual(enemyController.enemyHitState))
            {
                enemyController.StopCoroutine(WaitOnStun());
                enemyController.StartCoroutine(WaitOnStun());
            }
            else
            {
                enemyController.StopAllCoroutines();
                enemyController.EnemyAnimationManager.CancelAttackAnimation();
                enemyController.ChangeState(enemyController.enemyHitState);
                
            }
            enemyStatProperties.health--;
            enemyController.StartCoroutine(ApplyHitEffect());
            
        }

        public IEnumerator WaitOnStun()
        {
            yield return new WaitForSeconds(enemyStatProperties.hitStunCooldown);
            enemyController.ChangeState(enemyController.enemyIdleState);
        }
        void Die()
        {
            enemyController.enabled = false;
        }

        IEnumerator ApplyHitEffect()
        {
            var enemySR = enemyController.GetComponent<SpriteRenderer>();
            enemySR.material.shader = Shader.Find("Custom/DefaultColorFlash");
            
            enemySR.material.SetFloat("_FlashAmount",1);
            yield return new WaitForSeconds(0.1f);
            enemySR.material.SetFloat("_FlashAmount",0);
        }
    }

    [Serializable]
    public class EnemyStatProperties
    {
        public int health = 4;
        public float hitStunCooldown=1;
    }
}
