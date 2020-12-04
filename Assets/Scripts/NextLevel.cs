using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class NextLevel : MonoBehaviour
{
    public GameObject cinemachine;
    private CinemachineVirtualCamera vcam;
    public GameObject Win;
    public Transform player;
    public static bool isOver = false;
    AudioManager audioManager;
    void Start()
    {
        vcam = cinemachine.GetComponent<CinemachineVirtualCamera>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio");
        }
    }
    void FixedUpdate()
    {
        if(isOver && player != null)
        {
            player.Translate(new Vector3(.1f, 0, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.StopSound("music");
        audioManager.PlaySound("winSound");
        if(collision.gameObject.tag == "Player")        
        {
            StartCoroutine("WinText");
        }
    }
    IEnumerator WinText()
    {
        vcam.Follow = null;
        player.GetComponent<Animator>().SetFloat("Speed", 1f);
        player.GetComponent<Player>().enabled = false;
        isOver = true;
        Win.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        audioManager.PlaySound("music");
        isOver = false;
    }
}
