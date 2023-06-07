using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden.Player
{
    public class PlayerSoundManager : MonoBehaviour
    {
        [SerializeField] private AudioSource playerAudioSource;

        [Header("Audio Clip")] 
        [SerializeField] private AudioClip groundedAudioClip;
        private void Start()
        {
            MakeAssignment();
        }

        public void PlayAudio(AudioClip _audio)
        {
            playerAudioSource.PlayOneShot(_audio);
        }

        public void PlayGroundedSound()
        {
            PlayAudio(groundedAudioClip);
        }
        void MakeAssignment()
        {
            playerAudioSource = GetComponentInChildren<AudioSource>();
        }
    }
}
