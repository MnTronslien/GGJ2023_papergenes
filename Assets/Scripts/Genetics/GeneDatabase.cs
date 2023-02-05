//Create a scriptable object to act as a database for genes

using UnityEngine;


//Create the scriptable object from the create menu
[CreateAssetMenu(fileName = "GeneDatabase", menuName = "ScriptableObjects/GeneDatabase")]
public class GeneDatabase : ScriptableObject {

    public GeneExpression[] BodyGenes,
    HeadGenes,
    LegsGenes,
    ArmGenes;

    public static GeneExpression RandomBodyGene() {
        return GeneDatabase.instance.BodyGenes[Random.Range(0, GeneDatabase.instance.BodyGenes.Length)];
    }

    public static GeneExpression RandomHeadGene() {
        return GeneDatabase.instance.HeadGenes[Random.Range(0, GeneDatabase.instance.HeadGenes.Length)];
    }

    public static GeneExpression RandomLegsGene() {
        return GeneDatabase.instance.LegsGenes[Random.Range(0, GeneDatabase.instance.LegsGenes.Length)];
    }

    public static GeneExpression RandomArmGene() {
        return GeneDatabase.instance.ArmGenes[Random.Range(0, GeneDatabase.instance.ArmGenes.Length)];
    }

    public static GeneDatabase instance {
        get {
            return Resources.Load("GeneDatabase") as GeneDatabase;
        }
    }

}
