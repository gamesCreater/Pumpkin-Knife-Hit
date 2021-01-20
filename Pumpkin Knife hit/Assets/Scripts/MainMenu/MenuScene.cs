using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScene : MonoBehaviour
{
    public Button StartBut;
    public Button Options;    
    

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OptionsBut()
    {
        SceneManager.LoadScene("OptionsGam");
    }



}
