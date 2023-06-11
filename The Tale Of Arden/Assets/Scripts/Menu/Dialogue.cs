using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Arden
{    
    public class Dialogue : MonoBehaviour
    {
        public TextMeshProUGUI UiObject;
        string Textchange;
        void Start()
        {
            Textchange = UiObject.text;
            UiObject.text = null;

        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                UiObject.gameObject.SetActive(true);
                StartCoroutine(leveltext());
            }
        }
        void Update()
        {
        
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
               
                StartCoroutine(textfinish());
                
            }
        }
        IEnumerator leveltext()
        {
            for (int i = 0; i < Textchange.Length; i++)
            {
                UiObject.text += Textchange.Substring(i, 1);
                yield return new WaitForSeconds(0.05f);


            }
            
            
        }
        IEnumerator textfinish()
        {
            yield return new WaitForSeconds(1f);
            UiObject.gameObject.SetActive(false);
            Destroy(gameObject);
        }
            


        


    }
}
