using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBattle : GameScene
{
    public Room[] rooms;

    [Header("pr")]
    public Room room;
    public int enemyCount;

    private bool hasEnded;

    private void Start()
    {
        int r = Random.Range(0, rooms.Length);
        room = Instantiate(rooms[r]);
        enemyCount = room.Init();
        hasEnded = false;

        Player.onDamage += OnPlayerHurt;
    }

    private void OnDestroy()
    {
        Player.onDamage -= OnPlayerHurt;
    }

    private void Update()
    {
        if(enemyCount <= 0 && !hasEnded)
        {
            hasEnded = true;

            room.OpenDoor();
            room.OpenBonus();
        }
    }

    void OnPlayerHurt(float percent)
    {
        if(percent <= 0)
        {
            //TODO Death animation first
            GotoScene(7);
        }
    }
}
