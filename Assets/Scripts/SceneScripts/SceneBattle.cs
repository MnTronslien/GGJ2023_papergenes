using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBattle : GameScene
{
    public SlideAnimation door;
    public float doorSpeed;

    private void Start()
    {
        StartCoroutine(OpenDoor()); 
    }

    IEnumerator OpenDoor()
    {
        float t = 0;
        while (t < 1)
        {
            door.precent = t;
            t += Time.deltaTime * doorSpeed;
            yield return null;
        }
    }
}
