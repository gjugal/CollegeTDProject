using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject swordSoldierPrefab;
    public GameObject arrowSoldierPrefab;
    public GameObject hammerSoldierPrefab;
    public GameObject kingPrefab;
    public List<Transform> DefenseEntities;
    //Transform kingTransform;

    void Start()
    {
        //Instantiate(soldierPrefab, spawnPoint.position + Vector3.up*0.4f, Quaternion.identity);
        Instantiate(kingPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);

        //Debug.Log(kingTransform);
        //Camera.main.gameObject.GetComponent<CameraController>().SetInitialParameters(GameObject.FindGameObjectWithTag("King").transform);
        
    }

    public List<Transform> GetListOfDefenseEntities()//currently not in use
    {
        List<Transform> _targets = new List<Transform>();
        foreach (Transform t in DefenseEntities)
        {
            if (t.gameObject.activeSelf)
            {
                _targets.Add(t);
            }
        }
        return _targets;
    }

    public void SpawnSwordSoldier() {
        Instantiate(swordSoldierPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
    }

    public void SpawnArrowSoldier()
    {
        Instantiate(arrowSoldierPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
    }

    public void SpawnHammerSoldier()
    {
        Instantiate(hammerSoldierPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
    }
}
