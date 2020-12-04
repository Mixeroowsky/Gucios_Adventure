using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatBehaviour : MonoBehaviour
{
    public catState _currentState;
    public Animator anim;
    bool standing = false;
    public bool isAttacking = false;
    public float speed = 3f;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {        
        if(!standing && !isAttacking)
        {
           StartCoroutine("Behaviour");
        }
    }    
    public enum catState
    {
        StandStill,
        Idle,
        Walk,
        Attack
    }
    IEnumerator Behaviour()
    {
       
        while(!isAttacking)
        {

            standing = true;
            switch (_currentState)
            {
                case catState.StandStill:
                    {                        
                        anim.SetBool("Standing", true);
                        yield return new WaitForSeconds(3f);
                        standing = false;
                        anim.SetBool("Standing", false);
                        break;
                    }
                case catState.Idle:
                    {                        
                        anim.SetBool("Idle", true);
                        yield return new WaitForSeconds(3f);
                        anim.SetBool("Idle", false);
                        standing = false;
                        break;
                    }
                case catState.Walk:
                    {          
                        anim.SetBool("IsWalking", true);
                        float elapsedTime = 0f;
                        float duration = 20f;
                        float ratio = elapsedTime / duration;                        
                        while (ratio < 1.5f && !GameManager.isPaused)
                        {
                            elapsedTime += Time.fixedDeltaTime;
                            ratio = elapsedTime / duration;                            
                            transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(speed/200, 0, 0), 1f);
                            yield return null;
                        }                        
                        anim.SetBool("IsWalking", false);
                        standing = false;
                        break;
                    }
                case catState.Attack:
                    {                        
                        _currentState = catState.StandStill;
                        standing = false;
                        //isAttacking = false;
                        break;
                    }
            }
            _currentState = (catState)Random.Range(0, 3);
            yield break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "changeDirection")
        {            
            speed = -speed;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
    }
}
