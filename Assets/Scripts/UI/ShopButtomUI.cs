using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButtomUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject shopCard;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (RewardManager.Instance.currentCardReward == null)
        {
            return;
        }
        if (BattleManager.Instance.playerCoin >= RewardManager.Instance.currentCardReward.cardData.coinCost)
        {
            BattleManager.Instance.playerCoin -= RewardManager.Instance.currentCardReward.cardData.coinCost;
            RewardManager.Instance.AddCard(RewardManager.Instance.currentCardReward.cardData);
            shopCard.SetActive(false);
            gameObject.SetActive(false);
            // GameManager.Instance.AfterClickShopButtom();
        }
        else
        {
            //¹ºÂòÊ§°Ü
        }
    }
}