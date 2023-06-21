using System;
using System.Collections;
using Arden.Player;
using TMPro;
using UnityEngine;

namespace Arden.Cutscene
{
    public class LastCutscene : MonoBehaviour
    {
        
        [SerializeField] private Dialogue[] Dialogues;
        [SerializeField] private float textDelay = 0.1f;
        [SerializeField] private float waitForNextDialogue = 1;

        private int currentDialogue;
        private AudioSource textAudioSource;
        private LevelLoader _levelLoader;

        private void Awake()
        { 
            textAudioSource = GetComponent<AudioSource>();
            _levelLoader = GameObject.FindObjectOfType<LevelLoader>();
        }

        public void StartText()
        {
            StartCoroutine(LevelText());
        }
        IEnumerator LevelText()
        {
            if (currentDialogue >= Dialogues.Length)
            {
                Debug.Log("IT ENDS");
                _levelLoader.LoadNextLevel();
                yield break;
            }
            
            Dialogues[currentDialogue].srBubble.enabled = true;
            textAudioSource.PlayOneShot(Dialogues[currentDialogue].TextAudioClip);
            
            for (int i = 0; i < Dialogues[currentDialogue].dialogue.Length; i++)
            {
                string _substring = Dialogues[currentDialogue].dialogue.Substring(i, 1);
                Dialogues[currentDialogue].UIObject.text += _substring;
                yield return new WaitForSeconds(textDelay);
            }

            currentDialogue++;
           
            yield return new WaitForSeconds(waitForNextDialogue);
            Dialogues[currentDialogue-1].UIObject.text ="";
            Dialogues[currentDialogue - 1].srBubble.enabled = false;
            StartCoroutine(LevelText());
            
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.transform.TryGetComponent(out PlayerParent playerParent))
            {
                StartText();
            }
        }
    }

    [Serializable]
    public struct Dialogue
    {
        public TextMeshPro UIObject;
        public string dialogue;
        public AudioClip TextAudioClip;
        public SpriteRenderer srBubble;
    }
}
