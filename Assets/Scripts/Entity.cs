using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour,IDamagable {

    protected float health;
    float speed;
    bool dead;
    public event System.Action OnDeath;
	// Use this for initialization
	protected virtual void Start () {
        dead = false;
	}


    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected void Die()
    {
        dead = true;
        if (OnDeath != null)
        {
            OnDeath();
        }
        GameObject.Destroy(gameObject);
    }
}
