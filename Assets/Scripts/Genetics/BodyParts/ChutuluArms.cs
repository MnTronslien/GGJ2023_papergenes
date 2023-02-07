//new monobehavior inheriting from GeneExpression

using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ChutuluArms : Arm
{
    

    public GameObject hitboxEffect;
    public SoundEffect particleSound;
    public SoundEffect attackSound;

    public float width = 0.2f; 

    public override async Task Act(Vector3 aimDirection)
    {
        attackSound.PlaySoundEffect(1, transform.position);
        particleSound.PlaySoundEffect(1, transform.position);
        //Spawn a hitbox infront of the player in the aim direction
       
        //calculate how many hitboxes to spawn based on the total lenght and te width of the hitbox
        int hitboxCount = Mathf.CeilToInt(length / width);
        //Make list to put anything hit by the hitboxes in
        List<Collider> hitObjects = new List<Collider>();
        //For each hitbox to spawn, spawn them in a row from the player in the direction the player is aiming
        for (int i = 0; i < hitboxCount; i++)
        {
            //Calculate the position of the hitbox
            Vector3 hitboxPosition = transform.position + aimDirection.normalized * (i * width);
            //Check for colliders in the hitbox
            Collider[] hitColliders = Physics.OverlapBox(hitboxPosition,Vector3.one * width / 2, Quaternion.LookRotation(aimDirection));
            //For each collider in the hitbox, check if it is already in the list of hit objects
            foreach (Collider c in hitColliders)
            {
                if (!hitObjects.Contains(c))
                {
                    hitObjects.Add(c);
                }
            }

            //Spawn a partickle effect along the hitbox
            Instantiate(hitboxEffect, hitboxPosition, Quaternion.LookRotation(aimDirection));

            foreach (Collider c in hitObjects)
            {
                //If the object is an enemy, deal damage to it
                if (c.GetComponent<Health>())
                {
                    c.GetComponent<Health>().TakeDamage(damage);
                }
            }
        }
     
    //Await atatck cooldown
    await Task.Delay((int)(attackCooldown * 1000));
    }
}
