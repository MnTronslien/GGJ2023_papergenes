using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public TMPro.TextMeshProUGUI header;
    public TMPro.TextMeshProUGUI hint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickHead()
    {
        Debug.Log("Clicked Head");
    }

    public void ClickLeftArm()
    {
        Debug.Log("Clicked Left Arm");
    }

    public void ClickRightArm()
    {
        Debug.Log("Clicked Right Arm");
    }

    public void ClickTorso()
    {
        Debug.Log("Clicked Torso");
    }

    public void ClickLegs()
    {
        Debug.Log("Clicked Legs");
    }
}
