using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrine : MonoBehaviour
{
    public float range; 
    public ParticleSystem pre;
    public CanvasGroup GUI;
    public ParticleSystem post;
    public Room room;
    public Cinemachine.CinemachineVirtualCamera camera;

    private Player player;
    private bool hasSet;
    private float guiA;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // Start is called before the first frame update
    void Start()
    {
        var e = pre.emission;
        e.enabled = true;

        var p = post.emission;
        p.enabled = false;

        GUI.alpha = 0;


        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(!hasSet)
        {
            guiA = Vector3.Distance(player.transform.position, transform.position) < range ? 1 : 0;

            var e = pre.emission;
            e.enabled = GUI.alpha < 0.4f;

            camera.Priority = GUI.alpha > 0.5f ? 999 : -1;
        }

        GUI.alpha = Mathf.Lerp(GUI.alpha, guiA, Time.deltaTime * 3.14f);
        GUI.interactable = GUI.alpha > 0.8f;
        GlobalInfo.canAttack = GUI.alpha < 0.4f;

        
    }

    void Set()
    {
        hasSet = true;
        guiA = 0;

        var e = pre.emission;
        e.enabled = false;

        var p = post.emission;
        p.enabled = true;

        room.OpenDoor();
        camera.Priority = -1;
    }

    public void ClickHead()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.Head);
        Set();
    }

    public void ClickLeftArm()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.LeftArm);
        Set();
    }

    public void ClickRightArm()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.RightArm);
        Set();
    }

    public void ClickTorso()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.Body);
        Set();
    }

    public void ClickLegs()
    {
        GlobalInfo.playerGenome.MarkAsRecessive(Genome.GeneType.Legs);
        Set();
    }
}
