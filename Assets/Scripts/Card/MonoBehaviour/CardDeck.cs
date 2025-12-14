using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CardDeck : MonoBehaviour
{
    // public CardManager cardManager;
    public static CardDeck Instance;

    public List<CardDataSO> drawDeck = new();//

    public List<CardDataSO> discardDeck = new();//
    public List<CardDataSO> outGameDeck = new();//
    public List<Card> handCardObjectList = new();//

    public Vector3 DeckPos;//
    public CardLayoutManager layoutManager;

    public IntEventSO drawCountEvent;

    public IntEventSO discardCountEvent;
    //private CardManager cardManager = CardManager.Instance;

    private void Awake()
    {
        //cardManager = CardManager.Instance;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //InitDeck();//
        //DrawCard(4);
    }

    public void InitDeck()
    {
        drawDeck.Clear();
        discardDeck.Clear();
        outGameDeck.Clear();
        handCardObjectList.Clear();
        foreach (var entry in CardManager.Instance.currentLibrary.cardLibraryList)//从卡组力读卡
        {
            for (int i = 0; i < entry.amount; i++)//
            {
                drawDeck.Add(entry.cardData);//
            }
        }

        //TODO
        ShuffleDeck();
    }

    [ContextMenu("模拟抽卡")]
    public void textdraw()
    {
        DrawCard(1);

        foreach (var entry in drawDeck)
        {
            Debug.Log("entry" + entry);
        }
    }

    [ContextMenu("模拟洗牌抽牌")]
    public void testdis()
    {
        ShuffleDeck();
        foreach (var entry in drawDeck)
        {
            Debug.Log("entry" + entry);
        }
        DrawCard(5);
    }

    //
    public void NewTunrDrawCard()
    {
        DrawCard(4);
    }

    public void DrawCard(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (drawDeck.Count == 0)
            {
                //TODO
                foreach (var item in discardDeck)
                {
                    drawDeck.Add(item);//
                }
                ShuffleDeck();
            }
            CardDataSO currentCardData = drawDeck[0];

            drawDeck.RemoveAt(0);//

            //
            drawCountEvent.RaisEvent(drawDeck.Count, this);
            discardCountEvent.RaisEvent(discardDeck.Count, this);//卡堆数值通知

            var card = CardManager.Instance.GetCardObject().GetComponent<Card>();
            //
            card.Init(currentCardData);//
            card.transform.position = DeckPos;
            handCardObjectList.Add(card);
            var delay = i * 0.2f;
            SetCardLayout(delay);
        }
        //SetCardLayout(0.2f);
    }

    private void SetCardLayout(float delay)
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            Card currentCard = handCardObjectList[i];
            CardTransForm cardTransForm = layoutManager.GetCardTransForm(i, handCardObjectList.Count);
            // currentCard.transform.SetPositionAndRotation(cardTransForm.pos, cardTransForm.rotation);

            //var cardCost = currentCard.cardData.cost;
            //currentCard.isAvailiable = cardCost <= player.CurrentMana;//
            // currentCard.UpdateCardState();
            //更新卡牌阶段,颜色

            currentCard.isAni = true;
            currentCard.transform.DOScale(Vector3.one, 0.1f).SetDelay(delay).onComplete = () =>
            {
                currentCard.transform.DOMove(cardTransForm.pos, 0.2f).onComplete = () =>
                {
                    currentCard.isAni = false;
                };//
                  // currentCard.transform.DORotateQuaternion(cardTransForm.rotation, 0.5f);
            };

            //
            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            //currentCard.transform.position = new Vector3(currentCard.transform.position.x, currentCard.transform.position.y, -i * 0.01f);
            currentCard.UpdatePosRot(cardTransForm.pos, cardTransForm.rotation);
        }
    }

    //
    private void ShuffleDeck()
    {
        discardDeck.Clear();//
        drawCountEvent.RaisEvent(drawDeck.Count, this);//广播数量变化
        discardCountEvent.RaisEvent(discardDeck.Count, this);//广播数量变化
        for (int i = 0; i < drawDeck.Count; i++)
        {
            //打乱顺序
            CardDataSO temp = drawDeck[i];//
            int randomIndex = Random.Range(i, drawDeck.Count);
            drawDeck[i] = drawDeck[randomIndex];//
            drawDeck[randomIndex] = temp;
        }
    }

    //
    public void DiscardCard(object obj)
    {
        Card card = obj as Card;
        discardDeck.Add(card.cardData);//
        handCardObjectList.Remove(card);//
        CardManager.Instance.DiscardCard(card.gameObject);
        drawCountEvent.RaisEvent(drawDeck.Count, this);
        discardCountEvent.RaisEvent(discardDeck.Count, this);
        SetCardLayout(0f);//
    }

    public void OutGameCard(object obj)
    {
        Card card = obj as Card;
        outGameDeck.Add(card.cardData);//
        handCardObjectList.Remove(card);//
        CardManager.Instance.DiscardCard(card.gameObject);//manager里放回pool
        drawCountEvent.RaisEvent(drawDeck.Count, this);
        //discardCountEvent.RaisEvent(discardDeck.Count, this);
        SetCardLayout(0f);//
    }

    //弃置所有手牌
    public void DiscardAllCard()
    {
        for (int i = 0; i < handCardObjectList.Count; i++)
        {
            discardDeck.Add(handCardObjectList[i].cardData);
            CardManager.Instance.DiscardCard(handCardObjectList[i].gameObject);
        }
        handCardObjectList.Clear();
        discardCountEvent.RaisEvent(discardDeck.Count, this);
    }

    public void OnDestroy()
    {
        transform.DOKill();
    }
}