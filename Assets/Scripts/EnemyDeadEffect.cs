using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadEffect : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip deadEnemySource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        audioSource.clip = deadEnemySource;
        audioSource.Play();
        Destroy(gameObject,deadEnemySource.length);
    }
}
