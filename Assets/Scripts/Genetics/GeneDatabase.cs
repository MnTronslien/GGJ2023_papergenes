//Create a scriptable object to act as a database for genes

using UnityEngine;


//Create the scriptable object from the create menu
[CreateAssetMenu(fileName = "GeneDatabase", menuName = "ScriptableObjects/GeneDatabase")]
public class GeneDatabase : ScriptableObject {

    public GameObject[] BodyGenes,
    HeadGenes,
    LegsGenes,
    LeftArmGenes,
    RightArmGenes;

    public static GameObject RandomBodyGene() {
        return GeneDatabase.instance.BodyGenes[Random.Range(0, GeneDatabase.instance.BodyGenes.Length)];
    }

    public static GameObject RandomHeadGene() {
        return GeneDatabase.instance.HeadGenes[Random.Range(0, GeneDatabase.instance.HeadGenes.Length)];
    }

    public static GameObject RandomLegsGene() {
        return GeneDatabase.instance.LegsGenes[Random.Range(0, GeneDatabase.instance.LegsGenes.Length)];
    }

    public static GameObject RandomLeftArmGene() {
        return GeneDatabase.instance.LeftArmGenes[Random.Range(0, GeneDatabase.instance.LeftArmGenes.Length)];
    }

    public static GameObject RandomRightArmGene() {
        return GeneDatabase.instance.RightArmGenes[Random.Range(0, GeneDatabase.instance.RightArmGenes.Length)];
    }

    public static GeneDatabase instance {
        get {
            return Resources.Load("GeneDatabase") as GeneDatabase;
        }
    }

}
