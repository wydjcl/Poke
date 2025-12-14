using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RewardCardButtom : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (RewardManager.Instance.currentCardReward == null)
        {
            return;
        }
        else
        {
            RewardManager.Instance.AddCard(RewardManager.Instance.currentCardReward.cardData);
            GameManager.Instance.AfterClickRewardButtom();
        }
    }
}