using UnityEngine;
using System.Collections;

public class Cartridge : MonoBehaviour{

    protected float speed = 5f;
    // Use this for initialization
    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
