using UnityEngine;
using System.Collections;

public class Projectiles : MonoBehaviour {

    protected float speed;
    protected float damage;

    public LayerMask collisionMask;
    // Use this for initialization
    protected virtual void Start () {
        Destroy(gameObject, 3);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
