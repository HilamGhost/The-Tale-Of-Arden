using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


namespace Arden
{
    public class Booktext2 : MonoBehaviour
    {
        [SerializeField] private string SubtitleText1, SubtitleText2, SubtitleText3;
        [SerializeField] private TextMeshPro SubtitleText;
        public TextMeshPro TitleText;
        [SerializeField] private TextMeshPro Booklines;
        [SerializeField] private TextMeshPro Booktitle;
        float alpha;

        
        public SpriteRenderer tree;
        float subalpha = 0f;



        void Start()
        {
            SubtitleText.text = null;
            StartCoroutine(opacityadder());
            StartCoroutine(subtitle());
        }
        IEnumerator opacityadder()
        {
            yield return new WaitForSeconds(0f);
            alpha = 0f;
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.0f);
                alpha += 0.0f;
                TitleText.color = new Color(1, 1, 1, alpha);
            }
        }
        IEnumerator subtitle()
        {









            yield return new WaitForSeconds(0f);

            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.0f);

                SubtitleText.color = new Color(1, 1, 1);
            }
            yield return new WaitForSeconds(0f);
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.0f);
                alpha -= 0.0f;
                TitleText.color = new Color(1, 1, 1, alpha);
            }
            StartCoroutine(ahmetkaya());




        }


        IEnumerator ahmetkaya()
        {
            yield return new WaitForSeconds(0.1f);
            alpha = 0f;
            for (int i = 0; i < 100; i++)

            {
                yield return new WaitForSeconds(0.02f);
                alpha += 0.01f;
                tree.color = new Color(1, 1, 1, alpha);
            }
           
          
           

            yield return new WaitForSeconds(1f);
          
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        void Update()
        {

        }
    }
}

