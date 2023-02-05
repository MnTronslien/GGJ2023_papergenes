using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    public Genome genome;
    public GameObject player;

    public NavMeshAgent agent;

    public Animator animator;


    
    Leg legs;
    Torso torso;
    GeneExpression head;
    Arm leftArm;
    Arm rightArm;

    //Maximum attack didtance is calculated from the longest reach of both arms
    private float MaxAttackDistance => Mathf.Max(genome.LeftArmGene.length, genome.RightArmGene.length);
    private float MinAttackDistance => Mathf.Min(genome.LeftArmGene.length, genome.RightArmGene.length);
    
    void Start()
    {


        NameMonster();
//Get the player
        player = FindAnyObjectByType<Player>().gameObject;
        //TODO: Spawn Body from geneome

        if(genome.BodyGene == null)
        {
            genome = Genome.CreateRandomGenome();
        }

        if(animator != null)
        {
            legs = Instantiate(GlobalInfo.playerGenome.LegsGene, animator.transform, false);
            torso = Instantiate(GlobalInfo.playerGenome.BodyGene, legs.torsoPos.position, Quaternion.identity, legs.transform);
            head = Instantiate(GlobalInfo.playerGenome.HeadGene, torso.Head.position, Quaternion.identity, torso.transform);
            leftArm = Instantiate(GlobalInfo.playerGenome.LeftArmGene, torso.FrontArm.position, Quaternion.identity, torso.transform);
            rightArm = Instantiate(GlobalInfo.playerGenome.RightArmGene, torso.BackArm.position, Quaternion.identity, torso.transform);

            leftArm.back.SetActive(false);
            rightArm.front.SetActive(false);

            legs.name = "Legs";
            torso.name = "Torso";
            head.name = "Head";
            leftArm.name = "Front";
            rightArm.name = "Back";
        }
        
    }

    private void NameMonster()
    {
        //Generate a list of 20 random two syllable names as an array
        List<string> monsterNames = new List<string>
{
    "Blargoth",
    "Gruumsh",
    "Chomper",
    "Gribble",
    "Snarler",
    "Thrakgar",
    "Zogar",
    "Razorclaw",
    "Gornash",
    "Sneer",
    "Bristleback",
    "Kronk",
    "Gargantua",
    "Hisser",
    "Krall",
    "Fangrider",
    "Goregash",
    "Sludgebeast",
    "Howler",
    "Rampager"
};
name = monsterNames[UnityEngine.Random.Range(0, monsterNames.Count)];
    }

    async void Update()
    {
        if (agent == null)
            return;

        //Move towards the player and wait until we are close enough to attack
        agent.SetDestination(player.transform.position);
        while (Vector3.Distance(transform.position, player.transform.position) > MaxAttackDistance)
        {
            await Task.Delay(100);
            if (!Application.isPlaying) return; 
            if(this == null) return;   
        }
    //STop moving
        agent.isStopped = true;

        //Calculate aim direction
        Vector3 aimDir = player.transform.position - transform.position;


        //Now that you are close enough, attack with the longest ranged attack
        if (leftArm.length > rightArm.length)
        {
           await leftArm.Act(aimDir);
        }
        else
        {
           await rightArm.Act(aimDir);
        }
        //Await a short delay
        await Task.Delay(1500);
        //Break if not in play mode
        if (!Application.isPlaying) return;
        if (this == null) return;
        //If the minimum attack is also in range, then attack with that too
        if (Vector3.Distance(transform.position, player.transform.position) < MinAttackDistance)
        {
            if (leftArm.length < rightArm.length)
            {
                await leftArm.Act(aimDir);
            }
            else
            {
                await rightArm.Act(aimDir);
            }
        }
        await Task.Delay(1500);
        //Break if not in play mode
        if (!Application.isPlaying) return;
        if (this == null) return;

    }
    public void OnDestroy()
    {

        //Destroy the monster
        //Find room script and tell it that we died
        var room = FindAnyObjectByType<Room>();
        //If room exist then tell it that we died
        if (room != null)
        {
            room.enemyCount--;
        }
    }
}