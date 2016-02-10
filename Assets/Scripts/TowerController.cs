using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

    GameObject bullet;
    Transform dome;

    public float speed = 5;
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
    
    public void Shoot()
    {
        bullet = Instantiate(bulletPrefab, nozzlePosition.position, nozzlePosition.rotation) as GameObject;
        bullet.GetComponent<Bullet>().SetSpeed(speed);
    }
}
