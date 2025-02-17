using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bullet : MonoBehaviour
{
    public float speed = 25f;
    private Vector2 moveDirection;
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject ignorePlayer = GameObject.FindGameObjectWithTag("Player");
            Collider2D thisCollider = GetComponent<Collider2D>();
            Collider2D otherCollider = ignorePlayer.GetComponent<Collider2D>();
            if (otherCollider != null && thisCollider != null)
            {
                Physics2D.IgnoreCollision(thisCollider, otherCollider);
            }
        }
        if (collision.gameObject.CompareTag("Hole"))
        {
            GameObject[] ignoreHoles= GameObject.FindGameObjectsWithTag("Hole");
            Collider2D thisCollider = GetComponent<Collider2D>();
            foreach (GameObject hole in ignoreHoles)
            {
                Collider2D otherCollider = hole.GetComponent<Collider2D>();

                if (otherCollider != null && thisCollider != null)
                {
                    Physics2D.IgnoreCollision(thisCollider, otherCollider);
                }
            }
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            Destroy(gameObject);
        }
    }
}
