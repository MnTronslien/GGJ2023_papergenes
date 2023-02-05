using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInfo : MonoBehaviour
{
    public class Parentage
    {
        public Genome Alpha;    //Player
        public Genome Beta;     //Enemy

        public Parentage(Genome A, Genome B)
        {
            Alpha = A;
            Beta = B;
        }
    }

    public static GlobalInfo instance;

    // STATIC VARS
    public static float currentHealth;
    public static bool canWalk;
    public static bool canAttack;

    public static List<Parentage> ancestry;
    public static Genome playerGenome;

    // SETTINGS
    public Genome startingGenome;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
    }

    public static void Offspring(Genome partner)
    {
        if (ancestry == null)
            ancestry = new List<Parentage>();
        ancestry.Add(new Parentage(playerGenome, partner));

        playerGenome = Genome.Combine(playerGenome, partner);
    }
}
