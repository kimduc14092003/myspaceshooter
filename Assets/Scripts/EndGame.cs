using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private PlayerController playerController;
    public TMP_Text yourScore;
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Start()
    {
        yourScore.text = playerController.score + "";
    }

    public void OpenHomeScene()
    {
        SceneManager.LoadScene(0);
    }
    public void ReplayGame()
    {
        Application.LoadLevel(1);
        SceneManager.UnloadScene(2);
    }
}
