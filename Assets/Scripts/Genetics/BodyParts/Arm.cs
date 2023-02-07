using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.AI;

public class Arm : GeneExpressionFlippable
{
    public float length = 5;
    
    public int damage = 1;
    public float knockbackForce = 5;
    public float attackCooldown = 0.5f;

    public GameObject hitboxEffect;
    public SoundEffect particleSound;
    public SoundEffect attackSound;

    private bool drawHitbox { 
        get { return drawTimer > 0; }
        set{
            if (value)
                drawTimer = 0.2f;
        }
    }   

    private float drawTimer = 0.2f;
    private Vector3 hitBoxCentre;
    private Quaternion aimDirectionStored;

    public Logger logger;

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
                //Find direction between gameobject and target
                Vector3 knockbackDir = c.transform.position - transform.position;
                Knockback(c.gameObject, knockbackDir, knockbackForce);
            }
            //if player deal damage to the player
            if (c.GetComponent<Player>())
            {
                c.GetComponent<Player>().GetHit(damage);
            }
        }

        StoreDrawGizmoInfo(aimDirection, hitboxPosition);


        //Spawn a particle effect along the hitbox
        Instantiate(hitboxEffect, hitboxPosition, Quaternion.LookRotation(aimDirection));
        //Draw the hitbox to the screen

        await Task.Delay((int)(attackCooldown * 1000));
    }

    private void StoreDrawGizmoInfo(Vector3 aimDirection, Vector3 hitboxPosition)
    {
        //Store hitbox information for the Gizmos
        hitBoxCentre = hitboxPosition;
        aimDirectionStored = Quaternion.LookRotation( aimDirection);
        drawHitbox = true;
    }

    //knockback
    public async Task Knockback(GameObject target, Vector3 direction, float force)
    {
        //Simulate knockback by moving the nav mesh agent of the target in the direction of the knockback
        //If the target does not have a Nav Mesh Agent, then move the position directly.

        if (target.GetComponent<NavMeshAgent>())
        {
            target.GetComponent<NavMeshAgent>().velocity = direction.normalized * force;
        }
        else
        {
            target.transform.position += direction.normalized * force;
        }
    }

    public void Update()
    {
        if (drawHitbox) drawTimer -= Time.deltaTime;
    }
    void OnDrawGizmos()
    {
        if (!drawHitbox) return;
        //Valdidate parameters

try
        {
        //Draw the hitbox to the screen
        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(hitBoxCentre, aimDirectionStored, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * length);

        }
        catch (System.Exception e)
        {
            logger.LogWarning("Error drawing hitbox: " + e.Message);
        }

    }
}
