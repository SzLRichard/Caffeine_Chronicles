using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 25f;
    private Vector2 moveDirection;
    private CapsuleCollider2D capsuleCollider;
    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
    void Start()
    {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == ("Enemy")) {
            if(capsuleCollider!=null)
                Physics2D.IgnoreCollision(capsuleCollider, collision.collider);
        }
        if (collision.gameObject.CompareTag("Hole"))
        {
            if(capsuleCollider!=null)
                Physics2D.IgnoreCollision(capsuleCollider, collision.collider);
        }
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
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
