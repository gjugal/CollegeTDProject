using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]

public class Bomb : Projectiles
{
    //public GameObject explodeParticles;
    public float blastRadius = 1.2f;
    Rigidbody rb;
    bool speedSet = false;
    //LinkedList<Transform> targets = null;
    LayerMask pathMask, defenseMask;
    bool explode = false;


    protected override void Start()
    {
       // targets = new LinkedList<Transform>();
        rb = GetComponent<Rigidbody>();
        pathMask = 8;
        collisionMask = 9;
        defenseMask = 11;
        damage = 2;

    }


    void Update()
    {
        if (!speedSet) {//set speed only once at beginning after SetSpeed()
            rb.velocity = transform.forward * speed;
            speedSet = true;
        }
    }

    void FixedUpdate()
    {
        if(explode)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, blastRadius);
            foreach(Collider col in hitColliders)
            {
                if (col.gameObject.layer == collisionMask)
                {
                    IDamagable damagableObject = col.gameObject.GetComponent<IDamagable>();
                    if (damagableObject != null)
                    {
                        damagableObject.TakeDamage(damage);
                    }
                }
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.layer == collisionMask || col.gameObject.layer == pathMask)//check if it is colliding with the soldier and not its censor AND destroy bomb
        {
            explode = true;
        }
    }

}
