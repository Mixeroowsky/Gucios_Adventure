using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class Bone : MonoBehaviour
{
    public float speed;
    AudioManager audioManager;
    public AudioClip eat;
    void Start()
    {       
        audioManager = AudioManager.instance;
        if(audioManager == null)
        {
            Debug.LogError("No audio");
        }
    }
    void FixedUpdate()
    {
        Float();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && Player.health != 90)
        {
            Player.health += 30;
            audioManager.PlaySound("eatSound");
            Destroy(this.gameObject);
        }
    } 
    private void Float()
    {
        transform.Translate(0f,Mathf.Sin(speed * Time.time)/1000,0f);        
    }
}
