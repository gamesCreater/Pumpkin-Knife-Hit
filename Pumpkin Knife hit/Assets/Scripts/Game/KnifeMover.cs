using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeMover : MonoBehaviour
{    
    [SerializeField] private GameObject knifePref;
    public GameObject knifeObj;
    public int counterKnife;

    [SerializeField] private float Speed;

    public Knife kn;
    public GameManage gm;

    void Start()
    {        
        Speed = 25f; // начальная скорость
    }
    
    private void Update()
    {     
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);

            if (/*touch.phase == TouchPhase.Began*/ Input.GetMouseButtonDown(0))
            {

                if (!kn.stuck && knifeObj != null)
                {
                    knifeObj.GetComponent<Rigidbody2D>().AddForce(transform.up.normalized * Speed, ForceMode2D.Impulse);

                    CounterKnife();
                }
            }
        //}
    }

    public void CounterKnife()
    {
        if (counterKnife != gm.countThrow - 1)
        {
            knifeObj = Instantiate(knifePref, transform);

            counterKnife++;
        }
    }
}
