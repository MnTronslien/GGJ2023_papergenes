using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    public NavMeshAgent agent { get { if (_agent == null) _agent = GetComponent<NavMeshAgent>(); return _agent; } }

    public Animator animator;
    public float turnThreshold;
    public SoundEffect hitSound;

    private float dir;
    private Vector3 lastDir;
    private float lastX;
    private bool isDashing;
    private bool isDancing;

    public static UnityEngine.Events.UnityAction<float> onDamage;

    // Start is called before the first frame update
    void Start()
    {
        dir = 1;        
       
        if(GlobalInfo.playerGenome == null)
        {
            GlobalInfo.playerGenome = GlobalInfo.instance.startingGenome;
            GlobalInfo.currentHealth = GlobalInfo.instance.startingGenome.GetMaxHealth();
        }

        Leg l = Instantiate(GlobalInfo.playerGenome.LegsGene, animator.transform, false);
        Torso t = Instantiate(GlobalInfo.playerGenome.BodyGene, l.torsoPos.position, Quaternion.identity, animator.transform);
        GeneExpression h = Instantiate(GlobalInfo.playerGenome.HeadGene, t.Head.position, Quaternion.identity, animator.transform);
        GeneExpressionFlippable f = Instantiate(GlobalInfo.playerGenome.LeftArmGene, t.FrontArm.position, Quaternion.identity, animator.transform);
        GeneExpressionFlippable b = Instantiate(GlobalInfo.playerGenome.RightArmGene, t.BackArm.position, Quaternion.identity, animator.transform);

        f.back.SetActive(false);
        b.front.SetActive(false);

        onDamage?.Invoke((float)GlobalInfo.currentHealth / (float)GlobalInfo.instance.startingGenome.GetMaxHealth());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            if(GlobalInfo.canWalk)
            {
                agent.SetDestination(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            }
            
            animator.transform.rotation = Quaternion.identity;

            var d = (lastX - transform.position.x) * 10f;
            if (Mathf.Abs(d) > turnThreshold)
                dir = d >= 0 ? -1 : 1;

            HandleBreeding();

            if(!isDancing)
            {
                if (Input.GetButtonDown("Fire1") && GlobalInfo.playerGenome.LeftArmGene != null)
                {
                    //TODO: Move instantiation to start, this is just for debugging if they work
                    GeneExpression go = Instantiate(GlobalInfo.playerGenome.LeftArmGene, transform.position, Quaternion.identity);
                    go.Act();
                    Destroy(go);
                }

                if (Input.GetButtonDown("Fire2") && GlobalInfo.playerGenome.RightArmGene != null)
                {
                    GeneExpression go = Instantiate(GlobalInfo.playerGenome.RightArmGene, transform.position, Quaternion.identity);
                    go.Act();
                    Destroy(go);
                }

                if (Input.GetButtonDown("Legs"))
                    Dash(transform.position - lastDir);

                if (Input.GetButtonDown("Head"))
                    Special();
            }
            
            if (Input.GetKeyDown(KeyCode.P))
                GetHit(5);
        }

        transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, dir, Time.deltaTime * agent.angularSpeed), 1, 1);        

        animator.SetFloat("Speed", Mathf.Abs(lastX - transform.position.x) * 10f);
        lastX = transform.position.x;
        lastDir = transform.position;

        if(GlobalInfo.currentHealth > 0 && GlobalInfo.currentHealth < GlobalInfo.instance.startingGenome.GetMaxHealth())
        {
            GlobalInfo.currentHealth += Time.deltaTime;
        }
    }

    private void HandleBreeding()
    {
        isDancing = Input.GetKey(KeyCode.B);
        animator.SetBool("dance", isDancing);
        if (isDancing)
        {
            //Find enemy, if there are more than one it fails
            //increase enemy.charm
            //if charm is > enemy.charmResist
            GlobalInfo.Offspring(Genome.CreateRandomGenome()); //TODO replace with enemy
            UnityEngine.SceneManagement.SceneManager.LoadScene(6);
        }
    }

    private void GetHit(int damage)
    {
        animator.SetTrigger("Hit");
        hitSound.PlaySoundEffect(1 , transform.position);

        GlobalInfo.currentHealth -= damage;

        if(GlobalInfo.currentHealth <= 0)
        {
            onDamage?.Invoke(0);
        } else
        {
            onDamage?.Invoke((float)GlobalInfo.currentHealth / (float)GlobalInfo.instance.startingGenome.GetMaxHealth());
        }       
    }

    private void Dash(Vector3 dir)
    {
        
        StartCoroutine(DoDash(dir));
    }

    private void Special()
    {

    }

    private IEnumerator DoDash(Vector3 dir)
    {
        isDashing = true;
        float t = 0;
        while(t < 0.1f)
        {
            agent.Move(dir.normalized * 0.1f);
            t += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
    }
}
