using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetDataSO", menuName = "Pet/PetData")]
public class PetDataSO : ScriptableObject
{
    //静态
    public int ID;

    public string petName;
    public GameObject petPrefab;
    public int health;
    public int attack;
    public int maxMana;

    [TextArea]
    public string skillDes;

    [TextArea]
    public string smallSkill;

    [TextArea]
    public string bigSkill;

    [TextArea]
    public string exSkill;

    [TextArea]
    public string des;

    //TODO 执行的效果
}