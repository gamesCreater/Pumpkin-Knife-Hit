using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public GameObject pumpkinPref;
    public GameObject knifePref;
    public GameObject placePumpkin;
    public GameObject placeKnife;

    public GameObject pumpkinObj;

    public KnifeMover km;
    public Pumpkin pm;
    public Fruit fr;

    public int Level;
    public int countThrow;
    private int maxThrow;    

    public AppleData[] fruitData;
    private int whichFruit;        

    private Vector3 dirSpawn;
    private Vector3 fruitPlace;    

    private GameObject fruit;

    private int currentValue = 0;
    private int Record;
    
    private int TotalFruits;

    public delegate void ChangeDirPumRot();
    public static event ChangeDirPumRot ChangeDirEvent;

    public GameObject looseBg;

    public TextMeshProUGUI fuitsCounter;
    public TextMeshProUGUI score;
    public TextMeshProUGUI throwCounter;

    public TextMeshProUGUI endScoreTxt;
    public TextMeshProUGUI endRecordTxt;
    public GameObject endScore;
    public GameObject endRecord;

    public Button restartGameBut;

    void Start()
    {
        Pumpkin.HitEvent += CounterRecord;

        Pumpkin.NextLvlEvent += Invoker;
        Pumpkin.NextLvlEvent += PossibleThow;        
        Pumpkin.NextLvlEvent += LevelUp;

        Knife.GameOverEvent += LooseBg;
        Knife.GameOverEvent += EndShowText;

        dirSpawn = new Vector3(0, 1, 0);
        fruitPlace = placePumpkin.transform.position + dirSpawn;

        Level = 1;
        maxThrow = 13;
        countThrow = 7;

        Record = PlayerPrefs.GetInt("RecordPlayer");
        TotalFruits = PlayerPrefs.GetInt("TotalFruits");

        SpawnObj();              
    }

    private void Update()
    {
        fuitsCounter.text = TotalFruits.ToString();
        score.text = currentValue.ToString();
        throwCounter.text = (countThrow - km.counterKnife).ToString();
        endScoreTxt.text = currentValue.ToString();
        endRecordTxt.text = Record.ToString();

    }

    public void LevelUp()
    {
        Level++;        

        km.counterKnife = 0;

        if((Level % 10) == 0)
        {
            TotalFruits += 50;
        }

        if ((Level % 4) == 0)
        {
            ChangeDirEvent.Invoke();            
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
                whichFruit = i;
                return fruitData[i].prefab;
            }

            genInt -= fruitData[i].probability;
        }

        return null;
    }

    public void PossibleThow()
    {
        countThrow = Random.Range(8, maxThrow);
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

    public void Invoker()
    {
        Invoke("SpawnObj", 2f);        
    }

    public void SpawnObj()
    {
        pumpkinObj = Instantiate(pumpkinPref, placePumpkin.transform.position, Quaternion.identity);

        SpawnKnifeInPum();

        km.knifeObj = Instantiate(knifePref, placeKnife.transform.position, Quaternion.identity);

        km.knifeObj.transform.parent = placeKnife.transform;

        if (Level != 0)
        {
            var fruitPref = GetRandomFruit();

            if (fruitPref != null)
            {
                fruit = Instantiate(fruitPref, fruitPlace, Quaternion.identity);

                fruit.transform.parent = pumpkinObj.transform;

                fr = fruit.GetComponent<Fruit>();

                fr.HitFruitEvent += FruitsCounter;
            }
        }
    }

    public void CounterRecord()
    {      
        currentValue += 1;

        if (currentValue > Record)
        {
            Record = currentValue;
        }
    }

    public void FruitsCounter()
    {
        TotalFruits += fruitData[whichFruit].costPoint;

        fr.HitFruitEvent -= FruitsCounter;
    }   

    public void LooseBg()
    {
        StartCoroutine("LooseWindow");
    }

    IEnumerator LooseWindow()
    {
        for (float i = looseBg.transform.position.y; i>0; i-= 0.8f)
        {
            looseBg.transform.position = new Vector3(0, i, 0);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator WaitPlz()
    {        
        yield return new WaitForSeconds(1.5f);
        endScore.gameObject.SetActive(true);
        restartGameBut.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.8f);
        endRecord.gameObject.SetActive(true);
    }

    public void EndShowText()
    {
        StartCoroutine("WaitPlz");
    }

    public void ReatartGame()
    {

        SaveResults();

        SceneManager.LoadScene("GameScene");
        Knife.knifeCol = false;

        Unsubscribe();
        pm.UnsubscribePum();
    }

    public void SaveResults()
    {
        var i = PlayerPrefs.GetInt("TotalFruits");
        if (TotalFruits > i)
        {
            PlayerPrefs.SetInt("TotalFruits", TotalFruits);
        }

        var j = PlayerPrefs.GetInt("Record");
        if (Record > j)
        {
            PlayerPrefs.SetInt("RecordPlayer", Record);
        }
    }

    public void Unsubscribe()
    {
        Pumpkin.HitEvent -= CounterRecord;

        Pumpkin.NextLvlEvent -= Invoker;
        Pumpkin.NextLvlEvent -= PossibleThow;
        Pumpkin.NextLvlEvent -= LevelUp;

        Knife.GameOverEvent -= LooseBg;
        Knife.GameOverEvent -= EndShowText;
    }

}
