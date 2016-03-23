using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    // Use this for initialization

    Vector3 rot = new Vector3(0.0f, 0.0f, 0.0f);

    void Start()
    {
        //Debug.Log("start name is " + transform.parent.name + " and tag is " + transform.parent.tag);
        if (transform.parent.tag == "BlockBarricade")
        {
            Debug.Log("It is bloack barricade");
            if(transform.parent.transform.rotation.y != 0)
            {
                //Debug.Log("rotate me " + transform.parent.transform.rotation.y);//change it
                //transform.localScale = new Vector3(2f, 0.4f, 1f);
                //Debug.Log("now " + transform.localEulerAngles);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        transform.eulerAngles = rot;
	}
}
