using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Apple", menuName = "Create Apple", order = 51)]
public class AppleData : ScriptableObject
{
    [SerializeField] public string fruit;
    [SerializeField] public int costPoint;
    [SerializeField] public Sprite icon;
    [SerializeField] public int probability;
    public GameObject prefab;
   
}
