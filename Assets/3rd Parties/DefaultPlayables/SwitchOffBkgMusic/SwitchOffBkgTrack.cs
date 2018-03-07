using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[TrackColor(0.855f, 0.8623f, 0.87f)]
[TrackClipType(typeof(SwitchOffBkgClip))]
public class SwitchOffBkgTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<SwitchOffBkgMixerBehaviour>.Create (graph, inputCount);
    }

    public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
    {
#if UNITY_EDITOR
        
       
#endif
        base.GatherProperties(director, driver);
    }
}
