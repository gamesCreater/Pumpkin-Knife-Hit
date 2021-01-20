using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    private Rigidbody2D rbFruit;
    public GameObject destrFruit;

    public delegate void HitFruit();
    public event HitFruit HitFruitEvent;

    private void Start()
    {
        rbFruit = gameObject.GetComponent<Rigidbody2D>();

        Invoke("Trigger", 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            HitFruitEvent.Invoke();
            TouchFruit();
        }
    }

    void TouchFruit()
    {
        GameObject var = Instantiate(destrFruit, transform.position, Quaternion.identity);

        Destroy(gameObject);

        Destroy(var, 2f);
    }

    void Trigger()
    {
        var Trigger = gameObject.GetComponent<Collider2D>();
        Trigger.isTrigger = true;
    }

}
