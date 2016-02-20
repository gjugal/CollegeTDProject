using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockBarricade : Entity {

    public float blockBarrierHealth = 5;

    protected override void Start() {
        base.Start();
        myFirstName = "BlockBarricade";
        entityLL = new LinkedList<MyTargets>();
        health = blockBarrierHealth;
    }

    public void AddToAttackingSoldiers(Transform t)
    {
        Entity entity = t.gameObject.GetComponent<Entity>();
        entity.OnDeath += RemoveFromAttackingSoldiers;
        entityLL.AddLast(new MyTargets(t, true, entity.myFirstName));
    }

    void RemoveFromAttackingSoldiers(Transform t)
    {
         entityLL.Remove(FindFromTargets(t));
    }

    public int[] GetAttackingEntitiesCount()
    {
        int[] entities = new int[Constants.SOLDIER_TYPES];
        foreach (MyTargets t in entityLL)
        {
            string type = t.GetTargetType();
            if (type == "Sword_Soldier")
            {
                entities[Constants.SWORD_SOLDIER]++;
            }
            else if (type == "Arrow_Soldier")
            {
                entities[Constants.ARROW_SOLDIER]++;
            }
            else if (type == "Hammer_Soldier")
            {
                entities[Constants.HAMMER_SOLDIER]++;
            }
        }
        return entities;
    }

    // Update is called once per frame
    void Update () {
	
	}
}
