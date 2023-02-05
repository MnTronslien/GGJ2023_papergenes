using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEggs : GameScene
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Intro());
    }

    // Update is called once per frame
    IEnumerator Intro()
    {
        yield return new WaitForSeconds(4);

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }
}
