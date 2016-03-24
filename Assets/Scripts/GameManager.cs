using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    public Transform kingPrefab, OffenseHQ;
    public Transform myMap;
    public Image loadingBackground;
    public Text percentDoneText;
    public Transform pausePanel;
    public Vector3 spawnPosition;

    //All Lists
    public ScriptableRankProperties[] rankProps;
    public GameObject[] interfaceActiveInBattle;
    public GameObject[] interfaceBeforeBattle;

    int rank;
    ScriptableRankProperties currentRankProps;
    PathGenerator generator;
    Transform myGeneratedMap;

    float waitTime = 5f;
    bool loadingDone = false;

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
        loadingDone = false;
        loadingBackground.gameObject.SetActive(true);

        //Instantiate MyMap
        myGeneratedMap = Instantiate(myMap, Vector3.zero, Quaternion.identity) as Transform;
        myGeneratedMap.gameObject.name = "MyMap";

        //Create OffenseHeadquaters
        Transform OHQ = Instantiate(OffenseHQ, transform.position, Quaternion.identity) as Transform;
        OHQ.name = "OffenseHeadQuaters";

        BeforeBattle(false);
        InBattle(false);
        rank = StatisticsManager.SM.GetDetails("Player_Rank");
        currentRankProps = rankProps[rank - 1];
        generator = myGeneratedMap.gameObject.GetComponent<PathGenerator>();
        generator.OnPercentChange += UpdatePercentValue;
        generator.SetValuesAndGenerate(currentRankProps);
        spawnPosition = generator.spawnPointPosition;
    }

    public void UpdatePercentValue(int value)
    {
        percentDoneText.text = value + " %";
        if(value == 100)
        {
            loadingDone = true;
        }
    }

    void Update()
    {
        if(loadingDone)
        {
            if(Input.GetMouseButtonDown(0))
            {
                RemoveLoading();
                loadingDone = false;
            }
        }
    }
    public void RemoveLoading()
    {
        loadingBackground.gameObject.SetActive(false);
        BeforeBattle(true);
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
        else if(defense_type == "Gate")
        {
            return Constants.GATE;
        }
        return -1;
    }

    void InBattle(bool state)
    {
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
        Instantiate(kingPrefab, spawnPosition, Quaternion.identity);
    }

    public void OnPauseClicked()
    {
        pausePanel.gameObject.SetActive(true);
        InBattle(false);
        Time.timeScale = 0f;
    }
    
    public void OnResumeClicked()
    {
        pausePanel.gameObject.SetActive(false);
        InBattle(true);
        Time.timeScale = 1f;
    }
}
