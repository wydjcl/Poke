using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardInRewardLibrarySO", menuName = "Card/CardInRewardLibrary")]
public class CardInRewardLibrarySO : ScriptableObject
{
    public List<Rewardcard> rewardcardList;// 卡组列表
}

[System.Serializable]
public struct Rewardcard//卡组列表里的卡牌数据和他的权重
{
    public CardDataSO CardData;
    public int Weight;
}