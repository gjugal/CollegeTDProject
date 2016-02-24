using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SoldierProps : ScriptableObject {

    public string myFirstname = "entityFirstName";
    public Color originalColor = Color.white;
    public float timeBetweenShoots = 0;
    public float damage;
    public int health;
    public List<int> damagePercentage;
}
