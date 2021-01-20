using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodePumpkin : MonoBehaviour
{
    public Vector3 directionForce;

    public float spin;
    public float speed;

    public Rigidbody2D rbPumpkinParts;

    void Start()
    {
        
        rbPumpkinParts.AddForce(directionForce * speed);
        rbPumpkinParts.AddTorque(spin);
    }

}
