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
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.Head);
    }

    public void ClickLeftArm()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.LeftArm);
    }

    public void ClickRightArm()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.RightArm);
    }

    public void ClickTorso()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.Body);
    }

    public void ClickLegs()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.Legs);
    }
}
