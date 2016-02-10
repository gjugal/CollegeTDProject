using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public LayerMask collisionMask;
    float speed = 5f;
    float damage = 1;

    void Start()
    {
        Destroy(gameObject, 3);    
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    void Update()
    {
        float moveDistance = this.speed * Time.deltaTime;
        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollision(float distance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit)
    {
        IDamagable damagableObject = hit.collider.gameObject.GetComponent<IDamagable>();
        if (damagableObject != null)
        {
            damagableObject.TakeDamage(damage);
        }
        GameObject.Destroy(this.gameObject);
    }



}
