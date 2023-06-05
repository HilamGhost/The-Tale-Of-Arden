using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Arden
{
    public class CamMove : MonoBehaviour
    {
        public Animator Animator;  
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Animator.SetTrigger("CamMove");
                StartCoroutine(scenewait());
            }
        
        } 
        IEnumerator scenewait()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }   
}
