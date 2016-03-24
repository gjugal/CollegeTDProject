using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Gate : Entity {

    public float gateHealth = 15;
    float myHealth;
	// Use this for initialization
	protected override void Start () {
        base.Start();
        myFirstName = "Gate";
        entityLL = new LinkedList<MyTargets>();
        health = gateHealth;
        myHealth = health;
        this.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.GetComponent<Image>().fillAmount = health / myHealth;
    }
}
