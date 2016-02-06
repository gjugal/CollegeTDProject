using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance = null;
    public Transform spawnPoint;
    public GameObject soldierPrefab;

    private GameManager()
    {
    }

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }

        return instance;
    }

    void Start()
    {
        Instantiate(soldierPrefab, spawnPoint.position + Vector3.up*0.4f, Quaternion.identity);
    }
}
