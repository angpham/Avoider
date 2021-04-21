using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public PlayerController player;
    public EnemiesManager manager;

    public List<WaypointManager> waypoints = new List<WaypointManager>();
    public float speed = 1.0f;

    public int destinationWaypoint = 1;
    private Vector3 destination;
    private bool forwards = true;
    private float timePassed = 0f;

    private bool isChase = false;
    public float detectDistance = 50.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.destination = this.waypoints[destinationWaypoint].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChase && ((this.transform.position - player.transform.position).sqrMagnitude < detectDistance))
        {
            isChase = true;
        }

        if (isChase)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToPlayer());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(MoveToWaypoints());
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            manager.ResetEnemies();
            player.isCaught = true;
        }
    }

    IEnumerator MoveToPlayer()
    {
        while ((transform.position - player.transform.position).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, this.speed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveToWaypoints()
    {
        while ((transform.position - this.destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, this.destination, this.speed * Time.deltaTime);
            yield return null;
        }

        if ((transform.position - this.destination).sqrMagnitude <= 0.01f)
        {
            if (this.waypoints[destinationWaypoint].isSentry)
            {
                while (this.timePassed < this.waypoints[destinationWaypoint].pauseTime)
                {
                    this.timePassed += Time.deltaTime;
                    yield return null;
                }

                this.timePassed = 0;
            }

            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (this.waypoints[destinationWaypoint].isEndpoint)
        {
            if (this.forwards)
            {
                this.forwards = false;
            }
            else
            {
                this.forwards = true;
            }
        }

        if (this.forwards)
        {
            ++destinationWaypoint;
        }
        else
        {
            --destinationWaypoint;
        }

        if (destinationWaypoint >= this.waypoints.Count)
        {
            destinationWaypoint = 0;
        }

        this.destination = this.waypoints[destinationWaypoint].transform.position;
    }

    public void ResetEnemy()
    {
        this.isChase = false;
        this.destinationWaypoint = 1;
        this.transform.position = this.waypoints[0].transform.position;
    }
}
