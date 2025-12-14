using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NextLevelButtomUI : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.smallLevel += 1;
        foreach (Transform child in GameManager.Instance.RoomCards.transform)
        {
            Destroy(child.gameObject); // É¾³ý×Ó¶ÔÏó
        }
        GameManager.Instance.RollRoom();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}