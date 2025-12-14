using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TreasureReward : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public TreasureDataSO data;
    public Image image;
    public int ID;

    private void Start()
    {
        if (ID == 1)
        {
            TreasureManager.Instance.treasureReward1 = this;
        }
        if (ID == 2)
        {
            TreasureManager.Instance.treasureReward2 = this;
        }
        if (ID == 3)
        {
            TreasureManager.Instance.treasureReward3 = this;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // DetailsBox.gameObject.SetActive(true);
        TreasureManager.Instance.ChangeCurrentTreasure(this);

        //TODO´«Öµ
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.cardDesText.text = data.treasureDescription;
    }
}