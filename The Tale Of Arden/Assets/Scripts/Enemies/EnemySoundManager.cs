using UnityEngine;

namespace Arden.Enemy
{
    public class EnemySoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        
        [Header("Audios")]
        [SerializeField] private AudioClip[] walkAudioClips;
        [SerializeField] private AudioClip hitAudioClip;
        [SerializeField] private AudioClip parryAudioClip;
        [SerializeField] private AudioClip enemyAttackAudioClip;
        [SerializeField] private AudioClip deathAudioClip;
        private bool isSecond;
        private void Start()
        {
            MakeAssignment();
        }

        public void PlayAudio(AudioClip _audio)
        {
            audioSource.PlayOneShot(_audio);
        }

        public void PlayWalkSound()
        {
            AudioClip currentFootstep = isSecond ? walkAudioClips[1] : walkAudioClips[0];
            isSecond = !isSecond;
            
            PlayAudio(currentFootstep);
        }

        public void PlayParrySound()
        {
            PlayAudio(parryAudioClip);
        }

        public void PlayHitAudioClip()
        {
            PlayAudio(hitAudioClip);
        }
        public void PlayDeathSound()
        {
            PlayAudio(deathAudioClip);
        }

        public void PlayAttackSound()
        {
            PlayAudio(enemyAttackAudioClip);
        }
        
        void MakeAssignment()
        {
            audioSource = GetComponentInChildren<AudioSource>();
        }
    }
}
