using UnityEngine;
using System.Collections;

public class SoldierCensor : MonoBehaviour {


    void OnTriggerEnter(Collider col)
    {
        Debug.Log("triggered");
        if (col.gameObject.tag == "TowerBase") {
            //Debug.Log("triggered");
            this.transform.parent.GetComponent<Soldier>().TargetEntry(col.transform.parent.transform, col.gameObject.transform.parent.GetComponent<Tower>().GetAttackingEntitiesCount());
        }
        else if (col.gameObject.tag == "BlockBarricade") {
            Debug.Log("barricade detected");
            this.transform.parent.GetComponent<Soldier>().TargetEntry(col.transform, col.gameObject.GetComponent<BlockBarricade>().GetAttackingEntitiesCount());
        }
    }
}
