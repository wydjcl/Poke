using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("各项属性")]
    public GameObject entry;

    public TextMeshPro costText, descriptionText, typeText, cardName, healthText, attackText;
    public CardDataSO cardData;
    public SpriteRenderer sp;
    public int ID;
    public Vector3 originalPosition;
    public Quaternion originalRotation;
    public int originaLayerOrder;//原始叠层排序
    public GameObject petUI;

    public bool isAni = true;
    public bool isAvailiable;

    // public Player player;
    [Header("宠物预制体")]
    public GameObject Princess;

    [Header("广播")]
    public ObjectEventSO discardCardEvent;

    public ObjectEventSO outCardEvent;

    public IntEventSO summonPet;

    // Start is called before the first frame update
    private void Start()
    {
        //  Init(cardData);
    }

    public void Init(CardDataSO data)
    {
        cardData = data;
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
        costText.text = data.cost.ToString();
        descriptionText.text = data.description.ToString();
        cardName.text = data.cardName.ToString();
        ID = data.ID;
        //typeText.text = data.cardType.ToString();
        if (data.cardType == CardType.Attack)
        {
            typeText.text = "攻击牌";
        }
        if (data.cardType == CardType.Ability)
        {
            typeText.text = "能力牌";
        }
        if (data.cardType == CardType.Affect)
        {
            typeText.text = "效果牌";
        }
        if (data.cardType == CardType.Pet)
        {
            typeText.text = "宠物牌";
        }
        if (data.cardType != CardType.Pet)
        {
            petUI.SetActive(false);
        }
        if (data.cardType == CardType.PAffect)
        {
            typeText.text = "效果牌";
        }

        sp.sprite = data.cardImage;
        isAni = false;
    }

    public void UpdatePosRot(Vector3 pos, Quaternion rot)
    {
        originalPosition = pos;
        originalRotation = rot;
        originaLayerOrder = GetComponent<SortingGroup>().sortingOrder;
    }

    [ContextMenu("debug")]
    public void DebugLog()
    {
        Debug.Log(originalPosition.x + "//" + originalPosition.y);
    }

    public void OnPointerEnter(PointerEventData eventData)//鼠标滑入,
    {
        if (isAni)
        {
            return;
        }
        UIManager.Instance.ChangeCardDesText(descriptionText.text);
        entry.transform.position = originalPosition + Vector3.up * 0.7f;
        entry.transform.rotation = Quaternion.identity;

        //transform.position = originalPosition + Vector3.up;
        //transform.rotation = originalRotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, -5f);
        GetComponent<SortingGroup>().sortingOrder = 30;
        // transform.localScale = new Vector3(2, 2, 2);
    }

    public void OnPointerExit(PointerEventData eventData)//滑出
    {
        if (isAni)
        {
            return;
        }
        ReSetCard();
    }

    public void ReSetCard()
    {
        /*  entry.transform.position = originalPosition;
          entry.transform.rotation = originalRotation;*/
        entry.transform.localPosition = new Vector3(0, 0, 0);//用local表示子物体的position,而不是世界坐标
        entry.transform.rotation = originalRotation;
        /* Entry.transform.position = new Vector3(0, 0, 0);
         Entry.transform.rotation = Quaternion.identity;*/
        transform.rotation = originalRotation;
        transform.position = originalPosition;
        GetComponent<SortingGroup>().sortingOrder = originaLayerOrder;
        isAni = false;
        //transform.localScale = new Vector3(1, 1, 1);
    }

    public void UpdateCardState()
    {
        //根据费用改区别
        /*    isAvailiable = cardData.cost <= player.CurrentMana;
            costText.color = isAvailiable ? Color.green : Color.red;*/
    }

    public void ExecuteCardEffects(CharacterBase target)
    {
        // costManaEvent.RaisEvent(cardData.cost, this);

        /* foreach (var effect in cardData.effects)
         {
             effect.Execute(from, target);
         }*/
        BattleManager.Instance.playerMana -= cardData.cost;
        UIManager.Instance.ChangeUIInBattle();
        Debug.Log("ID" + ID);
        if (ID == -1)//擦拭粘液
        {
            outCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }

        if (ID == 1)//普通攻击
        {
            target.TakeDamage(BattleManager.Instance.playerAttack);
            discardCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }

        if (ID == 2)//基础魔法屏障
        {
            target.TakeDefense(5);
            discardCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }
        if (ID == 3)//法力恢复
        {
            BattleManager.Instance.playerMana += 1;
            UIManager.Instance.ChangeUIInBattle();
            discardCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }
        /*    if (ID == 2)//战争怒吼
            {
                for (var i = 0; i < BattleManager.Instance.characterList.Count; i++)
                {
                    if (BattleManager.Instance.characterList[i] < 3)
                    {
                        BattleManager.Instance.units[BattleManager.Instance.characterList[i]].attack += 3;

                        BattleManager.Instance.units[BattleManager.Instance.characterList[i]].ChangeUI();
                    }
                }
                Debug.Log("释放战争怒吼");

                //outCardEvent.RaisEvent(this, this);//除外这张卡
                discardCardEvent.RaisEvent(this, this);//弃这张卡
            }*/
        if (ID == 4)//努力学习
        {
            BattleManager.Instance.playerAttack += 6;
            UIManager.Instance.ChangeUIInBattle();
            outCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }
        /*  if (ID == 4)//宠物肌肉训练
          {
              target.attack += 5;
              target.ChangeUI();
              discardCardEvent.RaisEvent(this, this);//弃这张卡
          }*/
        if (ID == 5)//发现宝箱
        {
            discardCardEvent.RaisEvent(this, this);//弃这张卡
            CardDeck.Instance.DrawCard(2);
            return;
        }
        if (ID == 6)//强力一拳
        {
            target.TakeDamage(BattleManager.Instance.playerAttack * 3);
            outCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }
        if (ID == 7)//抢跑
        {
            target.attack += 3;
            if (target.petMana < target.petMaxMana)
            {
                target.SmallSkill();
                target.petMana += 1;
            }
            else
            {
                target.BigSkill();
                target.petMana = 0;
            }
            target.ChangeUI();
            discardCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }
        if (ID == 8)//力量解放
        {
            target.combo = 1;
            target.comboMax = 1;
            outCardEvent.RaisEvent(this, this);
            return;
        }
        if (ID == 9)//法力汲取
        {
            if (target.petMana > 0)
            {
                target.petMana -= 1;
                BattleManager.Instance.playerMana += 1;
                target.ChangeUI();
                UIManager.Instance.ChangeUIInBattle();
            }
            outCardEvent.RaisEvent(this, this);
            return;
        }

        if (ID >= 1001)
        {
            Debug.Log("进入召唤方法");
            target.SummonPet(target.NO, cardData.PetPrefab);
            outCardEvent.RaisEvent(this, this);//弃这张卡
            return;
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}