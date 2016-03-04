﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]

public class Bomb : Projectiles
{
    public float blastRadius = 1.2f;
    Rigidbody rb;
    bool speedSet = false;
    LayerMask pathMask;
    bool explode = false;


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        pathMask = 8;
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