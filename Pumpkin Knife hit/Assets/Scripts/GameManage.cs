using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public GameObject pumpkinPref;
    public GameObject knifePref;
    public GameObject placePumpkin;
    public GameObject placeKnife;

    public GameObject pumpkinObj;

    public KnifeMover km;    

    public int Level;
    public int countThrow;
    private int maxThrow;
    public bool win = true;

    public AppleData[] fruitData;

    public Vector3 dirSpawn;
    public Vector3 fruitPlace;    

    private GameObject fruit;

    public Pumpkin pm;

    public GameObject[] KnifeSpawnPlaces;
    


    void Start()
    {
        dirSpawn = new Vector3(0, 1, 0);
        fruitPlace = placePumpkin.transform.position + dirSpawn;

        Level = 0;
        maxThrow = 13;
        countThrow = 5;

        LevelUp();
        win = false;        
    }

    
    void Update()
    {
        if (win)
        {
            Invoke("LevelUp", 2f);
            win = false;
        }
    }

    public void LevelUp()
    {
        Level++;

        PossibleThow();

        km.counterKnife = 0;

        pumpkinObj = Instantiate(pumpkinPref, placePumpkin.transform.position, Quaternion.identity);

        km.knifeObj = Instantiate(knifePref, placeKnife.transform.position, Quaternion.identity);

        km.knifeObj.transform.parent = placeKnife.transform;

        SpawnKnifeInPum();        

        if(Level != 0)
        {
            var fruitPref = GetRandomFruit();

            if (fruitPref != null)
            {
                fruit = Instantiate(fruitPref, fruitPlace, Quaternion.identity);

                fruit.transform.parent = pumpkinObj.transform;
                                
            }            
        }        
    }

    public GameObject GetRandomFruit()
    {
        //int summa = fruitData.Sum(f => f.probability);
        int genInt = Random.Range(0, 100);

        for (int i = 0; i < fruitData.Length; i++)
        {
            if (genInt < fruitData[i].probability)
            {
                return fruitData[i].prefab;
            }

            genInt -= fruitData[i].probability;

            
        }

        return null;
    }

    public void PossibleThow()
    {
        countThrow = Random.Range(8, maxThrow);
        //Debug.Log(countThrow);
    }

    public void SpawnKnifeInPum()
    {
        for(int i=0; i<3; i++)
        {
            var number = Random.Range(0, 100);            

            if ((number % 2) == 0)
            {                
                pumpkinObj.transform.GetChild(i).gameObject.SetActive(true);    
            }
        }    
        

    }

}
