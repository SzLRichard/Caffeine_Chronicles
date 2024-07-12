using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    GameObject playerObject;
    public GameObject projectile_prefab;
    EnemyMovement movementScript;
    float time_since_shot = 0f;
    float shooting_cooldown = 0.5f;
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        movementScript = gameObject.GetComponent<EnemyMovement>();
    }
    void FixedUpdate()
    {
        time_since_shot += Time.deltaTime;
        if(movementScript.lineOfSight)
            shoot();
    }
    IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        Destroy(obj);
    }
    void shoot() {
        if (time_since_shot < shooting_cooldown) return;
        Vector2 direction = (playerObject.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject projectile = Instantiate(projectile_prefab, transform.position, Quaternion.Euler(new Vector3(0, 0, angle + 90)));
        EnemyBullet projectileMovement = projectile.GetComponent<EnemyBullet>();

        if (projectileMovement != null)
        {
            projectileMovement.SetDirection(direction);
        }

        StartCoroutine(DestroyAfterDelay(projectile));
        time_since_shot = 0;
    }
}
