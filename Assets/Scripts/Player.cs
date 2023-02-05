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

    Leg legs;
    Torso torso;
    GeneExpression head;
    Arm leftArm;
    Arm rightArm;




    public static UnityEngine.Events.UnityAction<float> onDamage;

    // Start is called before the first frame update
    void Awake()
    {
        dir = 1;

        if (GlobalInfo.playerGenome == null)
        {
            GlobalInfo.playerGenome = GlobalInfo.instance.startingGenome;
            GlobalInfo.currentHealth = GlobalInfo.instance.startingGenome.GetMaxHealth();
        }

        legs = Instantiate(GlobalInfo.playerGenome.LegsGene, animator.transform, false);
        torso = Instantiate(GlobalInfo.playerGenome.BodyGene, legs.torsoPos.position, Quaternion.identity, legs.transform);
        head = Instantiate(GlobalInfo.playerGenome.HeadGene, torso.Head.position, Quaternion.identity, torso.transform);
        leftArm = Instantiate(GlobalInfo.playerGenome.LeftArmGene, torso.FrontArm.position, Quaternion.identity, torso.transform);
        rightArm = Instantiate(GlobalInfo.playerGenome.RightArmGene, torso.BackArm.position, Quaternion.identity, torso.transform);

        legs.transform.localRotation = Quaternion.identity;
        torso.transform.localRotation = Quaternion.identity;
        head.transform.localRotation = Quaternion.identity;
        leftArm.transform.localRotation = Quaternion.identity;
        rightArm.transform.localRotation = Quaternion.identity;

        leftArm.back.SetActive(false);
        rightArm.front.SetActive(false);

        legs.isPlayer = true;
        torso.isPlayer = true;
        head.isPlayer = true;
        leftArm.isPlayer = true;
        rightArm.isPlayer = true;

        legs.name = "Legs";
        torso.name = "Torso";
        head.name = "Head";
        leftArm.name = "Front";
        rightArm.name = "Back";

        animator.gameObject.SetActive(false);
    }

    void Start()
    {
        animator.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 a = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);
        Vector3 b = new Vector3(transform.position.x, transform.position.y - Camera.main.transform.position.y, transform.position.z- Camera.main.transform.position.z);
        animator.transform.LookAt(Vector3.Lerp(a, b, GlobalInfo.instance.CharacterAngle));

        if (!isDashing)
        {
            if (GlobalInfo.canWalk)
            {
                agent.SetDestination(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            }

            //animator.transform.localRotation = Quaternion.identity;

            var d = (lastX - transform.position.x) * 10f;
            if (Mathf.Abs(d) > turnThreshold)
                dir = d >= 0 ? -1 : 1;

            HandleBreeding();

            if (!isDancing)
            {
                if (Input.GetButtonDown("Fire1") && GlobalInfo.playerGenome.LeftArmGene != null)
                {
                    //TODO: Move instantiation to start, this is just for debugging if they work

                    //Get the direction the player is aiming
                    Vector3 aimDirection = InputManager.Instance.CursorWorldPosition - transform.position;
                    leftArm.Act(aimDirection);

                }

                if (Input.GetButtonDown("Fire2") && GlobalInfo.playerGenome.RightArmGene != null)
                {
                    Vector3 aimDirection = InputManager.Instance.CursorWorldPosition - transform.position;
                    rightArm.Act(aimDirection);

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

        if (GlobalInfo.currentHealth > 0 && GlobalInfo.currentHealth < GlobalInfo.instance.startingGenome.GetMaxHealth())
        {
            GlobalInfo.currentHealth += Time.deltaTime;
        }
    }

    private void HandleBreeding()
    {
        isDancing = Input.GetKey(KeyCode.B) || (Input.GetButtonDown("Fire1") && Input.GetButtonDown("Fire2"));
        animator.SetBool("dance", isDancing);
        var enemies = FindObjectsOfType<Monster>();
        if (enemies.Length == 1)
        {
            if (isDancing)
            {
                enemies[0].charm += Time.deltaTime;
                if(enemies[0].charm >= enemies[0].charmTollerance)
                {
                    GlobalInfo.Offspring(enemies[0].genome);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(6);
                } 
            } else
            {
                enemies[0].charm = 0;
            }
        }
    }

    private void GetHit(int damage)
    {
        animator.SetTrigger("Hit");
        hitSound.PlaySoundEffect(1, transform.position);

        GlobalInfo.currentHealth -= damage;

        if (GlobalInfo.currentHealth <= 0)
        {
            onDamage?.Invoke(0);
        }
        else
        {
            Debug.Log($"C: {GlobalInfo.currentHealth} / {GlobalInfo.playerGenome.GetMaxHealth()}");
            onDamage?.Invoke((float)GlobalInfo.currentHealth / (float)GlobalInfo.playerGenome.GetMaxHealth()); 
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
        while (t < 0.1f)
        {
            agent.Move(dir.normalized * 0.1f);
            t += Time.deltaTime;
            yield return null;
        }
        isDashing = false;
    }
}
