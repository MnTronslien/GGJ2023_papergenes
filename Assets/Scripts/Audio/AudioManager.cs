//Create a manger for audio

using System.Threading.Tasks;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{

    public static float musicVolume = 1;
    public static float sfxVolume = 1;


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
        source.Play();
        await Task.Delay((int)(clip.length * 1000));
        if (!Application.isPlaying)
            return;
        AudioManager.Instance.sfxTracks.Remove(source);
        Object.Destroy(go);   
    }
//Play oneshot tracking a transform
    public static async void PlayOneShot(this AudioClip clip, float volume, Transform track)
    {
        //Discard if not in play mode
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Not in play mode, discarding");
            return;
        }
        //Block if not any more tracks available, discard if way to many
        while (AudioManager.Instance.sfxTracks.Count >= AudioManager.maxSFXTracks)
        {
            await Task.Delay(100);
            if (AudioManager.Instance.sfxTracks.Count > AudioManager.maxSFXTracks * 2)
            {
                Debug.LogWarning("Too many SFX tracks, discarding");
                return;
            }
        }


        GameObject go = new GameObject($"OneShotAudio: {clip.name}");
        Debug.Log($"Playing {clip.name} at {track.position}");
        go.transform.position = track.position;
        AudioSource source = go.AddComponent<AudioSource>();
        AudioManager.Instance.sfxTracks.Add(source);
        source.clip = clip;
        source.volume = volume * AudioManager.sfxVolume;
        source.Play();
        while (source.isPlaying)
        {
            go.transform.position = track.position;
            await Task.Delay(100);
        }
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
    
}


