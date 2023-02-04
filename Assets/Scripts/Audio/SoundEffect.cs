using UnityEngine;
//Make a scriptablke object that can hold sound effects
[CreateAssetMenu(fileName = "New Sound Effect", menuName = "Sound Effect")]
public class SoundEffect : ScriptableObject
{
    public AudioClip[] clips;
   [HideInInspector] public int lastPlayed = -1;



}