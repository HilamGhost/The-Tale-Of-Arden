using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Arden.Timeline
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private TimelineAsset wantedCutscene;
        bool isPlayed;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out Player.PlayerParent player))
            {
                if(isPlayed) return;
                Debug.Log("PLAYY");
                TimelineManager.Instance.PlayTrack((PlayableAsset)wantedCutscene);
                isPlayed = true;
            }
        }
    }
}
