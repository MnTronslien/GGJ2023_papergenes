using UnityEngine;

public class Monster : MonoBehaviour
{
    public Genome genome;
    public GameObject player;

    public Animator animator;

    //Maximum attack didtance is calculated from the longest reach of both arms
    private float MaxAttackDistance => Mathf.Max(genome.LeftArmGene.length, genome.RightArmGene.length);

    
    void Start()
    {
//Get the player
        player = FindAnyObjectByType<Player>().gameObject;
        //TODO: Spawn Body from geneome

         if(genome == null)
        {
            genome = Genome.CreateRandomGenome();
        }

        Leg l = Instantiate(GlobalInfo.playerGenome.LegsGene, animator.transform, false);
        Torso t = Instantiate(GlobalInfo.playerGenome.BodyGene, l.torsoPos.position, Quaternion.identity, l.transform);
        GeneExpression h = Instantiate(GlobalInfo.playerGenome.HeadGene, t.Head.position, Quaternion.identity, t.transform);
        GeneExpressionFlippable f = Instantiate(GlobalInfo.playerGenome.LeftArmGene, t.FrontArm.position, Quaternion.identity, t.transform);
        GeneExpressionFlippable b = Instantiate(GlobalInfo.playerGenome.RightArmGene, t.BackArm.position, Quaternion.identity, t.transform);

        f.back.SetActive(false);
        b.front.SetActive(false);

        l.name = "Legs";
        t.name = "Torso";
        h.name = "Head";
        f.name = "Front";
        b.name = "Back";
    }




    public void OnDestroy()
    {
        //Destroy the monster
        //Find room script and tell it that we died
        FindAnyObjectByType<Room>().enemyCount--;
    }
}