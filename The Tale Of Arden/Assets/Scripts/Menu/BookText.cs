using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


namespace Arden
{
    public class BookText : MonoBehaviour
    {
        [SerializeField] private string SubtitleText1, SubtitleText2, SubtitleText3;
        [SerializeField] private TextMeshPro SubtitleText;
        public GameObject TitleParent;
        public TextMeshPro TitleText;
        [SerializeField] private TextMeshPro Booklines;
        [SerializeField] private TextMeshPro Booktitle;
        float alpha;
        [SerializeField] private Animator Animator;
        


        void Start()
        {
            SubtitleText.text = null;
            StartCoroutine(opacityadder());
            StartCoroutine(subtitle());
        }
        IEnumerator opacityadder()
        {
            yield return new WaitForSeconds(2f);
            alpha = 0f;
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                alpha += 0.01f;
                TitleText.color = new Color(1, 1, 1, alpha);
            }
        }
        IEnumerator subtitle()
        {
            yield return new WaitForSeconds(5f);
            float subalpha = 0f;
            SubtitleText.text = SubtitleText1;
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                subalpha += 0.01f;
                SubtitleText.color = new Color(1, 1, 1, subalpha);
            }
            yield return new WaitForSeconds(3f);
            SubtitleText.text = SubtitleText2;
            yield return new WaitForSeconds(3f);
            SubtitleText.text = SubtitleText3;
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                subalpha -= 0.01f;
                SubtitleText.color = new Color(1, 1, 1, subalpha);
            }
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                alpha -= 0.01f;
                TitleText.color = new Color(1, 1, 1, alpha);
            }
            StartCoroutine(ahmetkaya());
           



        }
        IEnumerator ahmetkaya()
        {   
            for (int i = 0; i < Booklines.text.Length; i++)
            {
                Booktitle.text += Booklines.text.Substring(i,1);
                yield return new WaitForSeconds(0.1f);
                

            }
            yield return new WaitForSeconds(1f);
            Animator.SetTrigger("CamMove");
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        void Update()
        {
        
        }
    }
}
