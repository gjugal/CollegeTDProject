using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]

public class Bomb : Projectiles
{ 
    Rigidbody rb;
    bool speedSet = false;
    LinkedList<Transform> targets = null;


    protected override void Start()
    {
        targets = new LinkedList<Transform>();
        rb = GetComponent<Rigidbody>();
        collisionMask = 9;
        pathMask = 8;
        speed = 2;
        damage = 1;

    }


    void Update()
    {
        if (!speedSet) {//set speed only once at beginning after SetSpeed()
            rb.velocity = transform.forward * speed;
            speedSet = true;
        }
    }

    void OnTriggerEnter(Collider col) {//just add to last of linkedlist
        if (col.gameObject.layer == collisionMask)
        {
            targets.AddLast(col.gameObject.transform);
        }
    }

    void OnTriggerExit(Collider col) {//remove from linkedlist
        if (col.gameObject.layer == collisionMask)
        {
            targets.Remove(col.gameObject.transform);
        }
    }

    void OnCollisionEnter(Collision col) {
        //Debug.Log("oncollision");
        if (col.gameObject.layer == collisionMask || col.gameObject.layer == pathMask)//check if it is colliding with the soldier and not its censor AND destroy bomb
        {
            //Debug.Log("condition ttrue");
            Object bombBlast = Instantiate(blastEffect, this.transform.position, this.transform.rotation);
            Destroy(bombBlast, 1);
            GameObject.Destroy(this.gameObject);
            OnHitObject();
        }
    }

    private void OnHitObject() {//just give damage to all towers in LinkedList
        //Debug.Log("on hit called");
        foreach (Transform t in targets)
        {
            try
            {
                IDamagable damagableObject = t.GetComponent<IDamagable>();
                if (damagableObject != null)
                {
                    damagableObject.TakeDamage(damage);
                }
            }
            catch { }
        }
        //GameObject.Destroy(this.gameObject);
    }

}
