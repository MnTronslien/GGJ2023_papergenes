using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public enum Scene { Menu, Eggs, Tutorial, Battle, Loot, Map, Breed, Death  }

    public Cinemachine.CinemachineVirtualCamera camera;
    public Scene nextScene;

    private bool hasTriggered;

    public void Trigger()
    {
        if(!hasTriggered)
            StartCoroutine(DoExit());
    }

    IEnumerator DoExit()
    {
        hasTriggered = true;

        GlobalInfo.canAttack = false;
        GlobalInfo.canWalk = false;
        camera.Priority = 99;

        yield return new WaitForSeconds(2);

        UnityEngine.SceneManagement.SceneManager.LoadScene((int)nextScene);
    }
}
