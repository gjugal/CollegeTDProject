using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    public Transform spawnPoint;
    public GameObject soldierPrefab;
    public GameObject kingPrefab;
    public List<Transform> targets;

    void Start()
    {
        Instantiate(soldierPrefab, spawnPoint.position + Vector3.up*0.4f, Quaternion.identity);
        Instantiate(kingPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
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
}
