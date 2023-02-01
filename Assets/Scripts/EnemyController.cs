using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth;
    public float timeFire=0;
    public bool isFire = false;

    public float fireDelay = 0.1f;
    public GameObject enemySpaceBullet;
    public GameObject posEnemySpaceFire;

    public GameObject PC;
    private PlayerController playerController;

    private void Awake()
    {
        playerController= PC.GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        EnemySpaceStatus();
        SpaceFire();
    }

    private void EnemySpaceStatus()
    {
        if (enemyHealth <= 0)
        {
            Debug.Log("Enemy Dead!");
            Destroy(gameObject);
            playerController.score++;
        }
    }
    private void SpaceFire()
    {
        if (Time.time > timeFire)
        {
            timeFire += fireDelay;
            if (isFire)
            {
                Instantiate(enemySpaceBullet, posEnemySpaceFire.transform.position, Quaternion.Euler(new Vector3(180,0,0)));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerBullet")
        {
            enemyHealth--;
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerBullet")
        {
            enemyHealth--;
            Destroy(collision.gameObject);
        }
    }


}
