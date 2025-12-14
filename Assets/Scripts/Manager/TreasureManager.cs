using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureManager : MonoBehaviour
{
    public static TreasureManager Instance;

    public TreasureLibrarySO treasureLibrary;//遗物池子数据,TODO不同职业不同池子

    public TreasureLibrarySO playerTreasureLibrary;//玩家遗物数据
    public TreasureReward currentTreasureReward;

    public List<TreasureDataSO> treasureList;// 读取的奖励列表
    public TreasureReward treasureReward1;//三个奖励
    public TreasureReward treasureReward2;
    public TreasureReward treasureReward3;

    public void Awake()
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
        playerTreasureLibrary.treasureDataList.Clear();
    }

    public void AddTreasure(TreasureDataSO data)
    {
        playerTreasureLibrary.treasureDataList.Add(data);
    }

    public void StartRollReward()
    {
        treasureList.Clear();//奖池
        if (true)
        {
            //TODO不同角色类型有不同卡池
        }

        foreach (var item in treasureLibrary.treasureDataList)
        {
            treasureList.Add(item);//奖池读取
        }

        treasureReward1.data = RollTreasure();
        treasureReward1.image.sprite = treasureReward1.data.sprite;
        treasureReward2.data = RollTreasure();
        treasureReward2.image.sprite = treasureReward2.data.sprite;
        treasureReward3.data = RollTreasure();
        treasureReward3.image.sprite = treasureReward3.data.sprite;
    }

    public TreasureDataSO RollTreasure()
    {
        int randomValue = UnityEngine.Random.Range(0, treasureList.Count);
        var r = treasureList[randomValue];
        treasureList.RemoveAt(randomValue);
        return r; // 默认返回
    }

    public void ChangeCurrentTreasure(TreasureReward treasureReward)//三选一确定选那个
    {
        currentTreasureReward = treasureReward;
        RectTransform rect1 = treasureReward1.gameObject.GetComponent<RectTransform>();
        RectTransform rect2 = treasureReward2.gameObject.GetComponent<RectTransform>();
        RectTransform rect3 = treasureReward3.gameObject.GetComponent<RectTransform>();

        if (treasureReward.ID == 1)
        {
            /* cardReward1.transform.position = new Vector3(cardReward1.transform.position.x, -25f, 0);

             cardReward2.transform.position = new Vector3(cardReward2.transform.position.x, -95f, 0);
             cardReward3.transform.position = new Vector3(cardReward3.transform.position.x, -95f, 0);*/
            rect1.anchoredPosition = new Vector2(rect1.anchoredPosition.x, -25f);
            rect2.anchoredPosition = new Vector2(rect2.anchoredPosition.x, -95f);
            rect3.anchoredPosition = new Vector2(rect3.anchoredPosition.x, -95f);
        }
        if (treasureReward.ID == 2)
        {
            rect1.anchoredPosition = new Vector2(rect1.anchoredPosition.x, -95f);
            rect2.anchoredPosition = new Vector2(rect2.anchoredPosition.x, -25f);
            rect3.anchoredPosition = new Vector2(rect3.anchoredPosition.x, -95f);
        }
        if (treasureReward.ID == 3)
        {
            rect1.anchoredPosition = new Vector2(rect1.anchoredPosition.x, -95f);
            rect2.anchoredPosition = new Vector2(rect2.anchoredPosition.x, -95f);
            rect3.anchoredPosition = new Vector2(rect3.anchoredPosition.x, -25f);
        }
    }
}