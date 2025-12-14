using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardInBattle : MonoBehaviour
{
    public TextMeshPro costText, descriptionText, typeText, cardName, healthText, attackText;
    public CardDataSO cardData;
    public SpriteRenderer sp;

    // Start is called before the first frame update
    private void Start()
    {
        //Init(cardData);
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Init(CardDataSO data)
    {
        sp.sprite = data.cardImage;
    }
}