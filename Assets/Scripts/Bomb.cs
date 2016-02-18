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
        //Debug.Log("");
        
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    void Update()
    {
        //float moveDistance = this.speed * Time.deltaTime;
        //CheckCollision(moveDistance);
        //transform.Translate(Vector3.forward * moveDistance);
        //rb.AddRelativeForce(Vector3.forward * speed);
        //rb.velocity =transform.forward * speed;
        if (!speedSet) {//set speed only once at beginning after SetSpeed()
            rb.velocity = transform.forward * speed;
            speedSet = true;
        }
    }

    void OnTriggerEnter(Collider col) {//just add to last of linkedlist
        //Debug.Log("Collision");
        //if (col.gameObject.layer == collisionMask)
        //    {
        //        Debug.Log("added");
        //        targets.AddLast(col.gameObject);
        //    }
        //Debug.Log("added");
        //Debug.Log(col.gameObject);
        targets.AddLast(col.gameObject.transform);
    }

    void OnTriggerExit(Collider col) {//remove from linkedlist
        //if (col.gameObject.layer == collisionMask)
        //{
        //    targets.Remove(col.gameObject);
        //}
        targets.Remove(col.gameObject.transform);
    }
    /*void CheckCollision(float distance)
    {
        //Debug.Log("checkCollisionEnter");
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, collisionMask, QueryTriggerInteraction.Collide))
        {
            //Debug.Log("collision detected");
            OnHitObject();
        }
    }*/

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
    //private void OnHitObject(RaycastHit hit)
    //{
    //    //Debug.Log("onhit");
    //    IDamagable damagableObject = hit.collider.gameObject.GetComponent<IDamagable>();
    //    if (damagableObject != null)
    //    {
    //        damagableObject.TakeDamage(damage);
    //    }
    //    GameObject.Destroy(this.gameObject);
    //}

}
