using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class StoreManager : MonoBehaviour {

    public GameObject[] soldierPanels;
    public Text gems;
    public Text coins;

    int player_gems;
    int player_coins;
    string health = "";
    string range = "";
    string rate = "";
    string cost = "";
    bool isBuyable = false;
    bool isUpgradable = false;

    Button currentUpgradedButton;

	public void ChangeLevel(int i)
    {
        UIManager.UM.LoadScene(i);
    }

    void Start()
    {
        player_gems = StatisticsManager.SM.GetDetails("Player_Gems");
        player_coins = StatisticsManager.SM.GetDetails("Player_Coins");
        SetTheStore();
    }

    void SetTheStore()
    {
        gems.text =  player_gems.ToString();
        coins.text =  player_coins.ToString();
        for (int i = 0; i < soldierPanels.Length; i++)
        { 
            Text[] details = soldierPanels[i].GetComponentsInChildren<Text>();

            //wE HAVE TO SET THE IMAGE IN FUTURE
            //Image image = soldierPanels[i].GetComponentInChildren<Image>();

            Button buyButton = soldierPanels[i].GetComponentInChildren<Button>();

            if (i == Constants.SWORD_SOLDIER)
            {
                //This is a sword soldier
                SetSwordSoldierValues();
            }
            else if (i == Constants.ARROW_SOLDIER)
            {
                //This is an arrow soldier
                SetArrowSoldierValues();
            }
            else if (i == Constants.HAMMER_SOLDIER)
            {
                SetHammerSoldierValues();
            }
            details[Constants.SOLDIER_RANGE].text = range;
            details[Constants.SOLDIER_HEALTH].text = health;
            details[Constants.SOLDIER_RATE].text = rate;
            details[Constants.SOLDIER_COST].text = cost;
            if(isBuyable)
            {
                details[Constants.SOLDIER_BUY].text = "BUY";
            }
            else if(isUpgradable)
            {
                details[Constants.SOLDIER_BUY].text = "UPGRADE";
            }
            else
            {
                buyButton.enabled = false;
            }
        }
    }

    public void OnBuyButtonClicked(int soldier_type)
    {
        bool isBought = false;
        if (soldier_type == Constants.SWORD_SOLDIER)
        {
            int state = StatisticsManager.SM.GetDetails("Sword_Soldier_State");
            if (state == Constants.BUYABLE)
            {
                isBought = BuySoldier(Constants.SS_LVL_1_COST, soldier_type, true);
                if(isBought)
                {
                    state = Constants.BOUGHT_LEVEL1;
                } 
            }
            else if (state == Constants.BOUGHT_LEVEL1)
            {
                isBought = BuySoldier(Constants.SS_LVL_2_COST, soldier_type, true);
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL2;
                }
            }
            else if(state == Constants.BOUGHT_LEVEL2)
            {
                isBought = BuySoldier(Constants.SS_LVL_3_COST, soldier_type, true);
                currentUpgradedButton.enabled = false;
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL3;
                }
            }
            StatisticsManager.SM.SetDetails("Sword_Soldier_State", state.ToString());
            Debug.Log("Sword soldier upgraded to level " + state.ToString());
        }
        else if (soldier_type == Constants.ARROW_SOLDIER)
        {
            int state = StatisticsManager.SM.GetDetails("Arrow_Soldier_State");
            if (state == Constants.BUYABLE)
            {
                isBought = BuySoldier(Constants.AS_LVL_1_COST, soldier_type, true);
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL1;
                }
            }
            else if (state == Constants.BOUGHT_LEVEL1)
            {
                isBought = BuySoldier(Constants.AS_LVL_2_COST, soldier_type, true);
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL2;
                }
            }
            else if (state == Constants.BOUGHT_LEVEL2)
            {
                isBought = BuySoldier(Constants.AS_LVL_3_COST, soldier_type, true);
                currentUpgradedButton.enabled = false;
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL3;
                }
            }
            StatisticsManager.SM.SetDetails("Arrow_Soldier_State", state.ToString());
            Debug.Log("Arrow soldier upgraded to level " + state.ToString());
        }
        else if (soldier_type == Constants.HAMMER_SOLDIER)
        {
            int state = StatisticsManager.SM.GetDetails("Hammer_Soldier_State");
            if (state == Constants.BUYABLE)
            {
                isBought = BuySoldier(Constants.HS_LVL_1_COST, soldier_type, true);
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL1;
                }
            }
            else if (state == Constants.BOUGHT_LEVEL1)
            {
                isBought = BuySoldier(Constants.HS_LVL_2_COST, soldier_type, true);
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL2;
                }
            }
            else if (state == Constants.BOUGHT_LEVEL2)
            {
                isBought = BuySoldier(Constants.HS_LVL_3_COST, soldier_type, true);
                currentUpgradedButton.enabled = false;
                if (isBought)
                {
                    state = Constants.BOUGHT_LEVEL3;
                }
            }
            StatisticsManager.SM.SetDetails("Hammer_Soldier_State", state.ToString());
            Debug.Log("Hammer soldier upgraded to level " + state.ToString());
        }
        StatisticsManager.SM.SetDetails("Player_Gems", player_gems.ToString());
        if(isBought)
        {
            SetTheStore();
        }
    }

    private bool BuySoldier(int cost, int soldier_type, bool upgradable)
    {
        bool bought = false;
        player_gems = StatisticsManager.SM.GetDetails("Player_Gems");
        if (player_gems >= cost)
        {
            //Let him buy
            //Decrease his gems and Update in the Dictionary and GemsText
            //Change the text of buy button to upgrade
            //Change the level to 1
            player_gems -= cost;
            gems.text = player_gems.ToString();
            if (upgradable)
            {
                currentUpgradedButton = soldierPanels[soldier_type].GetComponentInChildren<Button>();
                Text t = currentUpgradedButton.GetComponentInChildren<Text>();
                if(!t.text.Equals("UPGRADE"))
                {
                    t.text = "UPGRADE";
                }
            }
            bought = true;
        }
        else
        {
            //Display dialog you don't have enough gems
            bought = false;
        }
        return bought;
    }

    void SetSwordSoldierValues()
    {
        
        if (StatisticsManager.SM.GetDetails("Sword_Soldier_State") == Constants.TOTAL_LOCKED)
        {
            //Sword soldier is cannot be bought
            //Set the panel a bit dull
            //Values of Level 1 of the soldier will be displayed
            health = Constants.SS_LVL_1_HEALTH.ToString();
            range = Constants.SS_LVL_1_RANGE.ToString();
            rate = Constants.SS_LVL_1_RATE.ToString();
            cost = Constants.SS_LVL_1_COST.ToString();
            isBuyable = false;
            isUpgradable = false;
        }
        else if (StatisticsManager.SM.GetDetails("Sword_Soldier_State") == Constants.BUYABLE)
        {
            //Sword soldier is can be bought
            //Set the panel active
            //Values of Level 1 of the soldier will be displayed
            health = Constants.SS_LVL_1_HEALTH.ToString();
            range = Constants.SS_LVL_1_RANGE.ToString();
            rate = Constants.SS_LVL_1_RATE.ToString();
            cost = Constants.SS_LVL_1_COST.ToString();
            isBuyable = true;
            isUpgradable = false;
        }
        else if (StatisticsManager.SM.GetDetails("Sword_Soldier_State") == Constants.BOUGHT_LEVEL1)
        {
            //Sword soldier is of level 1
            //Set the details of level 2
            health = Constants.SS_LVL_2_HEALTH.ToString();
            range = Constants.SS_LVL_2_RANGE.ToString();
            rate = Constants.SS_LVL_2_RATE.ToString();
            cost = Constants.SS_LVL_2_COST.ToString();
            isBuyable = false;
            isUpgradable = true;
        }
        else if (StatisticsManager.SM.GetDetails("Sword_Soldier_State") == Constants.BOUGHT_LEVEL2)
        {
            //Sword soldier is of level 2
            //Set the details of level 3
            health = Constants.SS_LVL_3_HEALTH.ToString();
            range = Constants.SS_LVL_3_RANGE.ToString();
            rate = Constants.SS_LVL_3_RATE.ToString();
            cost = Constants.SS_LVL_3_COST.ToString();
            isBuyable = false;
            isUpgradable = true;
        }
        else if(StatisticsManager.SM.GetDetails("Sword_Soldier_State") == Constants.BOUGHT_LEVEL3)
        {
            //Sword soldier is of level 3
            //Set the details of level 3
            health = Constants.SS_LVL_3_HEALTH.ToString();
            range = Constants.SS_LVL_3_RANGE.ToString();
            rate = Constants.SS_LVL_3_RATE.ToString();
            cost = Constants.SS_LVL_3_COST.ToString();
            isBuyable = false;
            isUpgradable = false;
        }
    }

    void SetArrowSoldierValues()
    {
        if (StatisticsManager.SM.GetDetails("Arrow_Soldier_State") == Constants.TOTAL_LOCKED)
        {
            //Arrow soldier cannot be bought
            //Set the panel a bit dull
            //Values of Level 1 of the soldier will be displayed
            health = Constants.AS_LVL_1_HEALTH.ToString();
            range = Constants.AS_LVL_1_RANGE.ToString();
            rate = Constants.AS_LVL_1_RATE.ToString();
            cost = Constants.AS_LVL_1_COST.ToString();
            isBuyable = false;
            isUpgradable = false;
        }
        else if (StatisticsManager.SM.GetDetails("Arrow_Soldier_State") == Constants.BUYABLE)
        {
            //Arrow soldier can be bought
            //Set the panel active
            //Values of Level 1 of the soldier will be displayed
            health = Constants.AS_LVL_1_HEALTH.ToString();
            range = Constants.AS_LVL_1_RANGE.ToString();
            rate = Constants.AS_LVL_1_RATE.ToString();
            cost = Constants.AS_LVL_1_COST.ToString();
            isBuyable = true;
            isUpgradable = false;
        }
        else if (StatisticsManager.SM.GetDetails("Arrow_Soldier_State") == Constants.BOUGHT_LEVEL1)
        {
            //Arrow soldier is of level 1
            //Set the details of level 2
            health = Constants.AS_LVL_2_HEALTH.ToString();
            range = Constants.AS_LVL_2_RANGE.ToString();
            rate = Constants.AS_LVL_2_RATE.ToString();
            cost = Constants.AS_LVL_2_COST.ToString();
            isBuyable = false;
            isUpgradable = true;
        }
        else if (StatisticsManager.SM.GetDetails("Arrow_Soldier_State") == Constants.BOUGHT_LEVEL2)
        {
            //Arrow soldier is of level 2 
            //Set the details of level 3
            health = Constants.AS_LVL_3_HEALTH.ToString();
            range = Constants.AS_LVL_3_RANGE.ToString();
            rate = Constants.AS_LVL_3_RATE.ToString();
            cost = Constants.AS_LVL_3_COST.ToString();
            isBuyable = false;
            isUpgradable = true;

        }
        else if (StatisticsManager.SM.GetDetails("Arrow_Soldier_State") == Constants.BOUGHT_LEVEL3)
        {
            //Arrow soldier is of level 3 
            //Set the details of level 3
            health = Constants.AS_LVL_3_HEALTH.ToString();
            range = Constants.AS_LVL_3_RANGE.ToString();
            rate = Constants.AS_LVL_3_RATE.ToString();
            cost = Constants.AS_LVL_3_COST.ToString();
            isBuyable = false;
            isUpgradable = false;
        }
    }

    void SetHammerSoldierValues()
    {
        if (StatisticsManager.SM.GetDetails("Hammer_Soldier_State") == Constants.TOTAL_LOCKED)
        {
            //Hammer soldier is cannot be bought
            //Set the panel a bit dull
            //Values of Level 1 of the soldier will be displayed
            health = Constants.HS_LVL_1_HEALTH.ToString();
            range = Constants.HS_LVL_1_RANGE.ToString();
            rate = Constants.HS_LVL_1_RATE.ToString();
            cost = Constants.HS_LVL_1_COST.ToString();
            isBuyable = false;
            isUpgradable = false;
        }
        else if (StatisticsManager.SM.GetDetails("Hammer_Soldier_State") == Constants.BUYABLE)
        {
            //Hammer soldier is can be bought
            //Set the panel active
            //Values of Level 1 of the soldier will be displayed
            health = Constants.HS_LVL_1_HEALTH.ToString();
            range = Constants.HS_LVL_1_RANGE.ToString();
            rate = Constants.HS_LVL_1_RATE.ToString();
            cost = Constants.HS_LVL_1_COST.ToString();
            isBuyable = true;
            isUpgradable = false;
        }
        else if (StatisticsManager.SM.GetDetails("Hammer_Soldier_State") == Constants.BOUGHT_LEVEL1)
        {
            //Hammer soldier is of level 1
            //Set the details of level 2
            health = Constants.HS_LVL_2_HEALTH.ToString();
            range = Constants.HS_LVL_2_RANGE.ToString();
            rate = Constants.HS_LVL_2_RATE.ToString();
            cost = Constants.HS_LVL_2_COST.ToString();
            isBuyable = false;
            isUpgradable = true;
        }
        else if (StatisticsManager.SM.GetDetails("Hammer_Soldier_State") == Constants.BOUGHT_LEVEL2)
        {
            //Hammer soldier is of level 2
            //Set the details of level 3
            health = Constants.HS_LVL_3_HEALTH.ToString();
            range = Constants.HS_LVL_3_RANGE.ToString();
            rate = Constants.HS_LVL_3_RATE.ToString();
            cost = Constants.HS_LVL_3_COST.ToString();
            isBuyable = false;
            isUpgradable = true;
        }
        else if (StatisticsManager.SM.GetDetails("Hammer_Soldier_State") == Constants.BOUGHT_LEVEL3)
        {
            //Hammer soldier is of level 3
            //Set the details of level 3
            health = Constants.HS_LVL_3_HEALTH.ToString();
            range = Constants.HS_LVL_3_RANGE.ToString();
            rate = Constants.HS_LVL_3_RATE.ToString();
            cost = Constants.HS_LVL_3_COST.ToString();
            isBuyable = false;
            isUpgradable = false;
        }
    }
}
