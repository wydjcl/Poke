using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagScrollUI : MonoBehaviour
{
    public CardLibrarySO cardLibrarySO;   // 在 Inspector 里拖拽背包 SO//卡组列表
    public CardLibrarySO newCardLibrarySO;   // 在 Inspector 里拖拽背包 SO//卡组列表
    public GameObject cardPrefab;     // 卡牌 UI 预制体（带 Image + Text）
    public Transform contentParent;   // Scroll View 的 Content
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI cardDescriptionText;
    private int count;//生成地第几张卡
    private int allCount;//总共几种牌
    private Vector2 cardPos;

    private void Start()
    {
    }

    private void OnEnable()
    {
        count = 0;
        allCount = 0;
        //TODO以后选角色改卡组,这里手动给初值
        /*     if (cardLibrarySO.cardLibraryList == null)
             {
                 foreach (var item in newCardLibrarySO.cardLibraryList)
                 {
                     cardLibrarySO.cardLibraryList.Add(item);
                 }
             }*/
        foreach (var card in cardLibrarySO.cardLibraryList)
        {
            if (card.amount == 0)
            {
                continue;
            }
            else
            {
            }

            allCount++;
        }

        RectTransform ct = contentParent.GetComponent<RectTransform>();

        if (allCount < 7)
        {
            ct.sizeDelta = new Vector2(0, 600f);
        }
        else
        {
            Debug.Log("大于7");
            ct.sizeDelta = new Vector2(0, 600f + ((allCount - 7) / 3 + 1) * 280f);//4 7 10张牌加高/每加280 卡牌需往上移动140
                                                                                  //ct.sizeDelta = new Vector2(0, 999); 6 0 7 1  81  91  10 2     00  11  21  31  42
        }

        PopulateInventory();
    }

    private void PopulateInventory()
    {
        foreach (var card in cardLibrarySO.cardLibraryList)
        {
            if (card.amount == 0)
            {
                continue;
            }
            else
            {
            }
            count++;
            GameObject cardObj = Instantiate(cardPrefab, contentParent);

            CardInBag cardInBag = cardObj.GetComponent<CardInBag>();
            cardInBag.nameText = card.cardData.cardName;
            cardInBag.desText = card.cardData.descriptionEasy;

            RectTransform rt = cardObj.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(CulPos(count).x, CulPos(count).y, 0); ;

            // 设置图片
            Image img = cardObj.transform.Find("CardImage").GetComponent<Image>();
            if (img == null)
            {
            }
            img.sprite = card.cardData.cardImage;

            // 设置费用
            TextMeshProUGUI costText = cardObj.transform.Find("CardCost").GetComponent<TextMeshProUGUI>();
            costText.text = card.cardData.cost.ToString();
        }
    }

    public Vector2 CulPos(int i)
    {
        int y = i % 3;//余数
        int c = i / 3;//结果

        if (y == 1)
        {
            if (allCount < 7)
            {
                return new Vector2(-187f, 150f - c * 280f);
            }
            else
            {
                return new Vector2(-187f, 150f - c * 280f + ((allCount - 7) / 3 + 1) * 140f);
            }
        }
        if (y == 2)
        {
            if (allCount < 7)
            {
                return new Vector2(5f, 150f - c * 280f);
            }
            else
            {
                return new Vector2(5f, 150f - c * 280f + ((allCount - 7) / 3 + 1) * 140f);
            }
        }
        if (y == 0)
        {
            if (allCount < 7)
            {
                return new Vector2(197f, 150f - (c - 1) * 280f);
            }
            else
            {
                return new Vector2(197f, 150f - (c - 1) * 280f + ((allCount - 7) / 3 + 1) * 140f);
            }
        }
        else
        {
            return new Vector2(191f, 750f - c * 280f);//忽略
        }
    }

    public void GetCardName(string s)
    {
        cardNameText.text = s;
    }

    public void GetCardDex(string s)
    {
        cardDescriptionText.text = s;
    }
}