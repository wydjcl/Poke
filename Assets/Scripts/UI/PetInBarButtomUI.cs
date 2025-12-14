using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetInBarButtomUI : MonoBehaviour, IPointerClickHandler
{
    public int ID;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("µã»÷");
        if (ID == 1)
        {
            if (RewardManager.Instance.petInBarData1 != null)
            {
                RewardManager.Instance.AddCard(RewardManager.Instance.petInBarData1);
                RewardManager.Instance.PetBarLibraryInGame.rewardcardList.RemoveAll(obj => obj.CardData == RewardManager.Instance.petInBarData1);
                GameManager.Instance.AfterClickPetBarButtom();
            }
        }
        if (ID == 2)
        {
            if (RewardManager.Instance.petInBarData2 != null)
            {
                RewardManager.Instance.AddCard(RewardManager.Instance.petInBarData2);
                RewardManager.Instance.PetBarLibraryInGame.rewardcardList.RemoveAll(obj => obj.CardData == RewardManager.Instance.petInBarData2);
                GameManager.Instance.AfterClickPetBarButtom();
            }
        }
        if (ID == 3)
        {
            if (RewardManager.Instance.petInBarData3 != null)
            {
                RewardManager.Instance.AddCard(RewardManager.Instance.petInBarData3);
                RewardManager.Instance.PetBarLibraryInGame.rewardcardList.RemoveAll(obj => obj.CardData == RewardManager.Instance.petInBarData3);
                GameManager.Instance.AfterClickPetBarButtom();
            }
        }
    }
}