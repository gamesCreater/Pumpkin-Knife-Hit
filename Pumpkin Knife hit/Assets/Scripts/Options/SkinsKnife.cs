using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkinsKnife : MonoBehaviour
{
    public KnifeType[] knifeType;
    public Image skinImage;
    public Button nextSkin;
    public int num;
    public GameObject knifePref;

    public Button ChooseSkinBut;
    public Button LockBut;

    public int Fruits;

    public TextMeshProUGUI fruitsCostsTxt;
    public TextMeshProUGUI fruitsCountTxt;



    private void Start()
    {
        num = 6;
        Fruits = PlayerPrefs.GetInt("TotalFruits");
        fruitsCostsTxt.text = knifeType[num].costs.ToString();
        fruitsCountTxt.text = Fruits.ToString();

    }

    public void NextSkin()
    {
        num++;

        if (num == knifeType.Length)
        {
            num = 0;
        }

        CheckLock();

        skinImage.sprite = knifeType[num].spriteKnife;

        fruitsCostsTxt.text = knifeType[num].costs.ToString();
        
    }

    public void ChooseKnife()
    {
        var i = knifePref.GetComponent<SpriteRenderer>();
        i.sprite = skinImage.sprite;

        SceneManager.LoadScene("GameScene");
    }

    public void CheckLock()
    {
        if (knifeType[num].lockSkin)
        {
            ChooseSkinBut.interactable = false;
            LockBut.gameObject.SetActive(true);
        }
        else
        {
            LockBut.gameObject.SetActive(false);
            ChooseSkinBut.interactable = true;            
        }
    }

    public void LockButton()
    {
        if(Fruits >= knifeType[num].costs)
        {
            // записываем открытие скина в PlayerPrefs
            Fruits -= knifeType[num].costs;
            fruitsCountTxt.text = Fruits.ToString();
            PlayerPrefs.SetInt("TotalFruits",Fruits);

            knifeType[num].lockSkin = false;
            CheckLock();
        }

        
    }

}
