//Create a health script

using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = value;
            if (_currentHealth > maxHealth)
                _currentHealth = maxHealth;
            if (_currentHealth <= 0)
            {

                Die();
            }
        }
    }

    public SoundEffect DeathSound;
    private float _currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    //Kill
    public void Die()
    {
        DeathSound.PlaySoundEffect(1, transform.position);
        Destroy(gameObject);
    }

    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}