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
    public AudioClip deadEnemySource;

    private PlayerController playerController;
    private AudioSource audioSource;
    private PolygonCollider2D polygonCollider2D;
    private bool isStartDeadEffect=false;
    private bool isDying = false;


    private void Awake()
    {
        playerController= PC.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        polygonCollider2D=GetComponent<PolygonCollider2D>();
    }

    private void FixedUpdate()
    {
        SpaceFire();
    }

    private void EnemySpaceStatus()
    {
        if (enemyHealth <= 0)
        {
            EnemyDead();
        }
    }

    private void EnemyDead()
    {
            Debug.Log("audio dead");
            isDying = true;
            audioSource.clip = deadEnemySource;
            audioSource.Play();
            polygonCollider2D.enabled = false;
            playerController.score++;
            Destroy(gameObject, deadEnemySource.length);
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
        if (collision.gameObject.tag == "playerBullet"&&!isDying)
        {
            enemyHealth--;
            Destroy(collision.gameObject);
            EnemySpaceStatus();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "playerBullet"&&!isDying)
        {
            enemyHealth--;
            Destroy(collision.gameObject);
            EnemySpaceStatus();

        }
    }


}
