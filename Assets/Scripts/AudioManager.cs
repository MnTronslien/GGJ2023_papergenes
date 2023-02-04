//Create a manger for audio

using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public static float musicVolume = 1;
    public static float sfxVolume = 1;


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

        GameObject go = new GameObject($"OneShotAudio: {clip.name}");
        go.transform.position = worldSpacePos;
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume * AudioManager.sfxVolume;
        source.Play();
        await Task.Delay((int)(clip.length * 1000));
        Object.Destroy(go);   
    }
}