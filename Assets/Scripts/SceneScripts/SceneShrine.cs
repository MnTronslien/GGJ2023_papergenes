using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneShrine : GameScene
{
    public Room room;
    public Shrine shrine;

    // Start is called before the first frame update
    void Start()
    {
        room.Init();
        AudioManager.Instance.ApplyMusicTrackPreset("Shrine");    
    }
}
