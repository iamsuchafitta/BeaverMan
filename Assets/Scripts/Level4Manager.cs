using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4Manager : MonoBehaviour
{
    private Animator SpringAnimator;
    public float JumpForse;

    private void Start()
    {
        SpringAnimator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("FirstPlayer") || collision.transform.CompareTag("SecondPlayer"))
        {
            SpringAnimator.SetBool("isWorking", true);
        }
        if (collision.relativeVelocity.y <= 0f)
        {
            Rigidbody2D rb = collision.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 velocity = rb.velocity;
                velocity.y = JumpForse;
                rb.velocity = velocity;
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("FirstPlayer") || collision.transform.CompareTag("SecondPlayer"))
        {
            SpringAnimator.SetBool("isWorking", false);
        }
    }

    

}
