using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour,IDamagable {

    protected float health;
    float speed;
    bool dead;
    public delegate void DeathOccurence(Transform t);
    public event DeathOccurence OnDeath;
	// Use this for initialization
	protected virtual void Start () {
        dead = false;
	}


    public void TakeDamage(float damage)//reduce health according to damage and check if the entity is still alive
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
            OnDeath(this.transform);//call all method registered to this event of all objects and pass this entity's transform as parameter 
        }
        //GameObject.Destroy(gameObject);//base class gameobject 
        //Debug.Log("Destroyed called");
        GameObject.Destroy(gameObject);
    }

}
