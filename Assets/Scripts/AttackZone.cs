using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private CatBehaviour state;
    public GameObject player;
    Rigidbody2D playerRb;
    AudioSource audioSour;
    public AudioClip playerHit;
    public AudioClip catAttack;

    // Start is called before the first frame update
    void Start()
    {
        state = GetComponentInParent<CatBehaviour>();
        playerRb = player.GetComponent<Rigidbody2D>();
        audioSour = player.GetComponent<AudioSource>();
    }

   
    

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && state._currentState != CatBehaviour.catState.Idle && player != null)
        {
            state._currentState = CatBehaviour.catState.Attack;
            state.anim.SetTrigger("Attack");
            audioSour.PlayOneShot(playerHit);
            audioSour.PlayOneShot(catAttack);
            Player.health -= 30;
            playerRb.velocity = Vector3.zero;
            Physics2D.IgnoreLayerCollision(9, 10);
            StartCoroutine("Flashing");
            if (player.transform.localScale.x < 0)
            {
                playerRb.AddForce(new Vector3(-1.5f, 3f, 0), ForceMode2D.Impulse);
            }
            else
            {                
                playerRb.AddForce(new Vector3(1.5f, 3f, 0), ForceMode2D.Impulse);                
            }

            yield return null;

        }
    }

    IEnumerator Flashing()
    {
        
        for (int i = 0; i < 6; i++)
        {
            if (player != null)
            {
                player.GetComponent<SpriteRenderer>().enabled = false;
                yield return new WaitForSeconds(.2f);
                player.GetComponent<SpriteRenderer>().enabled = true;
                yield return new WaitForSeconds(.2f);
            }
        }
        

        

        Physics2D.IgnoreLayerCollision(9, 10, false);
    }
}
