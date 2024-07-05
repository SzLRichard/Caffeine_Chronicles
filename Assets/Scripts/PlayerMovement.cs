using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Very basic movement, good enough for prototyping
    public float movementSpeed;
    float inputX, inputY;
    Rigidbody2D rigidBody;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector2(inputX, inputY).normalized * movementSpeed;
    }
}
