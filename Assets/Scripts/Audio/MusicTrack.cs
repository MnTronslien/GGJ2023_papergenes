using UnityEngine;
using System.Collections.Generic;

//Make a scriptablke object that can hold layerd music tracks
[System.Serializable,CreateAssetMenu(fileName = "New Music Track", menuName = "Music Track")]
public class MusicTrack : ScriptableObject
{
    public AudioClip[] Layers;
    public VolumePreset currentPreset;
    public float[] LayerCurrentVolumes;
    public List<VolumePreset> LayerPresets;

   
}

