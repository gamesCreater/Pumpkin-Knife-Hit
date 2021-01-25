using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Knife", menuName = "Create Knife", order = 51)]
public class KnifeType : ScriptableObject
{
    public string nameKnife;
    public Sprite spriteKnife;
    public bool lockSkin;
    public int costs;
}
