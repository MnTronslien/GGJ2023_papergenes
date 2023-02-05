using System.Threading.Tasks;
using UnityEngine;

//This class holds the data for how a gene is expressed - aka what it does
public class GeneExpression : MonoBehaviour
{
    //Method for making this component doe what is does, abstract
    public virtual async Task Act(Vector3 direction) { }
public bool isPlayer = false;

    public float health;
}