using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;


public class PlayerMovement : MonoBehaviour
{
    //Very basic movement, good enough for prototyping
    public TextMeshProUGUI energyMeter;
    public Timer timerScrpit;
    public GameObject deathMenu;
    public float movementSpeed;
    public Camera main_camera;
    public GameObject projectile_prefab;
    float inputX, inputY;
    Rigidbody2D rigidBody;
    private float energy;
    private float time_since_shot = 0f;
    private float shooting_cooldown = 0.5f;
    private float ground = 1f;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        energy = movementSpeed;
    }

    IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        Destroy(obj);
    }
    
    void FixedUpdate()
    {
        time_since_shot += Time.deltaTime;
        
        if (energy > 60) energy = 0;
        if (energy < 10) movementSpeed = energy * ground;
        else movementSpeed = 10 * ground;
        if (energy > 0) energy -= 1 / 60f;
        else energy = 0;
        if (energy == 0) {
            deathMenu.SetActive(true);
            timerScrpit.setTimerActive(false);
        }
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        rigidBody.velocity = new Vector2(inputX, inputY).normalized * movementSpeed;
    }

    private void Update()
    {
        energyMeter.text = (energy>0?System.Math.Round(energy,2):0).ToString();
        bool held_down = false;
        if (Input.GetMouseButtonDown(0))
        {
            held_down = true;
        }
        if (held_down) Shoot();
        if (Input.GetMouseButtonUp(0))
        {
            held_down = false;
        }
    }

    void Shoot()
    {
        if (time_since_shot < shooting_cooldown) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        GameObject projectile = Instantiate(projectile_prefab, transform.position, Quaternion.identity);
        Bullet projectileMovement = projectile.GetComponent<Bullet>();

        if (projectileMovement != null)
        {
            projectileMovement.SetDirection(direction);
        }

        StartCoroutine(DestroyAfterDelay(projectile));
        time_since_shot = 0;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            if (other.transform.rotation.eulerAngles.z == 0)
            {
                int szorzo = -1;
                if (transform.position.x < other.transform.position.x) szorzo = 1;
                main_camera.transform.position += new Vector3(17.5f * szorzo, 0, 0);
                transform.position += new Vector3(2 * szorzo, 0, 0);
            }
            else
            {
                int szorzo = -1;
                if (transform.position.y < other.transform.position.y) szorzo = 1;
                main_camera.transform.position += new Vector3(0, 9 * szorzo, 0);
                transform.position += new Vector3(0, 2 * szorzo, 0);
            }
        }
        if (other.CompareTag("Coffee"))
        {
            other.gameObject.SetActive(false);
            energy += 20;
        }
        if (other.CompareTag("SpeedPill"))
        {
            other.gameObject.SetActive(false);
            energy += 40;
        }
        if (other.CompareTag("AttackPill"))
        {
            other.gameObject.SetActive(false);
            shooting_cooldown *= 0.8f;
        }
        if (other.CompareTag("Ice"))
        {
            movementSpeed *= 0.5f;
        }
        if (other.CompareTag("Ice"))
        {
            ground = 0.333f;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            energy -= 0.03f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            ground = 1f;
        }
    }
}
