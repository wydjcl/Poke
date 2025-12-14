using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance;
    public PoolTool poolTool;
    public List<CardDataSO> cardDataList;//项目里所有卡牌数据的列表.因为我们需要manager管理我们所有卡牌,所以需要所有卡牌数据,为了代码可扩展性,使用address管理数据,直接从address里面读取

    [Header("卡组")]
    public CardLibrarySO newGameCardLibrary;

    public CardLibrarySO currentLibrary;

    private void Awake()
    {
        foreach (var item in newGameCardLibrary.cardLibraryList)
        {
            currentLibrary.cardLibraryList.Add(item);
        }
        Init();
        //初始化时将默认卡组添加到当前卡组
        //TODO以后选角色改卡组,这里手动给初值
    }

    private void OnApplicationQuit()
    {
        currentLibrary.cardLibraryList.Clear();
    }

    private void Update()
    {
    }

    public void Init()
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
    }

    //获取 回收
    public GameObject GetCardObject()
    {
        var cardObj = poolTool.GetObjectFromPool();
        //cardObj.transform.localScale = Vector3.zero;// 初始scale为0,后面变1
        return cardObj;
    }

    public void DiscardCard(GameObject cardObj)
    {
        poolTool.ReturnObjectToPool(cardObj);
    }
}