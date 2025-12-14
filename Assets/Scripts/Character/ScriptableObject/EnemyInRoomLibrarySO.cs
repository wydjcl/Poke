using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInRoomLibrarySO", menuName = "Enemy/EnemyInRoomLibrary")]
public class EnemyInRoomLibrarySO : ScriptableObject
{
    public List<EnemyInOneLevel> EnemyInOneLevelList;// 一大关里面敌人组合
}

[System.Serializable]
public struct EnemyInOneLevel//一关里面的怪物组合
{
    //每种卡牌有多少张
    public GameObject enemy1;

    public GameObject enemy2;
    public GameObject enemy3;
}