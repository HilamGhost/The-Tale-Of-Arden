using UnityEngine;

namespace Arden.Enemy.State
{
    public interface IEnemyState
    {
        public void OnEnemyStateStart();
        public void OnEnemyStateUpdate();
        public void OnEnemyStateFixed();
        public void OnEnemyStateExit();
        public void OnEnemyStateTriggerEnter(Collider2D col);
    }
}
