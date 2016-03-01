using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour {

    GameObject bullet;
    Transform dome;

    public GameObject bulletPrefab;
    public Transform nozzlePosition;

    void Start()
    {
        //Debug.Log(this.transform.FindChild("Armature").FindChild("Bone").name);
        dome = this.transform.FindChild("Armature").GetComponent<Transform>();
    }
	// Use this for initialization
    public void LookAtEnemy(Transform target)//called from update of arrowtower and bombtower
    {
        if (this.GetComponent<BombTower>() != null)//if this is bombtower //calculates angle required to shoot target and sets rotation.x accorgingly
        {
            //Debug.Log("BLABLA");
            float v = this.GetComponent<BombTower>().initialForceC;
            float x = Vector3.Distance(new Vector3(target.position.x, 0, target.position.z), new Vector3(nozzlePosition.parent.parent.parent.position.x, 0, nozzlePosition.parent.parent.parent.position.z));
            float y = -(nozzlePosition.parent.parent.parent.position.y + dome.transform.position.y - target.position.y);
            float g = 9.81f / 2f;
            float temp = (g * x * x) / (v * v);
            float thita = Mathf.Atan((x + Mathf.Sqrt((x * x) - 4 * temp * (y - temp))) / (2 * temp));
            //float beta = Mathf.Atan((x - Mathf.Sqrt((x * x) - 4 * temp * (y - temp))) / (2 * temp));
            //float thita = Mathf.Atan((v * v + Mathf.Sqrt(Mathf.Pow(v, 4) - g * ((g * x * x) + 2 * y * v * v))) / (g * x));
            //x = x - (y / Mathf.Tan(thita));
            //thita = Mathf.Atan((v * v + Mathf.Sqrt(Mathf.Pow(v, 4) - g * ((g * x * x) + 2 * y * v * v))) / (g * x));
            ////float thita = Mathf.Asin((g * x) / v * v);
            thita = Mathf.Rad2Deg * thita;
            //beta = Mathf.Rad2Deg * beta;
            ////Debug.Log("thita " + Mathf.Atan(v * v - Mathf.Sqrt(Mathf.Pow(v, 4) - g * ((g * x * x) + 2 * y * v * v))) / (g * x) * Mathf.Rad2Deg + "beta " + Mathf.Rad2Deg* Mathf.Atan(v * v - Mathf.Sqrt(Mathf.Pow(v, 4) - g * ((g * x * x) + 2 * (-y) * v * v))) / (g * x));
            ////Debug.Log("nozzle" + (nozzlePosition.parent.parent.parent.position.y + target.transform.position.y) + " target" + target.position.y + " x=" + x);
            //Debug.Log("thita= " + thita + " y= " + y + " x= " + x + " v= " + v + " g= " + g);
            //Transform t = target;
            //float diffb = dome.transform.localRotation.x * Mathf.Rad2Deg;
            dome.LookAt(target);
            //float diff = dome.transform.localRotation.x * Mathf.Rad2Deg;
            //Debug.Log("before " + diffb + " after " + diff);
            //Debug.Log(thita);
            dome.transform.eulerAngles = new Vector3(-thita, dome.transform.eulerAngles.y, dome.transform.eulerAngles.z);
            //Vector3 beta = new Vector3(-80, dome.transform.rotation.y, dome.transform.rotation.z);
            //dome.transform.rotation.SetAxisAngle(new Vector3(-1, 0, 0), 80);
            //dome.transform.rotation = Quaternion.Euler(beta);
            //dome.transform.Rotate((-80f + diff), 0, 0);
            //Debug.Log(dome.transform.rotation);
            //Debug.Log(thita);
        }
        else
        {
            dome.LookAt(target);
        }
    }
    
    public void Shoot(float InitialForce)//called from update() of bombtower and arrowtower
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
