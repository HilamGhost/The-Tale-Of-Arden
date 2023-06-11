using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden
{
    [CreateAssetMenu(fileName = "New Dialogue Event Manager",menuName = "Events/ Create Event Manager")]
    public class InGameDialogueEventManager : ScriptableObject
    {
        private Transform callerTransform;
        public void SetCallerTransform(Transform _transform) => callerTransform = _transform;
        public void ResetCallerTransform() => callerTransform = null;
        
        
        public void CreateObject(GameObject _gameObject)
        {
            if(!callerTransform) return;
            Vector3 createdObjectPos = callerTransform.position;
            Instantiate(_gameObject, createdObjectPos, Quaternion.identity);
        }
    }
}