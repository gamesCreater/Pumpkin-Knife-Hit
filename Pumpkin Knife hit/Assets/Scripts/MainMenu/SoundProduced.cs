using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundProduced : MonoBehaviour
{
    public static SoundProduced instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    
}
