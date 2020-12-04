using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    GameObject player;
    public GameObject cinemachine;
    CanvasGroup alpha;
    float time = 0f;
    private CinemachineVirtualCamera vcam;
    bool isDead = true;
    Rigidbody2D playerRb;
    public static bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject Ui;
    NextLevel over;

    // Start is called before the first frame update
    void Start()
    {
        alpha = gameOverCanvas.GetComponentInChildren<CanvasGroup>();
        player = GameObject.Find("Gucio");
        playerRb = player.GetComponent<Rigidbody2D>();
        vcam = cinemachine.GetComponent<CinemachineVirtualCamera>();
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void FixedUpdate()
    {
       
        if (Player.health <= 0 && player != null && NextLevel.isOver == false)
        {
            
            gameOver();
        }       
        

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }


    public void gameOver()
    {
        
        vcam.Follow = null;
        player.GetComponent<Player>().enabled = false;
        float fullAlpha = 3f;
        time += Time.deltaTime;
        float ratio = time / fullAlpha;
        gameOverCanvas.SetActive(true);
        alpha.alpha = Mathf.Lerp(0, 1, ratio);
        Physics2D.IgnoreLayerCollision(8, 9);
        if (isDead)
        {
            if (playerRb.velocity.y < 0)
            {
                playerRb.AddForce(new Vector3(0, 10f, 0), ForceMode2D.Impulse);
            }
            else
            {
                playerRb.AddForce(new Vector3(0, 3f, 0), ForceMode2D.Impulse);
            }
            isDead = false;
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitToMenu()
    {
        SceneManager.LoadScene(0);
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Ui.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Ui.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    


}
