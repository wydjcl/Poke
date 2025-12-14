using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrarySO", menuName = "Card/CardLibrary")]
public class CardLibrarySO : ScriptableObject
{
    public List<CardLibraryEntry> cardLibraryList;
}

[System.Serializable]
public class CardLibraryEntry
{
    //每种卡牌有多少张
    public CardDataSO cardData;

    public int amount;
}