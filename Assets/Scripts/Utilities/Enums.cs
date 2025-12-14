using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//枚举 房间类型
//[Flags]//枚举多选
public enum RoomType
{
    /* MinorEnemy = 1,//小怪
     EliteEnem = 2,//精英怪
     Shop = 4,//商店
     Treasure = 8,//宝箱
     RestRoom = 16,//休息
     Boss = 32*/
    NormalEnemy,//小怪
    BigEnemy,//精英怪
    Shop,//商店
    Treasure,//宝箱
    PetBar,
    RestRoom,//休息
    Boss
}

public enum RoomState
{
    Locked,//被锁定的
    Visited,//被访问过
    Attainable//可访问
}

public enum CardType
{
    Attack,//攻击
    Ability,//单效果
    PAffect,
    Affect,
    Pet
}

public enum EffectTargetType
{
    Self,
    Target,
    All
}

public enum CardRarity
{
    Ordinary,
    Rare,
    Epic,
    Legend
}

public enum EquipmentType
{
    大剑,
    小剑
}

public enum PlayerType
{
    Princess,
    Prince
}