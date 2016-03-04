using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(NavMeshAgent))]
public class King : Entity
{
    public float kingHealth;
    public float rotationSpeed = 5f;
    public float moveSpeed = 5f;
    enum KingStates { IDLE, WALK, ATTACK };
    NavMeshAgent agent;
    KingStates currentState;
    Camera mainCamera;
    public LayerMask pathLayerMask;
    public LayerMask defenseLayerMask;

    float timeBetweenShoots = 1;
    float lastShootTime = 0;
    float damage = 1;
    Color originalColor;

    Dictionary<string, bool> blockingButtonsName; 
    LinkedList<Transform> targets = null;
    Transform currentTarget = null;
    [HideInInspector]
    public bool isFirstPerson = false;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        health = kingHealth;
        currentState = KingStates.IDLE;
        agent = GetComponent<NavMeshAgent>();
        targets = new LinkedList<Transform>();
        originalColor = this.gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log((agent.destination + Vector3.up * 0.4f) + "   " + transform.position);
        //Debug.Log(currentState);
        // movement
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())//on mouse click go to destination
        {
            Vector3 clickPos = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(clickPos);
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            RaycastHit rayhit;
            if (Physics.Raycast(ray.origin, ray.direction * 10000, out rayhit, 5000, defenseLayerMask))
            {
                //Debug.Log(final);
                currentTarget = rayhit.collider.gameObject.transform.parent.transform;

                //Debug.Log(currentTarget);
                agent.SetDestination(rayhit.collider.transform.position + Vector3.up * 0.4f);
                currentState = KingStates.WALK;
            }
            else if (Physics.Raycast(ray.origin, ray.direction * 10000, out rayhit, 5000, pathLayerMask))
            {
                Vector3 final = rayhit.point;
                //Debug.Log(final);
                agent.SetDestination(final + Vector3.up * 0.4f);
                currentState = KingStates.WALK;
            }
            
            //clickPos1.z = 10;
            //Vector3 clickPos = Camera.main.ScreenToWorldPoint(clickPos1);
        }

        //Debug.Log("this" + this.transform.position);
        //Debug.Log("agent" + agent.destination);

        // if destination reached and there is target in range(in targets linked list) then attack else idle
        if (currentState == KingStates.WALK && this.transform.position == agent.destination + Vector3.up * 0.4f) {
            //Debug.Log("Dest. reached" + agent.destination + "");
            if (currentTarget != null) {
                currentState = KingStates.ATTACK;
            }
            else if (targets.Count > 0)
            {
                currentState = KingStates.ATTACK;
                currentTarget = targets.First.Value;
            }
            else
            {
                currentState = KingStates.IDLE;
            }
        }

        //shoot at regular interval
        if (currentState == KingStates.ATTACK && currentTarget != null)
        {
            if (Time.time > lastShootTime + timeBetweenShoots)
            {
                lastShootTime = Time.time;
                Shoot();
            }
            else
            {
                this.gameObject.GetComponent<Renderer>().material.color = originalColor;
            }
        }
        if (isFirstPerson)
        {
            transform.eulerAngles += new Vector3(0.0f, Input.GetAxis("Horizontal") * rotationSpeed, 0.0f);
            //transform.Translate(Vector3.forward * moveSpeed * Input.GetAxis("Vertical"));
        }

    }

    void OnTriggerEnter(Collider col) {
        try
        {
            if (col.gameObject.tag == "TowerBase")
            {
                Debug.Log("target entered");
                targets.AddLast(col.transform.parent.transform);
                //Debug.Log("registered");
                Entity towerEntity = targets.Last.Value.gameObject.GetComponent<Entity>();
                towerEntity.OnDeath += ChangeTarget;
                //targets.AddLast(col.gameObject.transform);
                if (currentTarget == null && currentState == KingStates.IDLE)//if king is idle on new entry then change to attack
                {
                    currentTarget = targets.First.Value;
                    currentState = KingStates.ATTACK;
                }
            }

            if (col.gameObject.transform.parent.tag == "BlockBarricade" && currentState == KingStates.WALK)
            {
                currentState = KingStates.ATTACK;
                currentTarget = col.gameObject.transform.parent;
            }

            //else if (col.gameObject.tag == "BlockBarricade" || col.gameObject.tag == "GroundBarricade")
            //{
            //    Debug.Log("target entered");
            //    targets.AddLast(col.transform);

            //    Entity BarricadeEntity = targets.Last.Value.gameObject.GetComponent<Entity>();
            //    BarricadeEntity.OnDeath += ChangeTarget;
            //    //targets.AddLast(col.gameObject.transform);
            //    if (currentTarget == null && currentState == KingStates.IDLE)//if king is idle on new entry then change to attack
            //    {
            //        currentTarget = targets.First.Value;
            //        currentState = KingStates.ATTACK;
            //    }
            //}
        }
        catch { }
    }

    void OnTriggerExit(Collider col) {
        try
        {
            if (currentTarget == col.gameObject.transform)
            {
                currentTarget = null;
                targets.Remove(col.gameObject.transform);
                Debug.Log("target removed");
                if (targets.Count > 0 && currentState == KingStates.ATTACK)
                {
                    currentTarget = targets.First.Value;
                }
            }
            else {
                targets.Remove(col.gameObject.transform);
            }
        }
        catch { }
    }

    void Shoot() {
        Debug.Log(currentTarget);
        this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        currentTarget.gameObject.GetComponent<Entity>().TakeDamage(damage);
    }

    void ChangeTarget(Transform t) {//called when tower dies
        //Debug.Log("changed");
        if (currentTarget == t)
        {
            currentTarget = null;
            targets.Remove(t);
            if (targets.Count > 0)
            {
                currentTarget = targets.First.Value;
            }
        }
        else
        {
            targets.Remove(t);
        }
    }

}
