using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed=3;
    GameObject playerObject;
    Vector2 target;
    int health=3;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
        target = new Vector2(playerObject.transform.position.x, playerObject.transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        if (health == 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
        }
    }

}
