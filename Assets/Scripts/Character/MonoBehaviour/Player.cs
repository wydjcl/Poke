using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    // public IntVariable playerMana;
    public static Player Instance;

    public int maxMana;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /*   public int CurrentMana
       {
         *//*  get => playerMana.currentValue;
           set => playerMana.SetValue(value);//读取数值的时候,触发事件,传递value这个数值//这里触发playerchangemanaintevent,需监听*//*
       }*/

    private void OnEnable()
    {
        /*   playerMana.maxValue = maxMana;
           CurrentMana = playerMana.maxValue;//设置初始法力值*/
    }

    /// <summary>
    /// 监听事件函数
    /// </summary>
    public void NewTurn()
    {
        //CurrentMana = maxMana;
    }

    public void UpdateMana(int cost)
    {
        /*   CurrentMana -= cost;
           if (CurrentMana <= 0)
           {
               CurrentMana = 0;
           }*/
    }
}