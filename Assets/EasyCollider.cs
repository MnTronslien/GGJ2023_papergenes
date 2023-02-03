using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyCollider : MonoBehaviour
{
    public bool player;
    public bool enemies;
    public UnityEngine.Events.UnityEvent onCollide;

    private void OnCollisionEnter(Collision collision)
    {
        Collide(collision.collider.gameObject.layer);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collide(other.gameObject.layer);
    }

    void Collide(int layer)
    {
        if(player && layer == LayerMask.NameToLayer("Player"))
            onCollide?.Invoke();
        if (enemies && layer == LayerMask.NameToLayer("Enemy"))
            onCollide?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Collider c = GetComponent<Collider>();
        if(c != null)
        {
            var m = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
            Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
            Gizmos.matrix = m;
        }
    }
}
