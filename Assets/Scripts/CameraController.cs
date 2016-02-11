using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    public bool isKing = false;
    Transform kingTransform;
    bool isFirstPerson = false;
    Vector3 cameraPosition;
    Quaternion cameraRotation;
	void Start () {
        cameraPosition = this.transform.position;
        cameraRotation = this.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
        if (isKing) {
            this.transform.position = kingTransform.position;
            this.transform.rotation = kingTransform.rotation;
        }
	}

    public void SetInitialParameters(Transform kingTransform) {
        Debug.Log("inside setinitparam");
        this.kingTransform = kingTransform;
        isKing = true;
        Debug.Log(kingTransform);
    }

    public void ChangeView() {
        if (!isFirstPerson)
        {
            cameraPosition = this.transform.position;
            cameraRotation = this.transform.rotation;
            //SetInitialParameters(GameObject.FindGameObjectWithTag("King").transform);
            this.kingTransform = GameObject.FindGameObjectWithTag("King").transform;
            isKing = true;
            isFirstPerson = true;
        }else {
            isKing = false;
            this.transform.position = cameraPosition;
            this.transform.rotation = cameraRotation;
            isFirstPerson = false;
        }
    }

}
