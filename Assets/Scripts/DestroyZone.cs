using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    public GameObject gucio;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            gucio.GetComponent<Rigidbody2D>().gravityScale = 0;
            gucio.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            gucio.GetComponent<SpriteRenderer>().enabled = false;
            Player.health = 0;
        }        
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
