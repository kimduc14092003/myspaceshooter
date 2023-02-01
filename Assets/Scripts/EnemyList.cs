using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemyList : MonoBehaviour
{
    public List<GameObject> listEnemy;
    public List<Vector3> listNewPos;
    public float[] listNewSpeed;
    public float spaceEnemySpeed;
    public float timeToMoveToNewPos = 2f;
    public GameObject ShieldPower;
    public float timeToChangeShapedTeam = 7;

    public bool isStartGame = false;
    public bool isTriangleTeam=false;
    public bool isSquareTeam = false;
    public bool isLogenzeTeam = false;
    public bool isRectangleTeam=false;

    private bool isMoving;
    private string[] listShapedTeam;
    private int currentShapedTeam=0;


    private void Awake()
    {
        getAllEnemySpace();
        listNewPos = new List<Vector3>(listEnemy.Count);
        listNewSpeed=new float[listEnemy.Count];
        listShapedTeam = new string[4] {  "SquareTeam", "LogenzeTeam", "TriangleTeam", "RectangleTeam" };
    }
    private void Start()
    {
        createStartValueForList();
        setRandomTimeFireStart();
    }

    private void FixedUpdate()
    {
        moveEnemyToPos();
        ChangeShapedTeam();
        testShapedTeam();
        isPlayerWin();
    }

    private void isPlayerWin()
    {
        int count = 0;
        for(int i = 0; i < listEnemy.Count; i++)
        {
            if (listEnemy[i])
            {
                count++;
            }
        }
        if (count == 0)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);
        }
    }

    private void ChangeShapedTeam()
    {
        if (Time.time % timeToChangeShapedTeam == 0)
        {
            switch (listShapedTeam[currentShapedTeam++])
            {
                case "TriangleTeam":
                    isTriangleTeam = true;
                    break;
                case "SquareTeam":
                    isSquareTeam = true;
                    break;
                case "LogenzeTeam":
                    isLogenzeTeam = true;
                    break;
                case "RectangleTeam":
                    isRectangleTeam = true;
                    break;
            }
            if (currentShapedTeam > 3)
            {
                currentShapedTeam = 0;
            }
            
        }
    }
    private void testShapedTeam()
    {
        if (isTriangleTeam)
        {
            moveToTriangleShapedTeam();
            isTriangleTeam = false;
        }
        else if (isSquareTeam)
        {
            moveToSquareShapedTeam();
            isSquareTeam = false;
        }
        else if (isLogenzeTeam)
        {
            moveToLogenzeShapedTeam();
            isLogenzeTeam = false;
        }
        else if (isRectangleTeam)
        {
            moveToRectangleShapedTeam();
            isRectangleTeam = false;
        }
    }

    private void setRandomTimeFireStart()
    {
        for(int i = 0; i < listEnemy.Count; i++)
        {
            listEnemy[i].GetComponent<EnemyController>().timeFire = Random.Range(1f, 4f);
        }
    }
    private void getAllEnemySpace()
    {
        Transform[] allChildren=this.transform.GetComponentsInChildren<Transform>(true);
        for(int i = 0; i < allChildren.Length; i++)
        {
            if (allChildren[i].tag == "enemy")
            {
                listEnemy.Add(allChildren[i].gameObject);
            }
        }
    }

    private void moveToSquareShapedTeam()
    {
        isMoving = true;
        isMovementToShapedTeam();

        listNewPos.Clear();

        float xPositionStart = -1.8f;
        float yPositionStart = 4f;
        float distanceHorizontal = 1.2f;
        float distanceVertical = 1f;
        for (int i = 0; i < listEnemy.Count; i++)
        {
            Vector3 newEnemyPos = new Vector3(xPositionStart+(i%4)* distanceHorizontal, yPositionStart-(i/4)* distanceVertical, 0);
            listNewPos.Add(newEnemyPos);
        }
        getListSpeed();

    }

    private void moveToTriangleShapedTeam()
    {
        isMoving = true;
        isMovementToShapedTeam();
        
        listNewPos.Clear();

        float xPositionStart = 0;
        float yPositionStart = 4;

        int altitudeOfTriangle= 5;

        float distanceHorizontal = 0.625f;
        float distanceVertical = 0.5f;

        for (int i = 0; i < altitudeOfTriangle-1; i++)
        {
            for(int j = 0; j < altitudeOfTriangle*2-1; j++)
            {
                if (j == (altitudeOfTriangle - 1) - i || j == (altitudeOfTriangle - 1) + i)
                {
                    Vector3 newEnemyPos = new Vector3(xPositionStart + (j - (altitudeOfTriangle - 1)) * distanceHorizontal, yPositionStart - i * distanceVertical, 0);
                    listNewPos.Add(newEnemyPos);
                }
            }
        }
        for(int i = 0; i < 2*altitudeOfTriangle-1; i++)
        {
            Vector3 newEnemyPos = new Vector3(xPositionStart + (-1*(altitudeOfTriangle-1)+i) * distanceHorizontal, yPositionStart - (altitudeOfTriangle-1) * distanceVertical, 0);
            listNewPos.Add(newEnemyPos);
        }
        getListSpeed();

    }

    private void moveToLogenzeShapedTeam()
    {
        isMoving = true;
        isMovementToShapedTeam();

        listNewPos.Clear();

        float xPositionStart = 0;
        float yPositionStart = 4;

        float distanceHorizontal = 0.8f;
        float distanceVertical = 0.8f;

        int enemyTeamShapedHeight = 5;
        int enemyTeamShapedWidth = 7;
        for(int i = 0; i < enemyTeamShapedHeight; i++)
        {
            for(int j = 0; j < enemyTeamShapedWidth; j++)
            {
                if ((i == 0||i==enemyTeamShapedHeight-1) && j == enemyTeamShapedWidth / 2)
                {
                    Vector3 newEnemyPos = new Vector3(xPositionStart, yPositionStart - i * distanceVertical, 0);
                    listNewPos.Add(newEnemyPos);
                }
                else if ((i == 1 || i == 3) && j != 0 && j != enemyTeamShapedWidth / 2 && j != enemyTeamShapedWidth - 1)
                {
                    Vector3 newEnemyPos = new Vector3(xPositionStart-(enemyTeamShapedWidth /2)*distanceHorizontal+j*distanceHorizontal, yPositionStart - i * distanceVertical, 0);
                    listNewPos.Add(newEnemyPos);
                }
                else if (i == 2 && j != enemyTeamShapedWidth / 2)
                {
                    Vector3 newEnemyPos = new Vector3(xPositionStart - (enemyTeamShapedWidth / 2) * distanceHorizontal + j * distanceHorizontal, yPositionStart - i * distanceVertical, 0);
                    listNewPos.Add(newEnemyPos);
                }
            }
        }
        getListSpeed();

    }

    private void moveToRectangleShapedTeam()
    {

        isMoving = true;
        isMovementToShapedTeam();

        listNewPos.Clear();

        float xPositionStart = 0;
        float yPositionStart = 3;

        float distanceHorizontal = 0.8f;
        float distanceVertical = 0.8f;

        int enemyTeamShapedHeight = 3;
        int enemyTeamShapedWidth = 7;

        for(int i = 0; i < enemyTeamShapedHeight; i++)
        {
            for (int j=0; j < enemyTeamShapedWidth; j++)
            {
                if (i == 0 || i == enemyTeamShapedHeight - 1)
                {
                    Vector3 newEnemyPos = new Vector3(xPositionStart - (enemyTeamShapedWidth / 2) * distanceHorizontal + j * distanceHorizontal, yPositionStart - i * distanceVertical, 0);
                    listNewPos.Add(newEnemyPos);
                } 
                else if(j == 0 || j == enemyTeamShapedWidth - 1)
                {
                    Vector3 newEnemyPos = new Vector3(xPositionStart - (enemyTeamShapedWidth / 2) * distanceHorizontal + j * distanceHorizontal, yPositionStart - i * distanceVertical, 0);
                    listNewPos.Add(newEnemyPos);
                }
            }
        }
        getListSpeed();

    }

    private void moveEnemyToPos()
    {
        if (isStartGame)
        {
            for(int i = 0; i < listEnemy.Count; i++)
            {

                if (!listEnemy[i])
                {
                    continue;
                }
                else
                {
                    listEnemy[i].transform.position = Vector3.MoveTowards(listEnemy[i].transform.position, listNewPos[i], listNewSpeed[i] * Time.deltaTime);
                }
            }
        }
    }

    private void isMovementToShapedTeam()
    {
        if (isMoving)
        {
            ShieldPower.SetActive(true);

            for (int i = 0; i < listEnemy.Count; i++)
            {
                if (listEnemy[i])
                {
                    listEnemy[i].GetComponent<EnemyController>().isFire=false;
                }
            }

            Invoke("stopMovingMovement", 2f);
        }
        else
        {
            ShieldPower.SetActive(false);

            for (int i = 0; i < listEnemy.Count; i++)
            {
                if (listEnemy[i])
                {
                    listEnemy[i].GetComponent<EnemyController>().isFire = true;
                }
            }
        }
    }

    private void stopMovingMovement()
    {
        isMoving=false;
        isMovementToShapedTeam();
    }

    private void getListSpeed()
    {
        for(int i = 0; i < listEnemy.Count; i++)
        {
            if (listEnemy[i])
            {
                float speed = Vector3.Distance(listEnemy[i].transform.position, listNewPos[i]) / timeToMoveToNewPos;
                listNewSpeed[i] = speed;
            }
        }
    }

    private void createStartValueForList()
    {
        for(int i=0; i < listEnemy.Count; i++)
        {
            listNewPos.Add(Vector3.zero);
        }
    }
}
