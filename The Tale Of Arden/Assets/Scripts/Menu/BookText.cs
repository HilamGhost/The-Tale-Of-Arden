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
        public TextMeshPro TitleText;
        [SerializeField] private TextMeshPro Booklines;
        [SerializeField] private TextMeshPro Booktitle;
        float alpha;
        [SerializeField] private Animator Animator;
        [SerializeField] private AudioSource textaudiosource1;
        [SerializeField] private AudioSource textaudiosource2;
        [SerializeField] private AudioSource textaudiosource3;
        [SerializeField] private AudioSource textaudiosource4;




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
            float subalpha = 1f;
            textaudiosource1.Play();
            SubtitleText.text = SubtitleText1;
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.001f);
                subalpha += 0.01f;
                SubtitleText.color = new Color(0, 0, 0, subalpha);
            }
            
            yield return new WaitForSeconds(6.5f);
            SubtitleText.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(1.5f);
            SubtitleText.color = new Color(0, 0, 0, 1);
            SubtitleText.text = SubtitleText2;
            textaudiosource2.Play();
          
            yield return new WaitForSeconds(5.7f);
            SubtitleText.color = new Color(0, 0, 0, 0);
            yield return new WaitForSeconds(1.5f);
            SubtitleText.color = new Color(0, 0, 0, 1);


            SubtitleText.text = SubtitleText3;
            
            textaudiosource3.Play();
            
            yield return new WaitForSeconds(7.5f);
            subalpha = 1f;
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.01f);
                subalpha -= 1f;
                SubtitleText.color = new Color(1, 1, 1, subalpha);
            }
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < 100; i++)
            {
                yield return new WaitForSeconds(0.02f);
                alpha -= 0.01f;
                TitleText.color = new Color(1, 1, 1, alpha);
            }
            StartCoroutine(ahmetkaya());
           



        }

        
        IEnumerator ahmetkaya()
        {
            textaudiosource4.Play();
            for (int i = 0; i < Booklines.text.Length; i++)
            {
                Booktitle.text += Booklines.text.Substring(i,1);
                yield return new WaitForSeconds(0.1f);
                

            }
            yield return new WaitForSeconds(1f);
            Animator.SetTrigger("Left");
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        void Update()
        {
        
        }
    }
}
