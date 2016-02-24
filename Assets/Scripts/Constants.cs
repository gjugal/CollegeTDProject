using UnityEngine;
using System.Collections;

public static class Constants{

    //SCENE DETAILS
    public const int START_MENU_SCENE = 0;
    public const int SHOP_MENU_SCENE = 1;
    public const int GAME_SCENE = 2;

    //PLAYER DETAILS
    public const int PLAYER_INITIAL_COINS = 500;
    public const int PLAYER_INITIAL_GEMS = 200;

    //SOLDIER DETAILS

    public const int SOLDIER_TYPES = 3;

    public const int KING = 99;
    public const int SWORD_SOLDIER = 0;
    public const int ARROW_SOLDIER = 1;
    public const int HAMMER_SOLDIER = 2;

    public const int SOLDIER_HEALTH = 0;
    public const int SOLDIER_RANGE = 1;
    public const int SOLDIER_RATE = 2;
    public const int SOLDIER_COST = 3;
    public const int SOLDIER_BUY = 4;

    public const int TOTAL_LOCKED = 0; //Cannot be bought
    public const int BOUGHT_LEVEL1 = 1;
    public const int BOUGHT_LEVEL2 = 2;
    public const int BOUGHT_LEVEL3 = 3;
    public const int BUYABLE = 4; //Can be bought and the soldier is of level 0

    //SWORD SOLDIER DETAILS
    public const int SS_LVL_1_COST = 10;
    public const int SS_LVL_1_HEALTH = 50;
    public const float SS_LVL_1_RANGE = 0.5f;
    public const float SS_LVL_1_RATE = 0.2f;

    public const int SS_LVL_2_COST = 20;
    public const int SS_LVL_2_HEALTH = 75;
    public const float SS_LVL_2_RANGE = 0.6f;
    public const float SS_LVL_2_RATE = 0.15f;

    public const int SS_LVL_3_COST = 30;
    public const int SS_LVL_3_HEALTH = 90;
    public const float SS_LVL_3_RANGE = 0.7f;
    public const float SS_LVL_3_RATE = 0.12f;

    //ARROW SOLDIER DETAILS
    public const int AS_LVL_1_COST = 15;
    public const int AS_LVL_1_HEALTH = 30;
    public const float AS_LVL_1_RANGE = 3f;
    public const float AS_LVL_1_RATE = 0.15f;

    public const int AS_LVL_2_COST = 30;
    public const int AS_LVL_2_HEALTH = 45;
    public const float AS_LVL_2_RANGE = 4f;
    public const float AS_LVL_2_RATE = 0.12f;

    public const int AS_LVL_3_COST = 45;
    public const int AS_LVL_3_HEALTH = 60;
    public const float AS_LVL_3_RANGE = 5f;
    public const float AS_LVL_3_RATE = 0.1f;


    //HAMMER SOLDIER DETAILS
    public const int HS_LVL_1_COST = 20;
    public const int HS_LVL_1_HEALTH = 150;
    public const float HS_LVL_1_RANGE = 0.5f;
    public const float HS_LVL_1_RATE = 0.3f;

    public const int HS_LVL_2_COST = 40;
    public const int HS_LVL_2_HEALTH = 200;
    public const float HS_LVL_2_RANGE = 0.6f;
    public const float HS_LVL_2_RATE = 0.25f;

    public const int HS_LVL_3_COST = 60;
    public const int HS_LVL_3_HEALTH = 250;
    public const float HS_LVL_3_RANGE = 0.7f;
    public const float HS_LVL_3_RATE = 0.2f;

    //DEFENSE ENTITIES
    public const int ARROW_TOWER = 0;
    public const int BOMB_TOWER = 1;
    public const int BLOCK_BARRICADE = 2;
    public const int GROUND_BARRICADE = 3;

   
}
