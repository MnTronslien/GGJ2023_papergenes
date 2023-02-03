using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    public NavMeshAgent agent { get { if (_agent == null) _agent = GetComponent<NavMeshAgent>(); return _agent; } }

    public float turnThreshold;

    private float lastX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        transform.rotation = Quaternion.identity;

        var dir = lastX -  transform.position.x;
        transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, Mathf.Abs(dir) > turnThreshold && dir > 0 ? -1 : 1, Time.deltaTime * agent.angularSpeed), 1, 1);
        lastX = transform.position.x;
    }

    private void LateUpdate()
    {
        
    }
}
