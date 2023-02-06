using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Arm : GeneExpressionFlippable
{
    public float length = 5;
    
    public int damage = 1;
    public float attckCooldown = 0.5f;

    public GameObject hitboxEffect;
    public SoundEffect particleSound;
    public SoundEffect attackSound;

    //Default arm behaviour copied rom ape behaviour
     override public async Task Act(Vector3 aimDirection)
    {

        attackSound.PlaySoundEffect(1, transform.position);
        particleSound.PlaySoundEffect(1, transform.position);

        //Calculate a position for the hitbox half a hitbox length infront of the player
        Vector3 hitboxPosition = transform.position + aimDirection.normalized * (length / 2);
        
        //Spawn a hitbox infront of the player in the aim direction
        Collider[] hitColliders = Physics.OverlapBox(
            hitboxPosition, Vector3.one * length / 2, Quaternion.LookRotation(aimDirection), isPlayer ? LayerMask.GetMask("Enemy") : LayerMask.GetMask("Player"));

            //For each collider in the hitbox, check if it has helath and deal damage to it
            foreach (Collider c in hitColliders)
            {
                if (c.GetComponent<Health>())
                {
                    c.GetComponent<Health>().TakeDamage(damage);
                }
                //if player deal damage to the player
                if (c.GetComponent<Player>())
                {
                    c.GetComponent<Player>().GetHit(damage);
                }
            }

        //Spawn a particle effect along the hitbox
        Instantiate(hitboxEffect, hitboxPosition, Quaternion.LookRotation(aimDirection));
        //Draw the hitbox to the screen
        
        await Task.Delay((int)(attckCooldown * 1000));
    }
}
