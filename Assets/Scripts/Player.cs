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

    [Header("Stats")]
    public float currentHealth;
    public int maxHealth { get { return 100; } } //Get from body parts

    public Genome genome;

    private float dir;
    private Vector3 lastDir;
    private float lastX;
    private bool isDashing;

    public static UnityEngine.Events.UnityAction<float> onDamage;

    // Start is called before the first frame update
    void Start()
    {
        dir = 1;
        currentHealth = maxHealth;
        onDamage?.Invoke(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
            agent.SetDestination(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            animator.transform.rotation = Quaternion.identity;

            var d = (lastX - transform.position.x) * 10f;
            if (Mathf.Abs(d) > turnThreshold)
                dir = d >= 0 ? -1 : 1;

            if (Input.GetButtonDown("Fire1")){
                //TODO: Move instantiation to start, this is just for debugging if they work
                GameObject go = Instantiate(genome.LeftArmGene, transform.position, Quaternion.identity);
                go.GetComponent<GeneExpression>().Act();
                Destroy(go);

            }

                
            if (Input.GetButtonDown("Fire2"))
                GetHit(5);
            if (Input.GetButtonDown("Legs"))
                Dash(transform.position - lastDir);
            if (Input.GetButtonDown("Head"))
                Special();
        }

        transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, dir, Time.deltaTime * agent.angularSpeed), 1, 1);

        animator.SetFloat("Speed", Mathf.Abs(lastX - transform.position.x) * 10f);
        lastX = transform.position.x;
        lastDir = transform.position;

        if(currentHealth > 0 && currentHealth < maxHealth)
        {
            currentHealth += Time.deltaTime;
        }
    }

    private void GetHit(int damage)
    {
        animator.SetTrigger("Hit");
        hitSound.PlaySoundEffect(1 , transform.position);

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            onDamage?.Invoke(0);
        } else
        {
            onDamage?.Invoke((float)currentHealth / (float)maxHealth);
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
