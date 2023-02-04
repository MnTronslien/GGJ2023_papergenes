using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTutorial : GameScene
{
    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        room.Init();
        room.OpenDoor();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
