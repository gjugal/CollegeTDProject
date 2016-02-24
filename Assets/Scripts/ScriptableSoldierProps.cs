using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ScriptableSoldierProps : ScriptableObject {

    public string name = "entityFirstName";
    public Color originalColor = Color.white;
    public float timeBetweenShoots = 0;
    public float damage;
    public int health;
    public List<int> damagePercentage;
    public int cost;
}
