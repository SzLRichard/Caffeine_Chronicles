using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed=3;
    GameObject playerObject;
    Vector2 target;
    public int health=3;
    public bool lineOfSight = false;

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
        RaycastHit2D [] rayHits = Physics2D.RaycastAll(transform.position, playerObject.transform.position - transform.position);
        foreach (RaycastHit2D hit in rayHits)
        {
            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Door") || hit.collider.CompareTag("BossDoor"))
                {
                    lineOfSight = false;
                    break;
                }
                else if (hit.collider.CompareTag("Player"))
                {
                    lineOfSight = true;
                    break;
                }
                else
                {
                    continue;
                }
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
