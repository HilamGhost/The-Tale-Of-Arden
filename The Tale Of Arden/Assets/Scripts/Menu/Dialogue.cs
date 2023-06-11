using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arden
{
    public class Dialogue : MonoBehaviour
    {
        public GameObject UiObject;
        
        void Start()
        {
            
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                UiObject.SetActive(true);
            }
        }
        void Update()
        {
        
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                UiObject.SetActive(false);
                Destroy(gameObject);
            }
        }



    }
}
