using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbKnife;
    public bool stuck;
    public static bool knifeCol;
    public ParticleSystem patric;

    public delegate void GameOver();
    public static event GameOver GameOverEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pumpkin"))
        {
            rbKnife.velocity = Vector3.zero;
            transform.parent = collision.transform;
            stuck = true;

            Handheld.Vibrate();
        }

        else if (collision.gameObject.CompareTag("Knife"))
        {
            if (collision.gameObject.GetComponent<Knife>().stuck)
            {
                rbKnife.velocity = Vector3.zero;                
                transform.parent = null;

                rbKnife.gravityScale = 10;
                gameObject.transform.Rotate(0, 0, 12);

                knifeCol = true;

                GameOverEvent.Invoke();

                Handheld.Vibrate();

            }            
        }

        else if (collision.gameObject.CompareTag("KnifeInPum"))
        {
            if (collision.gameObject.GetComponent<Knife>().stuck)
            {
                rbKnife.velocity = Vector3.zero;
                transform.parent = null;

                rbKnife.gravityScale = 10;
                gameObject.transform.Rotate(0, 0, 12);

                knifeCol = true;

                GameOverEvent?.Invoke();

                Handheld.Vibrate();

            }
        }
    }

    
}
