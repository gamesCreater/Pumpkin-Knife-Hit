using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    public Rigidbody2D rbPumpkin;
    private float speedRotate;
    private Vector3 directionRotate;
    private Quaternion rotationPumpkin;

    private bool isShaking;
    private Vector2 initialPosition;
    private float shakePower = 0.05f;

    [SerializeField] private GameObject destrucktPumpkinPref;    

    public Knife kn;
    public GameManage gm;

    
    

    void Start()
    {
        gm = Camera.main.GetComponent<GameManage>();        

        directionRotate = new Vector3(0, 0, 1); // задаем направление
        speedRotate = 2; // задаем скорость вращения
        rotationPumpkin = Quaternion.AngleAxis(speedRotate, directionRotate);

        initialPosition = transform.position;
    }

    private void Update()
    {
        if (transform.childCount == gm.countThrow)
        {
            //transform.childCount == gm.countThrow
            gm.win = true;
            ExplodePumpkin();

            Debug.Log("WIN");

            

        }

        if(isShaking == true)
        {
            transform.position = initialPosition + UnityEngine.Random.insideUnitCircle * shakePower;
        }
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
    }   


}
