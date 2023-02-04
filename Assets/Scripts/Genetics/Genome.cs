//A class that represents the genes of a creature
//It is used to store the genes of a creature and to mutate them

using UnityEngine;

//Serializable so that it can be seen in the inspector
[System.Serializable]
public class Genome
{

    //Genome
    public GameObject
    BodyGene,
    HeadGene,
    LegsGene,
    LeftArmGene,
    RightArmGene;

    //Gene strenght with default as neutral
    private GeneStrength
    BodyGeneStrength = GeneStrength.Neutral,
    HeadGeneStrength = GeneStrength.Neutral,
    LegsGeneStrength = GeneStrength.Neutral,
    LeftArmGeneStrength = GeneStrength.Neutral,
    RightArmGeneStrength = GeneStrength.Neutral;

//Method for blending two geneomes togehther
//If a gene in the genome is marked as ressesive, the gene from the other genome will be used.
//If a gene in the genome is marked as dominant, the gene from this genome will be used.
//If a gene is marked as neutral, then pick a random gene from either genome.
public static Genome Combine(Genome genome1, Genome genome2)
{
    Genome newGenome = new Genome();
    newGenome.BodyGene = genome1.BodyGeneStrength == GeneStrength.Recessive ? genome2.BodyGene : genome1.BodyGeneStrength == GeneStrength.Dominant ? genome1.BodyGene : Random.Range(0, 2) == 0 ? genome1.BodyGene : genome2.BodyGene;
    newGenome.HeadGene = genome1.HeadGeneStrength == GeneStrength.Recessive ? genome2.HeadGene : genome1.HeadGeneStrength == GeneStrength.Dominant ? genome1.HeadGene : Random.Range(0, 2) == 0 ? genome1.HeadGene : genome2.HeadGene;
    newGenome.LegsGene = genome1.LegsGeneStrength == GeneStrength.Recessive ? genome2.LegsGene : genome1.LegsGeneStrength == GeneStrength.Dominant ? genome1.LegsGene : Random.Range(0, 2) == 0 ? genome1.LegsGene : genome2.LegsGene;
    newGenome.LeftArmGene = genome1.LeftArmGeneStrength == GeneStrength.Recessive ? genome2.LeftArmGene : genome1.LeftArmGeneStrength == GeneStrength.Dominant ? genome1.LeftArmGene : Random.Range(0, 2) == 0 ? genome1.LeftArmGene : genome2.LeftArmGene;
    newGenome.RightArmGene = genome1.RightArmGeneStrength == GeneStrength.Recessive ? genome2.RightArmGene : genome1.RightArmGeneStrength == GeneStrength.Dominant ? genome1.RightArmGene : Random.Range(0, 2) == 0 ? genome1.RightArmGene : genome2.RightArmGene;
    return newGenome;
}
    //Method for marking a gene as forced ressesive for this instance of the genome
    public void MarkAsRecessive(GeneType geneType)
    {
        switch (geneType)
        {
            case GeneType.Body:
                BodyGeneStrength = GeneStrength.Recessive;
                break;
            case GeneType.Head:
                HeadGeneStrength = GeneStrength.Recessive;
                break;
            case GeneType.Legs:
                LegsGeneStrength = GeneStrength.Recessive;
                break;
            case GeneType.LeftArm:
                LeftArmGeneStrength = GeneStrength.Recessive;
                break;
            case GeneType.RightArm:
                RightArmGeneStrength = GeneStrength.Recessive;
                break;
        }

    }
    public void MarkAsDominant(GeneType geneType)
    {
        switch (geneType)
        {
            case GeneType.Body:
                BodyGeneStrength = GeneStrength.Dominant;
                break;
            case GeneType.Head:
                HeadGeneStrength = GeneStrength.Dominant;
                break;
            case GeneType.Legs:
                LegsGeneStrength = GeneStrength.Dominant;
                break;
            case GeneType.LeftArm:
                LeftArmGeneStrength = GeneStrength.Dominant;
                break;
            case GeneType.RightArm:
                RightArmGeneStrength = GeneStrength.Dominant;
                break;
        }
    }

    public static Genome CreateRandomGenome()
    {
        Genome genome = new Genome();
        genome.BodyGene = GeneDatabase.RandomBodyGene();
        genome.HeadGene = GeneDatabase.RandomHeadGene();
        genome.LegsGene = GeneDatabase.RandomLegsGene();
        genome.LeftArmGene = GeneDatabase.RandomLeftArmGene();
        genome.RightArmGene = GeneDatabase.RandomRightArmGene();
        return genome;
    }
    private enum GeneStrength
    {
        Dominant,
        Neutral,
        Recessive
    }

    public enum GeneType
    {
        Body,
        Head,
        Legs,
        LeftArm,
        RightArm
    }
}