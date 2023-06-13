using System;
using UnityEngine.Playables;

namespace Arden.Timeline
{
    public class TimelineManager : Singleton<TimelineManager>
    {
        private PlayableDirector playerCutsceneTimeline;

        private void Start()
        {
            playerCutsceneTimeline = GetComponent<PlayableDirector>();
        }

        public void PlayTrack(PlayableAsset _cutscene)
        {
            playerCutsceneTimeline.playableAsset = _cutscene;
            playerCutsceneTimeline.Play();
        }

        
        
        
    }
}
