using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden.Player
{
    public class PlayerSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource playerAudioSource;

        [Header("Audio Clip")] 
        [SerializeField] private AudioClip[] walkAudioClips;
        [SerializeField] private AudioClip jumpAudioClip;
        [SerializeField] private AudioClip groundedAudioClip;
        [SerializeField] private AudioClip dashAudioClip;
        [SerializeField] private AudioClip playerAttackAudioClip;
        [SerializeField] private AudioClip playerHitAudioClip;
        
        private bool isSecond;
        private void Start()
        {
            MakeAssignment();
        }

        public void PlayAudio(AudioClip _audio)
        {
            playerAudioSource.PlayOneShot(_audio);
        }

        public void PlayWalkSound()
        {
            AudioClip currentFootstep = isSecond ? walkAudioClips[1] : walkAudioClips[0];
            isSecond = !isSecond;
            
            PlayAudio(currentFootstep);
        }

        public void PlayJumpSound()
        {
            PlayAudio(jumpAudioClip);
        }
        public void PlayHitSound()
        {
            PlayAudio(playerHitAudioClip);
        }

        public void PlayDashAudioClip()
        {
            PlayAudio(dashAudioClip);
        }
        public void PlayGroundedSound()
        {
            PlayAudio(groundedAudioClip);
        }

        public void PlayAttackSound()
        {
            PlayAudio(playerAttackAudioClip);
        }
        
        void MakeAssignment()
        {
            playerAudioSource = GetComponentInChildren<AudioSource>();
        }
    }
}
