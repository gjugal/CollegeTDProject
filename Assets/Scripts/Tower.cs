using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {
    Queue<Transform> soldierQueue;
    Transform currentTarget;


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Soldier")
        {
            Entity soldierEntity = col.gameObject.GetComponent<Entity>();
            soldierEntity.OnDeath += SetTarget;

            if (soldierQueue.Count == 0)
            {
                currentTarget = col.gameObject.transform;
            }
            else
            {
                soldierQueue.Enqueue(col.gameObject.transform);
            }
        }
    }

    void SetTarget()
    {
        if (soldierQueue.Count > 0)
        {
            currentTarget = soldierQueue.Dequeue();
        }
    }

}
