using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomOnAwake : MonoBehaviour
{
    public GameObject[] objectsToRandomize;

    // Start is called before the first frame update
    void Awake()
    {
        int r = Random.Range(0, objectsToRandomize.Length);
        for(int i = 0; i < objectsToRandomize.Length; i++)
        {
            objectsToRandomize[i].SetActive(i == r);
        }
    }
}
