using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    // Use this for initialization
    float currentTime;
    public float timeInSeconds;

	void Start () {
        currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if(this.gameObject.activeSelf)
        {
            if (currentTime <= timeInSeconds)
            {
                currentTime += Time.deltaTime;
                this.GetComponent<Image>().fillAmount = currentTime / timeInSeconds;
            }
            else
            {
                //Deactive self
                this.gameObject.SetActive(false);
                //Set GameOver
            }
        }
	}
}
