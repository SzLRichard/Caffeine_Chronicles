using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Very basic movement, good enough for prototyping
    public float movementSpeed;
    public Camera main_camera;
    public GameObject projectile_prefab;
    float inputX, inputY;
    Rigidbody2D rigidBody;
    private float energy;
    private float time_since_shot = 0f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        energy = movementSpeed;
    }

    void FixedUpdate()
    {
        time_since_shot += Time.deltaTime;

        if (energy > 60) energy = 0;
        if (energy < 10) movementSpeed = energy;
        else movementSpeed = 10;
        if (energy > 0) energy -= 1 / 60f;
        else energy = 0;

        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector2(inputX, inputY).normalized * movementSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("aAA");
            Shoot();
        }
    }

    void Shoot()
    {
        if (time_since_shot < 0.5f) return;
        GameObject projectile = Instantiate(projectile_prefab, transform.position, transform.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * 10f;
        time_since_shot = 0;
        Debug.Log("uefj");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DoorX"))
        {
            if (transform.position.x < other.transform.position.x)
            {
                main_camera.transform.position += new Vector3(17.5f, 0, 0);
                transform.position += new Vector3(2, 0, 0);
            }
            else
            {
                main_camera.transform.position += new Vector3(-17.5f, 0, 0);
                transform.position += new Vector3(-2, 0, 0);
            }
        }
        if (other.CompareTag("DoorY"))
        {
            if (transform.position.y < other.transform.position.y)
            {
                main_camera.transform.position += new Vector3(0, 9, 0);
                transform.position += new Vector3(0, 2, 0);
            }
            else
            {
                main_camera.transform.position += new Vector3(0, -9, 0);
                transform.position += new Vector3(0, -2, 0);
            }
        }
        if (other.CompareTag("Coffee"))
        {
            other.gameObject.SetActive(false);
            energy += 20;
        }
    }
}
