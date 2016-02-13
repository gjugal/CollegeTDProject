using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

    GameObject bullet;
    Transform dome;

    public GameObject bulletPrefab;
    public Transform nozzlePosition;

    void Start()
    { 
        dome = this.transform.GetChild(1).gameObject.GetComponent<Transform>();
    }
	// Use this for initialization
    public void LookAtEnemy(Transform target)
    {
        dome.LookAt(target);
    }
    
    public void Shoot(float InitialForce)
    {
        bullet = Instantiate(bulletPrefab, nozzlePosition.position, nozzlePosition.rotation) as GameObject;
        //Debug.Log(bullet);
        try
        {
            bullet.GetComponent<Bullet>().SetSpeed(InitialForce);
        }
        catch
        {
            bullet.GetComponent<Bomb>().SetSpeed(InitialForce);
        }
    }
}
