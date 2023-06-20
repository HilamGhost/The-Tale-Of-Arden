using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Mathematics;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace Arden.Event
{
    public class CorruptedTextTrigger : MonoBehaviour
    { 
        [SerializeField] string fullSentence;
        [SerializeField] private float textDelay = 0.1f;
        
        [SerializeField] string[] textOfWords;
        [Header("Assignments")]
        [SerializeField] private CorruptedWord[] wordPoses;
        [SerializeField] private string corruptedWord;
        [SerializeField] private AudioClip TextAudioClip;
       
        [Header("Transforming Object")] 
        [SerializeField] private GameObject firstObject;
        [SerializeField] private GameObject transformedObject;
        [SerializeField] private ParticleSystem transformParticleEffect;

        [Header("Platforms")] 
        [SerializeField] private GameObject wordPlatform;
        [SerializeField] private GameObject platformParent;
        
        private AudioSource textAudioSource;

        private bool isStarted;
        void Start()
        {
            
            textOfWords = GetAllWords(fullSentence);
            SetAllWordPoses();
            
            textAudioSource = GetComponent<AudioSource>();
            
        }
        void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Player"))
            {
                if(isStarted) return;
                
                StartCoroutine(ChangeText());
                isStarted = true;
            }
        }
        
        IEnumerator ChangeText()
        {
            textAudioSource.PlayOneShot(TextAudioClip);
            
            for (int p = 0; p < wordPoses.Length; p++)
            {
                for (int i = 0; i < wordPoses[p].FullWord.Length; i++)
                {
                    string _substring = wordPoses[p].FullWord.Substring(i, 1);

                    wordPoses[p].CurrentWord += _substring;
                    yield return new WaitForSeconds(textDelay);
                }
                yield return new WaitForSeconds(textDelay);
            }
           
            Debug.Log("Biter");

        }
        public void FinishText()
        {
            Destroy(gameObject);
        }

        #region Word Methods

        public string[] GetAllWords(string _wantedString)
        {
            string[] _wordList = _wantedString.Split(new[] { " " },StringSplitOptions.None);
            return _wordList;
        }
        public void SetAllWordPoses()
        {
            for (int i = 0; i < wordPoses.Length; i++)
            {
                wordPoses[i].SetWord(textOfWords[i]);
                
                if(textOfWords[i] == corruptedWord) wordPoses[i].SetCorrupted();
            }
        }


        #endregion

        #region ChangeMethods

        public void TransformObject()
        {
            Vector2 pos = firstObject.transform.position;
            firstObject.SetActive(false);

            Instantiate(transformedObject, pos, quaternion.identity);
            Instantiate(transformParticleEffect, pos, quaternion.identity);
        }
        

        #endregion
        #region SetThePlatforms
        public void SetPlatforms()
        {
            
            while(platformParent.transform.childCount>0)
            {
                DestroyImmediate(platformParent.transform.GetChild(0).gameObject);
            }
            
            textOfWords = GetAllWords(fullSentence);
            wordPoses = new CorruptedWord[textOfWords.Length];
            
            int _lenghtOfWord = wordPoses.Length;
            for (int i = 0; i < _lenghtOfWord; i++)
            {
                GameObject _platform = Instantiate(wordPlatform, transform.position, quaternion.identity,platformParent.transform);
                wordPoses[i] = _platform.GetComponent<CorruptedWord>();
            }
        }
        #endregion
       
    }
}
