using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden.VFX
{
    public class VFXManager : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
                if(_particleSystem.isStopped) Destroy(gameObject);
        }
    }
}
