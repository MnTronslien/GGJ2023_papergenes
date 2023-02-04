using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public SlideAnimation entryDoor;
    public SlideAnimation mainDoor;
    public SlideAnimation bonusDoor;

    public float doorSpeed;

    //TODO bonus door requirements

    public int Init()
    {
        if(entryDoor != null)
            StartCoroutine(DoCloseDoor(entryDoor));

        //return monster count
        return 0;
    }

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
        while (t < 1)
        {
            door.precent = t;
            t += Time.deltaTime * doorSpeed;
            yield return null;
        }
        door.precent = 1;
    }



    IEnumerator DoCloseDoor(SlideAnimation door)
    {
        float t = 1;
        while (t > 0)
        {
            door.precent = t;
            t -= Time.deltaTime * doorSpeed;
            yield return null;
        }
        door.precent = 0;
    }
}
