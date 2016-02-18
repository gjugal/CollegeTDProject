using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockBarricade : Entity {

    public float blockBarrierHealth = 5;


    public Dictionary<string, int> attackingSoldiers = new Dictionary<string, int>();//This is to keep track of type and count of attackingsoldiers for soldier AI
    // Use this for initialization

    protected override void Start() {
        base.Start();
        health = blockBarrierHealth;

        attackingSoldiers.Add("SwordSoldier(Clone)", 0);
        attackingSoldiers.Add("ArrowSoldier(Clone)", 0);
        attackingSoldiers.Add("HammerSoldier(Clone)", 0);
        attackingSoldiers.Add("Soldier(Clone)", 0);
    }

    public void AddToAttackingSoldiers(Transform t)
    {
        string name = t.gameObject.name;
        //Debug.Log("added " + name);
        attackingSoldiers[name] += 1;
        Entity soldierEntity = t.gameObject.GetComponent<Entity>();
        soldierEntity.OnDeath += RemoveFromDictionary;

    }

    void RemoveFromDictionary(Transform t)
    {
        string name = t.gameObject.name;
        //check if removed entity is soldier

        if (name != "King")
        {
            attackingSoldiers[name] -= 1;
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
