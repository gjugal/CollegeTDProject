using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Bomb : MonoBehaviour {

    public LayerMask collisionMask;
    float damage = 1;
    float speed = 5f;
    
    LinkedList<Transform> targets = null;


    void Start()
    {
        Destroy(gameObject, 1);
        targets = new LinkedList<Transform>();
        //Debug.Log("");
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    void Update()
    {
        float moveDistance = this.speed * Time.deltaTime;
        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void OnTriggerEnter(Collider col) {
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

    void OnTriggerExit(Collider col) {
        //if (col.gameObject.layer == collisionMask)
        //{
        //    targets.Remove(col.gameObject);
        //}
        targets.Remove(col.gameObject.transform);
    }
    void CheckCollision(float distance)
    {
        //Debug.Log("checkCollisionEnter");
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, collisionMask, QueryTriggerInteraction.Collide))
        {
            //Debug.Log("collision detected");
            OnHitObject();
        }
    }

    private void OnHitObject() {
        try
        {
            foreach (Transform t in targets)
            {
                IDamagable damagableObject = t.GetComponent<IDamagable>();
                if (damagableObject != null)
                {
                    damagableObject.TakeDamage(damage);
                }
            }
        }
        finally {
            GameObject.Destroy(this.gameObject);
        }
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
