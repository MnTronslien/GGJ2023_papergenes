using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTutorial : GameScene
{
    public Room room;

    private int enemyCount;
    private bool hasEnded;

    // Start is called before the first frame update
    void Start()
    {
        enemyCount = room.Init();
    }

    private void Update()
    {
        if (enemyCount <= 0 && !hasEnded)
        {
            hasEnded = true;

            room.OpenDoor();
            room.OpenBonus();
        }
    }
}
