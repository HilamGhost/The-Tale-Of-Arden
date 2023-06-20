using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Arden.Timeline
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private TimelineAsset wantedCutscene;
        bool isPlayed;
        [SerializeField] private Animator Levelfadein;
        


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player.PlayerParent player))
            {
                if(isPlayed) return;
                Debug.Log("PLAYY");
                TimelineManager.Instance.PlayTrack((PlayableAsset)wantedCutscene);
                isPlayed = true;
                StartCoroutine(levelcomplete());
            }       
        }
        IEnumerator levelcomplete()
        {
            yield return new WaitForSeconds(2f);
            Levelfadein.SetTrigger("Start");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            
        }
            
        
        
        
    }


   
}
