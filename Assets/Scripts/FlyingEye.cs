using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEye : MonoBehaviour
{

    public float flightSpeed = 2f;
    public float waypointReachedDistance = 0.1f;
    public DetectionZone biteDetectionZone;
    public Collider2D deathCollider;

    

    //public Transform target;

    

    //public List<Transform> waypoints;


    Animator animator;
    Damageable damageable;
    Rigidbody2D rb;
    
    //Transform nextWaypoint;
    //int wayPointNum = 0;

    public float nextWaypointDistance = 3f;

    Path path;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    GameObject target;

    public bool _hasTarget = false;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);

        }
    }

    private void Awake()
    {

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
        target = GameObject.FindGameObjectWithTag("Player");
    }


    private void Start()
    {
        
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        //nextWaypoint = waypoints[wayPointNum];
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;


    }


    public bool canMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }

    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (canMove)
            {
                Flight();

            }
            else
            {
                rb.velocity = Vector3.zero;
                
            }
        }



    }

    private void Flight()
    {

        // waypoints

        //Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        //float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        //rb.velocity = directionToWaypoint * flightSpeed;
        //UpdateDirection();

        //if (distance <= waypointReachedDistance)
        //{
        //    wayPointNum++;
        //    if (wayPointNum >= waypoints.Count)
        //    {
        //        wayPointNum = 0;
        //    }

        //    nextWaypoint = waypoints[wayPointNum];
        //}


        // new method
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;

        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * flightSpeed;
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        UpdateDirection();
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

    }

    private void UpdateDirection()
    {

        if (transform.localScale.x > 0)
        {
            //face to right
            if (rb.velocity.x < 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        //face to left
        else
        {
            if (rb.velocity.x > 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public void OnDeath()
    {
        // dead falls 
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }


    void OnDestroy()
    {
        if (GameObject.FindGameObjectWithTag("WaveSpawner") != null)
        {
            GameObject.FindGameObjectWithTag("WaveSpawner").GetComponent<WaveSpawner>().spawnedEnemies.Remove(gameObject);
        }


    }
}
