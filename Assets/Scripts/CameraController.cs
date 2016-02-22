using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    //public bool isKing = false;
    Transform kingTransform;
    bool isFirstPerson = false;
    Vector3 cameraPosition;
    Quaternion cameraRotation;

    //GameObject king;
	void Start () {
        //store initial values of camera's transform
        cameraPosition = this.transform.position;
        cameraRotation = this.transform.rotation;
        kingTransform = GameObject.FindGameObjectWithTag("King").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (isFirstPerson) {
            this.transform.position = kingTransform.position;
            this.transform.rotation = kingTransform.rotation;
        }
	}

    //public void SetInitialParameters(Transform kingTransform) {
    //    Debug.Log("inside setinitparam");
    //    this.kingTransform = kingTransform;
    //    isKing = true;
    //    Debug.Log(kingTransform);
    //}

    public void ChangeView() {//called from button changeView from scene and changes the view
        
        if (!isFirstPerson)
        {
            cameraPosition = this.transform.position;
            cameraRotation = this.transform.rotation;
            //SetInitialParameters(GameObject.FindGameObjectWithTag("King").transform);
            //isKing = true;
            isFirstPerson = true;
            kingTransform.gameObject.GetComponent<King>().isFirstPerson = true;
            //king.GetComponent<King>().isfirstPerson = true;
        }else {
            //isKing = false;
            this.transform.position = cameraPosition;
            this.transform.rotation = cameraRotation;
            isFirstPerson = false;
            kingTransform.gameObject.GetComponent<King>().isFirstPerson = false;
            //king.GetComponent<King>().isfirstPerson = false;
        }
    }

}
