using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataSO", menuName = "Card/CardData")]
public class CardDataSO : ScriptableObject
{
    public int ID;
    public string cardName;
    public Sprite cardImage;
    public int cost;
    public CardType cardType;
    public CardRarity cardRarity;
    public GameObject PetPrefab;
    public int coinCost;
    public Sprite petIcon;

    [TextArea]
    public string descriptionEasy;

    [TextArea]
    public string description;

    //TODO 执行的效果
}