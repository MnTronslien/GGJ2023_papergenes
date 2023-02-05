using UnityEngine;
//make a private serializable class for holding volume presets
[System.Serializable, CreateAssetMenu(fileName = "New Volume Preset", menuName = "Volume Preset")]
public class VolumePreset : ScriptableObject
{

    public float[] LayerVolume;
}