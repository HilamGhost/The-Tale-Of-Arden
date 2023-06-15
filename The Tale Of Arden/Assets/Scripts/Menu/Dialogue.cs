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
        [SerializeField] TextMeshProUGUI UiObject;
        [SerializeField] string Textchange;
        [SerializeField] private float textDelay = 0.1f;
       
        [SerializeField] private UnityEvent callEvents;

        private bool isStarted;
        void Start()
        {
            UiObject = FindObjectOfType<NarrativeText>().GetComponent<TextMeshProUGUI>();
            UiObject.text = null;
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if(isStarted) return;
                
                UiObject.gameObject.SetActive(true);
                StartCoroutine(LevelText());
                isStarted = true;
            }
        }
        
        IEnumerator LevelText()
        {
            for (int i = 0; i < Textchange.Length; i++)
            {
                string _substring = Textchange.Substring(i, 1);
                
                if (_substring == "$")
                {
                    ApplyThing();
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
            yield return new WaitForSeconds(1f);
            UiObject.gameObject.SetActive(false);
            Destroy(gameObject);
        }

        void ApplyThing()
        {
            callEvents.Invoke();
        }


        


    }
}
