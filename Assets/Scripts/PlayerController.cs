using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float timeFire;

    public float PlayerHealth;
    public float fireDelay=0.1f;
    public float spaceSpeed;
    public GameObject SpaceBullet;
    public AudioClip SpaceBulletSound;
    public GameObject positionSpaceFire;
    public int score = 0;
    public TMP_Text yourScore;

    private void Awake()
    {
        rb= GetComponent<Rigidbody2D>();
        timeFire = 0;
    }
    private void Start()
    {
        Time.timeScale = 1;
        score = 0;
    }
    private void FixedUpdate()
    {
        SpaceMovement();
        SpaceFire();
        PlayerStatus();
        yourScore.text = "Score: " + score;
    }
    private void SpaceMovement()
    {
        float xAxis = Input.GetAxisRaw("Horizontal") * spaceSpeed;
        float yAxis = Input.GetAxisRaw("Vertical") * spaceSpeed;
        rb.velocity= new Vector2(xAxis, yAxis);
    }
    private void SpaceFire()
    {
        if (Time.time > timeFire)
        {
            Instantiate(SpaceBullet,positionSpaceFire.transform.position,Quaternion.Euler(Vector3.zero));
            timeFire = Time.time+fireDelay;
        }
    }

    private void PlayerStatus()
    {
        if (PlayerHealth <= 0)
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
            SceneManager.LoadScene(2, LoadSceneMode.Additive);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyBullet")
        {
            Destroy(collision.gameObject);
            PlayerHealth--;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemyBullet")
        {
            Destroy(collision.gameObject);
            PlayerHealth--;
        }
    }
}
