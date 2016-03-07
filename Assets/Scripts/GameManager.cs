using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public Transform spawnPoint;
    public GameObject kingPrefab;
    public Image loadingBackground;

    //All Lists
    public ScriptableRankProperties[] rankProps;
    public GameObject[] interfaceActiveInBattle;
    public GameObject[] interfaceBeforeBattle;

    int rank;
    ScriptableRankProperties currentRankProps;
    PathGenerator generator;
    WaitForSeconds waitTime = new WaitForSeconds(10f);

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
        //1. Loading should appear
        //2. Get value from StatisticsManager
        //3. Set the values for Path
        //4. Give the  values to PathGenerator Class
        //5. Activate the interface before battle
        //5. End
        loadingBackground.gameObject.SetActive(true);
        BeforeBattle(false);
        InBattle(false);
        rank = StatisticsManager.SM.GetDetails("Player_Rank");
        currentRankProps = rankProps[rank - 1];
        generator = GameObject.Find("MyMap").GetComponent<PathGenerator>();
        generator.SetValuesAndGenerate(currentRankProps);
        StartCoroutine(WaitSometime());
        loadingBackground.gameObject.SetActive(false);
        BeforeBattle(true);
       
    }

    IEnumerator WaitSometime()
    {
        yield return waitTime;
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

    void InBattle(bool state)
    {
        if(state)
        Instantiate(kingPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
        foreach (GameObject b in interfaceActiveInBattle)
        {
            b.gameObject.SetActive(state);
        }
    }

    void BeforeBattle(bool state)
    {
        foreach (GameObject b in interfaceBeforeBattle)
        {
            b.gameObject.SetActive(state);
        }
    }

    public void OnBattleBtnClicked(Transform t)
    {
        t.gameObject.SetActive(false);
        InBattle(true);
        // King should be instantiated now. This need to be done in future.
        // Instantiate(kingPrefab, spawnPoint.position + Vector3.up * 0.4f, Quaternion.identity);
    }
    
}
