using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundScroll : MonoBehaviour
{
    public AudioSource sound;
    public Scrollbar scroll;
    public Button Back;

    private void Start()
    {
        sound = SoundProduced.instance.GetComponent<AudioSource>();  
    }

    private void Update()
    {
        sound.volume = scroll.value;
    }

    public void BackBut()
    {
        SceneManager.LoadScene("MainMenu");
    }




}
