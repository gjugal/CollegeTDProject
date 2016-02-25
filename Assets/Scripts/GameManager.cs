using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public Transform spawnPoint;
    public GameObject kingPrefab;
    public List<Transform> DefenseEntities;
    //Transform kingTransform;

    void Awake()
    {
        if (!GM)
        {
            GM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Instantiate(kingPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
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

    public int GetDefenseType(string defense_type)
    {
        if(defense_type == "Arrow_Tower")
        {
            return Constants.ARROW_TOWER;
        }
        else if(defense_type == "Bomb_Tower")
        {
            return Constants.BOMB_TOWER;
        }
        else if(defense_type == "Block_Barricade")
        {
            return Constants.BLOCK_BARRICADE;
        }
        else if(defense_type == "Ground_Barricade")
        {
            return Constants.GROUND_BARRICADE;
        }
        return -1; ;
    }
    
}
