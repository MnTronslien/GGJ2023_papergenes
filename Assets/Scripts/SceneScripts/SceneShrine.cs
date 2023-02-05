using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShrine : GameScene
{
    public Room room;

    // Start is called before the first frame update
    void Start()
    {
        room.Init();
        AudioManager.Instance.ApplyMusicTrackPreset("Shrine");    
    }

    // Update is called once per frame
    void Update()
    {
        //TODO wait til user has used shrine
        room.OpenDoor();
    }
}
