using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBabymaker : GameScene
{
    public Transform A, B;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.ApplyMusicTrackPreset("Merge", 4);

        GlobalInfo.Parentage p = GlobalInfo.ancestry[GlobalInfo.ancestry.Count-1];
        CreateCharacter(p.Alpha, A);    
        CreateCharacter(p.Beta, B);

        
    }

    // Update is called once per frame
    void CreateCharacter(Genome genome, Transform container)
    {
        var legs = Instantiate(genome.LegsGene, container, false);
        var torso = Instantiate(genome.BodyGene, legs.torsoPos.position, Quaternion.identity, legs.transform);
        var head = Instantiate(genome.HeadGene, torso.Head.position, Quaternion.identity, torso.transform);
        var leftArm = Instantiate(genome.LeftArmGene, torso.FrontArm.position, Quaternion.identity, torso.transform);
        var rightArm = Instantiate(genome.RightArmGene, torso.BackArm.position, Quaternion.identity, torso.transform);

        leftArm.back.SetActive(false);
        rightArm.front.SetActive(false);

        legs.isPlayer = true;
        torso.isPlayer = true;
        head.isPlayer = true;
        leftArm.isPlayer = true;
        rightArm.isPlayer = true;

        legs.name = "Legs";
        torso.name = "Torso";
        head.name = "Head";
        leftArm.name = "Front";
        rightArm.name = "Back";
    }
}
