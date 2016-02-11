using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject soldierPrefab;
    public GameObject kingPrefab;
    public List<Transform> targets;
    Transform kingTransform;

    void Start()
    {
        //Instantiate(soldierPrefab, spawnPoint.position + Vector3.up*0.4f, Quaternion.identity);
        Instantiate(kingPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);

        Debug.Log(kingTransform);
        //Camera.main.gameObject.GetComponent<CameraController>().SetInitialParameters(GameObject.FindGameObjectWithTag("King").transform);
        
    }

    public List<Transform> GetListOfActiveTarget()
    {
        List<Transform> _targets = new List<Transform>();
        foreach (Transform t in targets)
        {
            if (t.gameObject.activeSelf)
            {
                _targets.Add(t);
            }
        }
        return _targets;
    }

    public void SpawnSoldier() {
        Instantiate(soldierPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
    }
}
