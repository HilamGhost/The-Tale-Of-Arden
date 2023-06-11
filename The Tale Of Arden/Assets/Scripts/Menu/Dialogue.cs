using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Arden
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
            UiObject.text = null;
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if(isStarted) return;
                
                UiObject.gameObject.SetActive(true);
                StartCoroutine(leveltext());
                isStarted = true;
            }
        }
        
        IEnumerator leveltext()
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
            StartCoroutine(textfinish());
            
        }
        IEnumerator textfinish()
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
