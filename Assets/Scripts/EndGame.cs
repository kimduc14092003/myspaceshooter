using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private PlayerController playerController;
    public TMP_Text yourScore;
    public GameObject loseGame;
    public GameObject winGame;
    private void Awake()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    private void Start()
    {
        yourScore.text = playerController.score + "";
        StatusOfEndGame();
    }

    private void StatusOfEndGame()
    {
        if (playerController.score >= 16)
        {
            winGame.SetActive(true);
            loseGame.SetActive(false);
        }
        else
        {
            winGame.SetActive(false);
            loseGame.SetActive(true);
        }
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
