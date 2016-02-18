using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StatisticsManager : MonoBehaviour {

    public static StatisticsManager SM;
    Dictionary<string, string> stats;

    void Awake()
    {
        if (!SM)
        {
            SM = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        //create a dictionary of player statistics
        stats = new Dictionary<string, string>();

        //Check whether data is present on the disk, then fetch it and store the player current stats
        /*
            Code goes here
        */

        //Get the internet connectivity and set the dictionary here
        /*
            Code goes here
        */

        //else check if the dictionary is null then so can go and set the InitialDetails
        SetThePlayerInitialDetails();

    }

    void SetThePlayerInitialDetails()
    {
        SetDetails("Player_Coins", Constants.PLAYER_INITIAL_COINS.ToString());
        SetDetails("Player_Gems", Constants.PLAYER_INITIAL_GEMS.ToString());
        SetDetails("Sword_Soldier_State", Constants.BUYABLE.ToString());
        SetDetails("Arrow_Soldier_State", Constants.BUYABLE.ToString());
        SetDetails("Hammer_Soldier_State", Constants.BUYABLE.ToString());

    }

    public void SetDetails(string key, string value)
    {
        if(stats.ContainsKey(key))
        {
            stats[key] = value;
        }
        else
        {
            stats.Add(key, value);
        }
    }

    public int GetDetails(string key)
    {
        if (stats.ContainsKey(key))
        {
            return int.Parse(stats[key]);
        }
        else
        {
            Debug.LogError("Key Not Found In Player Statistics Dictionary");
        }
        return -1;
    }
}
