using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Loading : MonoBehaviour {

    Text loading;
    int i = 0;
    float lastTime = 0;
    int length;

    public float timeGap;
    string originalString;

	void Start()
    {
        loading = gameObject.GetComponent<Text>();
        originalString = loading.text.ToString();
        length = originalString.Length;
    }

    void Update()
    {
        if(Time.time > lastTime + timeGap)
        {
            lastTime = Time.time;
            loading.text = originalString.Substring(0, length - i);
            i = (i + 1) % (length + 1);
        }
    }
}
