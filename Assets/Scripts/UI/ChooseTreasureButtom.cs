using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseTreasureButtom : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (TreasureManager.Instance.currentTreasureReward == null)
        {
            return;
        }
        else
        {
            TreasureManager.Instance.AddTreasure(TreasureManager.Instance.currentTreasureReward.data);
            GameManager.Instance.AfterClickRewardButtom();
        }
    }
}