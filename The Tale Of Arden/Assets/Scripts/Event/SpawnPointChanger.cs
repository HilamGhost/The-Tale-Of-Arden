using Arden.Player;
using UnityEngine;

namespace Arden.Event
{
    public class SpawnPointChanger : MonoBehaviour
    {
        [SerializeField] private float yOffset;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.TryGetComponent(out PlayerParent player))
            {
                Vector2 _pos = new Vector2(transform.position.x, yOffset);
                GameManager.Instance.SetSpawnPos(_pos);
            }
        }
    }
}
