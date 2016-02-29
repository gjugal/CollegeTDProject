﻿using UnityEngine;
using System.Collections;

public class Bullet : Projectiles
{
  

    protected override void Start()
    {
        Destroy(gameObject, 2);
        base.Start();
        speed = 5;
        damage = 1;
        collisionMask = 9;
    }

    

    void Update()
    {
        float moveDistance = this.speed * Time.deltaTime;
        CheckCollision(moveDistance);//check collision before hitting using raycast
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollision(float distance)//check collision before hitting using raycast
    {
        //Debug.Log("checkCollisionEnter");
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            //Debug.Log("collision detected");
            if(hit.collider.gameObject.layer == collisionMask)
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit)//gives damage to colliding tower and destroys bullet
    {
        //Debug.Log("onhit");
        IDamagable damagableObject = hit.collider.gameObject.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeDamage(damage);
        }
        GameObject.Destroy(this.gameObject);
    }



}
