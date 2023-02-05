//Create a manger for audio

using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
    [Range(0, 1)]
    public static float musicVolume = 1;
    public static float sfxVolume = 1;

    //Track with layers
    public MusicTrack currentMusicTrack;
    public VolumePreset Debug_VolumePreset;
    [HideInInspector] public List<AudioSource> musicTrackLayers = new List<AudioSource>();


    public List<AudioSource> sfxTracks = new List<AudioSource>();
    public const int maxSFXTracks = 10;


    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }

    //Make a method to apply music track presets to current music track
    //The method should find the preset based on name
    //Then apply the preset to the current music track
    public void ApplyMusicTrackPreset(string presetName, float duration = 2)
    {
        //Find the preset
        VolumePreset preset = currentMusicTrack.LayerPresets.Find(x => x.name.ToLower() == presetName.ToLower());
        if (preset == null)
        {
            Debug.LogWarning($"No preset found with name {presetName}");
            return;
        }

        //save the preset applied
        currentMusicTrack.currentPreset = preset;
        //For each layer in the current music track,
        // Fade the volume to the preset volume
        for (int i = 0; i < currentMusicTrack.Layers.Length; i++)
        {
            FadeMusicLayerVolumeToPresetVolume(i, preset.LayerVolume[i], duration);
        }   
    }

    private async void FadeMusicLayerVolumeToPresetVolume(int layerID, float presetVolume, float duration)
    {
        //loop that fades
        float t = 0;
        float startVolume = musicTrackLayers[layerID].volume;
        while (t < duration)
        {
            //Check if still in play mode
            if (!Application.isPlaying) return;
            musicTrackLayers[layerID].volume = Mathf.Lerp(startVolume, presetVolume, t / duration);
            t += Time.deltaTime;
            await Task.Delay(1);
        }
    }

    //Context menu method to cycle presets
    [ContextMenu("Apply current Debug Preset")]
    public void ApplyDebugPreset()
    {
        ApplyMusicTrackPreset(Debug_VolumePreset.name);
    }
    
  

    //Make a method to reload the current music track
    [ContextMenu("Reload Current Music Track")]
    public void ReloadCurrentMusicTrack()
    {
        currentMusicTrack.PlayMusicTrack(0);
    }
}

//Create a script to play music and sfx
public static class AudioExtensions
{
    public static async void PlayOneShot(this AudioClip clip, float volume, Vector3 worldSpacePos)
    {
//Spawn a game object with an audio source at location, 
//play audio clip at volume from audio source
// Wait untill the clip is done playing and then destroy the game object.
        //Discard if not in play mode
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Not in play mode, discarding");
            return;
        }
        //Discard if not any more tracks available
        if (AudioManager.Instance.sfxTracks.Count > 2)
        {
            Debug.LogWarning("Too many SFX tracks, discarding");
            return;
        }

        GameObject go = new GameObject($"OneShotAudio: {clip.name}");
        Debug.Log($"Playing {clip.name} at {worldSpacePos}");
        go.transform.position = worldSpacePos;
        AudioSource source = go.AddComponent<AudioSource>();
        AudioManager.Instance.sfxTracks.Add(source);
        source.clip = clip;
        source.volume = volume * AudioManager.sfxVolume;
        source.spatialBlend = 1;
        source.Play();
        await Task.Delay((int)(clip.length * 1000));
        if (!Application.isPlaying) return; //Because of await we might have exited the game
        AudioManager.Instance.sfxTracks.Remove(source);
        Object.Destroy(go);   
    }

    //Play one shot at random from list of clips
    public static void PlayOneShot(this AudioClip[] clips, float volume, Vector3 worldSpacePos)
    {
        if (clips.Length == 0)
            return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        clip.PlayOneShot(volume, worldSpacePos);
    }

    //Make a static public method that plays a sound effect
    public static void PlaySoundEffect(this SoundEffect effect, float volume, Vector3 worldSpacePos)
    {
        //Select a random clip that isnt the same as last played
        int index = Random.Range(0, effect.clips.Length);
        while (index == effect.lastPlayed && effect.clips.Length > 1){
            index = Random.Range(0, effect.clips.Length);
        }
        effect.lastPlayed = index;
        effect.clips.PlayOneShot(volume, worldSpacePos);
    }


    //Make a static public method that plays a music track
    public static void PlayMusicTrack(this MusicTrack track, float volume = 1)
    {
        //If there is any musictracklayers playing, destroy them all
        if (AudioManager.Instance.musicTrackLayers.Count > 0)
        {
            foreach (AudioSource source in AudioManager.Instance.musicTrackLayers)
            {
                Object.Destroy(source.gameObject);
            }
            AudioManager.Instance.musicTrackLayers.Clear();
        }

        //Create a new game object for each layer
        foreach (AudioClip layer in track.Layers)
        {
            GameObject go = new GameObject($"MusicTrackLayer: {layer.name}");
            go.transform.position = Vector3.zero;
            //attach to audio manager so they become DoNOtDestroyOnLoad
            go.transform.SetParent(AudioManager.Instance.transform);
            AudioSource source = go.AddComponent<AudioSource>();
            AudioManager.Instance.musicTrackLayers.Add(source);
            source.clip = layer;
            source.volume = volume * AudioManager.musicVolume;
            source.loop = true;
            source.spatialBlend = 0;
            source.Play();
        }
    }



    
}


