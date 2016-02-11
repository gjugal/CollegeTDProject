using UnityEngine;
using System.Collections;

public class ArrowTower : Tower {

    public float timeBetweenShoot = 0.5f;
    float lastShootTime = 0;
    public float towerHealth;
    // Use this for initialization
    void Start () {
        base.Start();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
