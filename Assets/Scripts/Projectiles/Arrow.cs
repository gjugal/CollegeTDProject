using UnityEngine;
using System.Collections;

public class Arrow : Projectiles
{

    protected override void Start()
    {
        Destroy(gameObject, 2);
        base.Start();
        speed = 5;
    }



    void Update()
    {
        float moveDistance = this.speed * Time.deltaTime;
        CheckCollision(moveDistance);//check collision before hitting using raycast
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollision(float distance)//check collision before hitting using raycast
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.gameObject.layer == pathMask || hit.collider.gameObject.layer == DefenseMask)
            {
                Destroy(gameObject);
            }
        }
    }

    



}

