using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardReward : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public GameObject Entry;
    public CardDataSO cardData;
    public Image cardImage;
    public TextMeshProUGUI costText;
    public int ID;

    public void OnPointerClick(PointerEventData eventData)
    {
        // DetailsBox.gameObject.SetActive(true);
        RewardManager.Instance.ChangeCurrentCardReward(this);

        //TODO´«Öµ
    }

    public void DisDetailBox()
    {
        //   DetailsBox.gameObject.SetActive(false);
    }

    public void ChangeUI()
    {
        cardImage.sprite = cardData.cardImage;
    }

    private void Start()
    {
        RewardManager.Instance.GetCardReward(this, ID);
        // cardData = RewardManager.Instance.CardRewardGetCardData();
    }

    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.cardDesText.text = cardData.description;
    }
}