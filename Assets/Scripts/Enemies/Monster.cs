using UnityEngine;

public class Monster : MonoBehaviour
{
    public Genome genome;

    //Maximum attack didtance is calculated from the longest reach of both arms
    private float MaxAttackDistance => 6;

    //Start , get player reference
    void Start()
    {

        player = FindAnyObjectByType<Player>().gameObject;
    }




    public void OnDestroy()
    {
        //Destroy the monster
        //Find room script and tell it that we died
        FindAnyObjectByType<Room>().enemyCount--;
    }
}