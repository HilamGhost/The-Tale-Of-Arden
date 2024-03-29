using System.Collections;
using System.Collections.Generic;
using Arden.Event;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Arden.Event
{    
    public class Dialogue : MonoBehaviour
    {
        [SerializeField] TextMeshPro UiObject;
        [SerializeField] string Textchange;
        [SerializeField] private float textDelay = 0.1f;
        [SerializeField] private AudioClip TextAudioClip;
        [SerializeField] private Transform textPos;
        
        [SerializeField] private UnityEvent callEvents;
        private AudioSource textAudioSource;
        

        private bool isStarted;
        void Start()
        {
            textAudioSource = GetComponent<AudioSource>();
           if(UiObject == null) UiObject = FindObjectOfType<NarrativeText>().GetComponent<TextMeshPro>();
            UiObject.text = null;
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if(isStarted) return;
                UiObject.text = "";
                
                UiObject.gameObject.SetActive(true);
                UiObject.transform.position = textPos.position;
                
                StartCoroutine(LevelText());
                isStarted = true;
            }
        }
        
        IEnumerator LevelText()
        {
            textAudioSource.PlayOneShot(TextAudioClip);
            
            for (int i = 0; i < Textchange.Length; i++)
            {
                string _substring = Textchange.Substring(i, 1);
                
                // Stringte $ işareti çıktığı zaman belirlenen eventleri(Kutu çıkması gibi) oynatır, $ işaretini siler.
                if (_substring == "$")
                {
                    ApplyThing(); //Eventleri Çağırır.
                    _substring = "";
                    yield return new WaitForSeconds(textDelay);
                }
                
                UiObject.text += _substring;
                yield return new WaitForSeconds(textDelay);
            }
            StartCoroutine(TextFinish());
            
        }
        IEnumerator TextFinish()
        {
            yield return new WaitForSeconds(5f);
            UiObject.gameObject.SetActive(false);
            UiObject.text = "";
            Destroy(gameObject);
        }

        void ApplyThing()
        {
            callEvents.Invoke();
        }


        


    }
}
