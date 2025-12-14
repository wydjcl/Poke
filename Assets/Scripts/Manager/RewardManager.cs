using System.Collections.Generic;

using TMPro;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public static RewardManager Instance;
    private PlayerType playerType;
    public RewardUI rewardUI;
    public CardReward cardReward1;//三个奖励
    public CardReward cardReward2;
    public CardReward cardReward3;
    public CardReward currentCardReward;
    public TextMeshProUGUI coinText;

    public PetInBarUI petInBarUI1;//三个宠物选项
    public PetInBarUI petInBarUI2;//三个宠物选项
    public PetInBarUI petInBarUI3;//三个宠物选项
    public CardDataSO petInBarData1;
    public CardDataSO petInBarData2;
    public CardDataSO petInBarData3;

    //TODO遗物控制抽几次s
    public CardDataSO cardDataSO;

    public CardLibrarySO currentCardLibrary;//获取当前卡组

    public CardInRewardLibrarySO OrdinaryCardInRewardLibrary;//普通品质卡牌奖池TODO分职业之后
    public CardInRewardLibrarySO ShopLibrary;//商店池子
    public CardInRewardLibrarySO PetBarLibrary;//宠物酒馆池子
    public CardInRewardLibrarySO PetBarLibraryInGame;//游戏里实际用的池子,用于防止宠物重复

    public List<Rewardcard> rewardcardList;// 读取的奖励列表
    public List<Rewardcard> shopcardList;//商店列表
    public List<Rewardcard> petBarList;//宠物酒馆列表

    public Dictionary<CardRarity, int> rewards = new Dictionary<CardRarity, int>()
    {
        //TODO 根据遗物改权重
        { CardRarity.Ordinary, 70 },
        { CardRarity.Rare, 20 },
        { CardRarity.Epic, 8 },
        { CardRarity.Legend, 2 }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        PetBarLibraryInGame.rewardcardList.Clear();
        foreach (var item in PetBarLibrary.rewardcardList)
        {
            PetBarLibraryInGame.rewardcardList.Add(item);
        }
    }

    public void GetRewardUI(RewardUI ui)
    {
        rewardUI = ui;
        ui.enabled = true;
    }

    public void GetCardReward(CardReward cR, int id)
    {
        if (id == 1)
        {
            cardReward1 = cR;
        }
        if (id == 2)
        {
            cardReward2 = cR;
        }

        if (id == 3)
        {
            cardReward3 = cR;
        }
    }

    private void Start()
    {
        // 抽三次
        playerType = GameManager.Instance.playerType;
        for (int i = 0; i < 3; i++)
        {
            //  string result = DrawReward();
            //Debug.Log($"第{i + 1}次抽奖结果: {result}");
        }
    }

    // 抽奖方法
    public CardRarity RollRarity()
    {
        int totalWeight = 0;
        foreach (var kv in rewards)
        {
            totalWeight += kv.Value;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int current = 0;

        foreach (var kv in rewards)
        {
            current += kv.Value;
            if (randomValue < current)
            {
                return kv.Key;
            }
        }

        return CardRarity.Ordinary; // 默认返回
    }

    public CardDataSO RollCard()
    {
        var totalWeight = 0;
        foreach (var item in rewardcardList)
        {
            totalWeight += item.Weight;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int current = 0;

        foreach (var item in rewardcardList)
        {
            current += item.Weight;
            if (randomValue < current)
            {
                rewardcardList.Remove(item);
                return item.CardData;
            }
        }
        rewardcardList.RemoveAt(0);
        return rewardcardList[0].CardData; // 默认返回
    }

    public CardDataSO RollShop()
    {
        var totalWeight = 0;
        foreach (var item in shopcardList)
        {
            totalWeight += item.Weight;
        }

        int randomValue = UnityEngine.Random.Range(0, totalWeight);
        int current = 0;

        foreach (var item in shopcardList)
        {
            current += item.Weight;
            if (randomValue < current)
            {
                shopcardList.Remove(item);
                return item.CardData;
            }
        }
        shopcardList.RemoveAt(0);
        return shopcardList[0].CardData; // 默认返回
    }

    public CardDataSO RollPet()
    {
        if (petBarList.Count == 0)
        {
            return null;
        }
        int randomValue = UnityEngine.Random.Range(0, petBarList.Count);
        var r = petBarList[randomValue].CardData;
        petBarList.RemoveAt(randomValue);
        return r; // 默认返回
    }

    public void ChangeCurrentCardReward(CardReward cardReward)//三选一确定选那个
    {
        currentCardReward = cardReward;
        RectTransform rect1 = cardReward1.gameObject.GetComponent<RectTransform>();
        RectTransform rect2 = cardReward2.gameObject.GetComponent<RectTransform>();
        RectTransform rect3 = cardReward3.gameObject.GetComponent<RectTransform>();

        if (cardReward.ID == 1)
        {
            /* cardReward1.transform.position = new Vector3(cardReward1.transform.position.x, -25f, 0);

             cardReward2.transform.position = new Vector3(cardReward2.transform.position.x, -95f, 0);
             cardReward3.transform.position = new Vector3(cardReward3.transform.position.x, -95f, 0);*/
            rect1.anchoredPosition = new Vector2(rect1.anchoredPosition.x, -25f);
            rect2.anchoredPosition = new Vector2(rect2.anchoredPosition.x, -95f);
            rect3.anchoredPosition = new Vector2(rect3.anchoredPosition.x, -95f);
        }
        if (cardReward.ID == 2)
        {
            rect1.anchoredPosition = new Vector2(rect1.anchoredPosition.x, -95f);
            rect2.anchoredPosition = new Vector2(rect2.anchoredPosition.x, -25f);
            rect3.anchoredPosition = new Vector2(rect3.anchoredPosition.x, -95f);
        }
        if (cardReward.ID == 3)
        {
            rect1.anchoredPosition = new Vector2(rect1.anchoredPosition.x, -95f);
            rect2.anchoredPosition = new Vector2(rect2.anchoredPosition.x, -95f);
            rect3.anchoredPosition = new Vector2(rect3.anchoredPosition.x, -25f);
        }
    }

    public void DebugLog()
    {
        Debug.Log(cardReward1.transform.position);
    }

    //TODO 从职业和稀有度抽卡

    public void StartRollReward()
    {
        rewardcardList.Clear();//奖池
        if (true)
        {
            //TODO不同角色类型有不同卡池
        }
        var R = RollRarity();//稀有度
        R = CardRarity.Ordinary;
        if (R == CardRarity.Ordinary)
        {
            foreach (var item in OrdinaryCardInRewardLibrary.rewardcardList)
            {
                rewardcardList.Add(item);//奖池读取
            }
        }

        cardReward1.cardData = RollCard();
        cardReward1.cardImage.sprite = cardReward1.cardData.cardImage;
        if (cardReward1.costText != null)
        {
            cardReward1.costText.text = cardReward1.cardData.coinCost.ToString();
        }
        cardReward2.cardData = RollCard();
        cardReward2.cardImage.sprite = cardReward2.cardData.cardImage;
        if (cardReward2.costText != null)
        {
            cardReward2.costText.text = cardReward2.cardData.coinCost.ToString();
        }
        cardReward3.cardData = RollCard();
        cardReward3.cardImage.sprite = cardReward3.cardData.cardImage;
        if (cardReward3.costText != null)
        {
            cardReward3.costText.text = cardReward3.cardData.coinCost.ToString();
        }
    }

    public void StartRollShop()
    {
        shopcardList.Clear();//奖池
        if (true)
        {
            //TODO不同角色类型有不同卡池
        }
        var R = RollRarity();//稀有度
        R = CardRarity.Ordinary;
        if (R == CardRarity.Ordinary)
        {
            foreach (var item in ShopLibrary.rewardcardList)
            {
                shopcardList.Add(item);//奖池读取
            }
        }

        cardReward1.cardData = RollShop();
        cardReward1.cardImage.sprite = cardReward1.cardData.cardImage;
        if (cardReward1.costText != null)
        {
            cardReward1.costText.text = cardReward1.cardData.coinCost.ToString();
        }
        cardReward2.cardData = RollShop();
        cardReward2.cardImage.sprite = cardReward2.cardData.cardImage;
        if (cardReward2.costText != null)
        {
            cardReward2.costText.text = cardReward2.cardData.coinCost.ToString();
        }
        cardReward3.cardData = RollShop();
        cardReward3.cardImage.sprite = cardReward3.cardData.cardImage;
        if (cardReward3.costText != null)
        {
            cardReward3.costText.text = cardReward3.cardData.coinCost.ToString();
        }
    }

    [ContextMenu("测试抽宠物")]
    public void StartRollPet()
    {
        petBarList.Clear();
        petInBarData1 = null; petInBarData2 = null; petInBarData3 = null;
        foreach (var item in PetBarLibraryInGame.rewardcardList)
        {
            petBarList.Add(item);//奖池读取
        }
        petInBarData1 = RollPet();
        if (petInBarData1 != null)
        {
            petInBarUI1.PetIcon.sprite = petInBarData1.petIcon;
            petInBarUI1.PetDes.text = petInBarData1.description;
        }

        petInBarData2 = RollPet();
        if (petInBarData2 != null)
        {
            petInBarUI2.PetIcon.sprite = petInBarData2.petIcon;
            petInBarUI2.PetDes.text = petInBarData2.description;
        }
        petInBarData3 = RollPet();
        if (petInBarData3 != null)
        {
            petInBarUI3.PetIcon.sprite = petInBarData3.petIcon;
            petInBarUI3.PetDes.text = petInBarData3.description;
        }
    }

    public CardDataSO CardRewardGetCardData()
    {
        return cardDataSO;
    }

    public void AddCard(CardDataSO targetCard)
    {
        for (int i = 0; i < currentCardLibrary.cardLibraryList.Count; i++)
        {
            if (currentCardLibrary.cardLibraryList[i].cardData == targetCard)
            {
                currentCardLibrary.cardLibraryList[i].amount++;
                return;
            }
        }

        // 没找到则新增
        CardLibraryEntry newEntry = new CardLibraryEntry
        {
            cardData = targetCard,
            amount = 1
        };
        currentCardLibrary.cardLibraryList.Add(newEntry);
    }

    /// <summary>
    /// 删除一张卡：如果数量大于1就减1，如果等于1就移除
    /// </summary>
    public void RemoveCard(CardDataSO targetCard)
    {
        for (int i = 0; i < currentCardLibrary.cardLibraryList.Count; i++)
        {
            if (currentCardLibrary.cardLibraryList[i].cardData == targetCard)
            {
                if (currentCardLibrary.cardLibraryList[i].amount > 1)
                {
                    currentCardLibrary.cardLibraryList[i].amount--;
                }
                else
                {
                    currentCardLibrary.cardLibraryList.RemoveAt(i);
                }
                return;
            }
        }
    }

    /// <summary>
    /// 生成min到max之间的数,约接近max权值越低
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public int GetWeightedRandomValue(int min, int max)
    {
        int totalWeight = 0;

        // 计算总权重：数值越小，权重越高（权重=max - 数值 + 1，确保50权重最高，150最低）
        for (int i = min; i <= max; i++)
        {
            totalWeight += (max - i + 1);
        }

        // 随机取权重区间
        int randomWeight = UnityEngine.Random.Range(0, totalWeight);
        int currentWeight = 0;

        // 根据权重命中数值
        for (int i = min; i <= max; i++)
        {
            currentWeight += (max - i + 1);
            if (randomWeight < currentWeight)
            {
                return i;
            }
        }
        return min; // 兜底返回最小值
    }
}