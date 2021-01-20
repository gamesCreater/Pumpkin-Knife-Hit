using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    public Rigidbody2D rbPumpkin;
    private float speedRotate;
    private Vector3 directionRotate;
    public Quaternion rotationPumpkin;

    private bool isShaking;
    private Vector2 initialPosition;
    private float shakePower = 0.05f;

    [SerializeField] private GameObject destrucktPumpkinPref;    

    public Knife kn;
    public GameManage gm;

    public delegate void EvNextLvl();
    public static event EvNextLvl NextLvlEvent;

    public delegate void EvHits();
    public static event EvHits HitEvent;

    public int countHitsInPum;

    


    void Start()
    {       
        HitEvent += CheckWin;
        GameManage.ChangeDirEvent += RotationPum;

        gm = GameObject.Find("GameManager").GetComponent<GameManage>();           

        initialPosition = transform.position;

        RotationPum();
    }

    private void Update()
    {      
        if (isShaking == true)
        {
            transform.position = initialPosition + UnityEngine.Random.insideUnitCircle * shakePower;
        }        
    }

    public void RotationPum()
    {
        if (gm.Level < 4)
        {
            directionRotate = new Vector3(0, 0, 1); // задаем направление
            speedRotate = 2; // задаем скорость вращения
        }
        else
        {
            int z = UnityEngine.Random.Range(-1, 1);
            if (z == 0)
            {
                z = 1;
            }

            directionRotate = new Vector3(0, 0, z); // задаем направление
            
            speedRotate = UnityEngine.Random.Range(0.5f, 5f); // задаем скорость вращения
            
        }       

        rotationPumpkin = Quaternion.AngleAxis(speedRotate, directionRotate);

    }

    void FixedUpdate()
    {        
        if (!Knife.knifeCol)
        {            
            transform.rotation *= rotationPumpkin;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Knife"))
        {
            isShaking = true;

            Invoke("StopShaking", 0.1f);

            countHitsInPum += 1;

            HitEvent.Invoke();
        }
    }

    void StopShaking()
    {
        isShaking = false;
        transform.position = initialPosition;
    }

    void ExplodePumpkin()
    {
        GameObject var = Instantiate(destrucktPumpkinPref, gameObject.transform.position, Quaternion.identity);

        Destroy(gameObject);

        Destroy(var, 2f);

        NextLvlEvent.Invoke();

        HitEvent -= CheckWin;
                
    }   

    public void CheckWin()
    {
        if (countHitsInPum == gm.countThrow)
        {
            Invoke("ExplodePumpkin", 0.1f);

            Handheld.Vibrate();
        }
    }

    public void UnsubscribePum()
    {
        HitEvent -= CheckWin;
        GameManage.ChangeDirEvent -= RotationPum;
    }


        


}
