using UnityEngine;
using System.Collections;

public class GroundBarricade : Entity {

    public float groundbarricadeHealth = 5f;

    float timeBetweenShoots = 1;
    float lastShootTime = 0;
    float damage = 1;
    Color originalColor;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        myFirstName = "GroundBarricade";
        entityLL = new System.Collections.Generic.LinkedList<MyTargets>();
        health = groundbarricadeHealth;
        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    void OnTriggerEnter(Collider col) {
        Debug.Log("Collider triggered");
        if (col.tag == "Soldier" || col.tag == "King")
        {
            Entity entity = col.gameObject.GetComponent<Entity>();
            entity.OnDeath += RemoveEntity;
            entityLL.AddLast(new MyTargets(col.transform, true, entity.myFirstName));
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Soldier" || col.tag == "King")
        {
            entityLL.Remove(FindFromTargets(col.transform));
        }
    }

    void RemoveEntity(Transform t) {
        entityLL.Remove(FindFromTargets(t));
    }

	// Update is called once per frame
	void Update () {
        if(entityLL.Count > 0) {
            //Debug.Log("Total targets = " + entityLL.Count);
            if (Time.time > lastShootTime + timeBetweenShoots)
            {
                lastShootTime = Time.time;
                //Shoot();
                this.gameObject.GetComponent<Renderer>().material.color = Color.black;
                foreach (MyTargets t in entityLL)
                {
                    try
                    {
                        IDamagable damagableObject = t.GetTransfrom().GetComponent<IDamagable>();
                        if (damagableObject != null)
                        {
                            damagableObject.TakeDamage(damage);
                        }
                    }
                    catch { }
                }
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = originalColor;
            }
            
        }
	}
}
