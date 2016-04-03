using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Use this for initialization
    //Transform kingTransform;
    Vector3 cameraPosition;
    Quaternion cameraRotation;
    public float minimumX;
    public float maximumX;
    public float minimumZ;
    public float maximumZ;

    //GameObject king;
	void Start () {
        cameraPosition = this.transform.position;
        cameraRotation = this.transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {
            float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * 10;
            xMove = xMove + transform.position.x;
            xMove = Mathf.Clamp(xMove, minimumX, maximumX);
            float zMove = Input.GetAxis("Vertical") * Time.deltaTime * 10;
            zMove = zMove + transform.position.z;
            zMove = Mathf.Clamp(zMove, minimumZ, maximumZ);
            this.transform.position = new Vector3(xMove, transform.position.y, zMove);
	}

}
