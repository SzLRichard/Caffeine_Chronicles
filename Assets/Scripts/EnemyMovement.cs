using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed=3;
    GameObject playerObject;
    Vector2 target;
    int health=3;
    bool lineOfSight = false;

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (lineOfSight)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerObject.transform.position, movementSpeed * Time.deltaTime);
        }
        if (health <= 0)
            Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerObject.transform.position - transform.position);
        if (ray.collider != null) {
            Debug.Log(ray.collider.tag);
            lineOfSight = ray.collider.CompareTag("Player");
            if (lineOfSight) {
                Debug.DrawRay(transform.position, playerObject.transform.position - transform.position,Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, playerObject.transform.position - transform.position, Color.red);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
        }
    }
}
