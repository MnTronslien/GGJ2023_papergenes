using UnityEngine;

public class Monster : MonoBehaviour
{
    public void OnDestroy()
    {
        //Destroy the monster
        //Find room script and tell it that we died
        FindAnyObjectByType<Room>().enemyCount--;
    }
}