using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public SlideAnimation entryDoor;
    public SlideAnimation mainDoor;
    public SlideAnimation bonusDoor;

    public AudioClip doorOpenSound;
    [Tooltip("How much sooner than the sound should the door finish animating")]
    public float doorMoveFinishOffset = 0.5f;

    //TODO bonus door requirements

    public int Init()
    {
        if(entryDoor != null)
            StartCoroutine(DoCloseDoor(entryDoor));

        //return monster count
        return 0;
    }

    [ContextMenu("Open Door")]
    public void OpenDoor()
    {
        if(mainDoor!=null)
            StartCoroutine(DoOpenDoor(mainDoor));
    }

    public void OpenBonus()
    {
        if (bonusDoor == null)
            return;

        //TODO qualify requirements

        StartCoroutine(DoOpenDoor(bonusDoor));
    }

    IEnumerator DoOpenDoor(SlideAnimation door)
    {
        float t = 0;
        doorOpenSound.PlayOneShot(1, door.transform.position);
        float duration = doorOpenSound.length - doorMoveFinishOffset;
        while (t < duration)
        {
            door.percent = t/duration; //Normalize t to 0-1
            t += Time.deltaTime;
            yield return null;
        }
        door.percent = 1;
    }



    IEnumerator DoCloseDoor(SlideAnimation door)
    {
        float t = 1;
        float duration = doorOpenSound.length - doorMoveFinishOffset;
        while (t > duration)
        {
            door.percent = t/duration; //Normalize t to 0-1
            t -= Time.deltaTime * doorOpenSound.length;
            yield return null;
        }
        door.percent = 0;
    }
}
