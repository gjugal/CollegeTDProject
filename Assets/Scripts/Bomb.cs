using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(Rigidbody))]
public class Bomb : MonoBehaviour {

    public LayerMask collisionMask;
    float damage = 1;
    float speed ;
    Rigidbody rb;
    bool speedSet = false;
    
    LinkedList<Transform> targets = null;


    void Start()
    {
        Destroy(gameObject, 3);
        targets = new LinkedList<Transform>();
        rb = GetComponent<Rigidbody>();
        
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
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
        if (col.gameObject.tag == "Soldier" || col.gameObject.tag == "King")//check if it is colliding with the soldier and not its censor AND destroy bomb
        {
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
