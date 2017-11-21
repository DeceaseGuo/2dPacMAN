using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int scoreAmount;
    public Text scoreText;
    public GameObject getReady;
    public GameObject[] lifes;
    private int leftLife = 3;
    [HideInInspector]
    public int feed = 185;
    private float countDown = 2.5f;
    PlayerMove playerMove;
    bool onlyOncee = false;

    private void Start()
    {
        playerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }

    void Update ()
    {
        scoreText.text = scoreAmount.ToString();

        if (countDown > 0)
        {
            countDown -= Time.deltaTime;

        }
        else
        {
            startGame();            
        }

        if (feed == 0)
        {
            Debug.Log("You Win!");
        }
	}

    void startGame()
    {
        if (!onlyOncee)
        {
            onlyOncee =!onlyOncee;
            getReady.SetActive(false);
            playerMove.gameStart = true;
            playerMove.ani.SetBool("Move", true);
        }
    }

    public void respawnGame()
    {
        if (onlyOncee)
        {
            countDown = 2.5f;
            onlyOncee = !onlyOncee;
            getReady.SetActive(true);
            playerMove.gameStart = false;
            playerMove.ani.SetBool("Move", false);            
        }
    }

    public void lifeReduce()
    {
        leftLife--;
        lifes[leftLife].SetActive(false);

        if (!lifes[0].activeSelf)
        {
            playerMove.finalLife = true;
            playerMove.gameStart = false;
            StartCoroutine(gameOver(.5f));
        }
    }

    IEnumerator gameOver(float time)
    {
        yield return new WaitForSeconds(time);        
        getReady.GetComponent<TextMesh>().text = "GAMEOVER";
        getReady.SetActive(true);
    }
}
