using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerType playerType;
    public PlayerDataSO playerDataSO;
    public PetLibrarySO petLibrarySO;
    public int coin;
    public GameObject MainSceneFather;
    public GameObject RoomCards;
    public GameObject pet1;
    public GameObject pet2;
    public GameObject pet3;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public int smallLevel;//第几小关
    public int bigLevel;//第几大关
    public List<RoomCard> currentRoomCardLists;
    public List<EnemyInOneLevel> CFirstLevelNormalEnemyList = new List<EnemyInOneLevel>();//第一关的普通敌人池子的

    [Header("需要导入")]
    public RoomLibrarySO roomLibraryData;

    public GameObject RoomCardPrefab;

    public EnemyInRoomLibrarySO FirstLevelNormalEnemyLibrary;//第一关的普通敌人池子,现在池子的so

    [Header("测试用Boss预制体")]
    public GameObject KuluBoss;

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
        smallLevel = 0;
        Init();
    }

    public void Init()
    {
        //测试用,开局手动添加角色类型
        playerType = playerDataSO.playerType;

        CFirstLevelNormalEnemyList = FirstLevelNormalEnemyLibrary.EnemyInOneLevelList.ToList();//直接赋值等于赋予地址,是不可以的,用tolist是真正赋值
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void GetMainSceneFather(GameObject g)
    {
        MainSceneFather = g;
    }

    public void LoadRewardScene()
    {
        //SceneManager.LoadScene(4, LoadSceneMode.Additive);
        StartCoroutine(IStartReward());
        SceneManager.UnloadSceneAsync(2);
        //Destroy(UIManager.Instance.PetBattleSpace.GetComponentInChildren<GameObject>());
        foreach (Transform child in UIManager.Instance.PetBattleSpace.transform)
        {
            Destroy(child.gameObject); // 删除子对象
        }

        UIManager.Instance.PetBox1.isFull = false;
        UIManager.Instance.PetBox2.isFull = false;
        UIManager.Instance.PetBox3.isFull = false;
    }

    [ContextMenu("测试打开宝藏房间")]
    public void LoadTreasureScene()
    {
        //SceneManager.LoadScene(4, LoadSceneMode.Additive);
        StartCoroutine(IStartTreasure());

        //Destroy(UIManager.Instance.PetBattleSpace.GetComponentInChildren<GameObject>());
        foreach (Transform child in UIManager.Instance.PetBattleSpace.transform)
        {
            Destroy(child.gameObject); // 删除子对象
        }

        UIManager.Instance.PetBox1.isFull = false;
        UIManager.Instance.PetBox2.isFull = false;
        UIManager.Instance.PetBox3.isFull = false;
    }

    public void LoadShopScene()
    {
        //SceneManager.LoadScene(4, LoadSceneMode.Additive);
        StartCoroutine(IStartShop());

        //Destroy(UIManager.Instance.PetBattleSpace.GetComponentInChildren<GameObject>());
        foreach (Transform child in UIManager.Instance.PetBattleSpace.transform)
        {
            Destroy(child.gameObject); // 删除子对象
        }

        UIManager.Instance.PetBox1.isFull = false;
        UIManager.Instance.PetBox2.isFull = false;
        UIManager.Instance.PetBox3.isFull = false;
    }

    public void LoadBarScene()
    {
        //SceneManager.LoadScene(4, LoadSceneMode.Additive);
        StartCoroutine(IStartPetBar());

        //Destroy(UIManager.Instance.PetBattleSpace.GetComponentInChildren<GameObject>());
        foreach (Transform child in UIManager.Instance.PetBattleSpace.transform)
        {
            Destroy(child.gameObject); // 删除子对象
        }

        UIManager.Instance.PetBox1.isFull = false;
        UIManager.Instance.PetBox2.isFull = false;
        UIManager.Instance.PetBox3.isFull = false;
    }

    public void StartBattle()
    {
        // 启动协程加载场景
        StartCoroutine(IStartBattle());
    }

    private IEnumerator IStartBattle()
    {
        // 异步加载场景
        // GetEnemy();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

        // 等待加载完成
        while (!asyncLoad.isDone)
        {
            Debug.Log("加载进度: " + asyncLoad.progress);
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            //  SceneManager.UnloadSceneAsync(1);
            //MainScene不关闭
            MainSceneFather.SetActive(false);
            //yield return new WaitForSeconds(3f);
            AfterStartBattle();
        }
    }

    private IEnumerator IStartReward()
    {
        // 异步加载场景
        // GetEnemy();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(4, LoadSceneMode.Additive);

        // 等待加载完成
        while (!asyncLoad.isDone)
        {
            Debug.Log("加载进度: " + asyncLoad.progress);
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            //  SceneManager.UnloadSceneAsync(1);
            //MainScene不关闭
            MainSceneFather.SetActive(false);
            //yield return new WaitForSeconds(3f);

            RewardManager.Instance.StartRollReward();
            int gainCoin = RewardManager.Instance.GetWeightedRandomValue(50, 150);
            UIManager.Instance.coinText.text = gainCoin.ToString();
            BattleManager.Instance.playerCoin += gainCoin;
            UIManager.Instance.ChangeUIInBattle();
        }
    }

    private IEnumerator IStartTreasure()
    {
        // 异步加载场景
        // GetEnemy();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(5, LoadSceneMode.Additive);

        // 等待加载完成
        while (!asyncLoad.isDone)
        {
            Debug.Log("加载进度: " + asyncLoad.progress);
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            //  SceneManager.UnloadSceneAsync(1);
            //MainScene不关闭
            MainSceneFather.SetActive(false);
            //yield return new WaitForSeconds(3f);

            TreasureManager.Instance.StartRollReward();
        }
    }

    private IEnumerator IStartShop()
    {
        // 异步加载场景
        // GetEnemy();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);

        // 等待加载完成
        while (!asyncLoad.isDone)
        {
            Debug.Log("加载进度: " + asyncLoad.progress);
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            //  SceneManager.UnloadSceneAsync(1);
            //MainScene不关闭
            MainSceneFather.SetActive(false);
            //yield return new WaitForSeconds(3f);

            RewardManager.Instance.StartRollReward();
        }
    }

    private IEnumerator IStartPetBar()
    {
        // 异步加载场景
        // GetEnemy();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(6, LoadSceneMode.Additive);

        // 等待加载完成
        while (!asyncLoad.isDone)
        {
            Debug.Log("加载进度: " + asyncLoad.progress);
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            //  SceneManager.UnloadSceneAsync(1);
            //MainScene不关闭
            MainSceneFather.SetActive(false);
            //yield return new WaitForSeconds(3f);

            RewardManager.Instance.StartRollPet();
        }
    }

    public void AfterStartBattle()
    {
        BattleManager.Instance.units.Clear();
        BattleManager.Instance.units = new List<CharacterBase>(new CharacterBase[6]);//

        BattleManager.Instance.characterList.Clear();

        if (enemy1 != null)
        {
            var e1 = Instantiate(enemy1, new Vector3(-3f, 1f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
            // SceneManager.MoveGameObjectToScene(e1, SceneManager.GetSceneByBuildIndex(2));
            CharacterBase enemy1C = e1.GetComponent<CharacterBase>();

            //enemy1C.ChangeEnemyData();

            enemy1C.NO = 3;
            BattleManager.Instance.units[3] = enemy1C;
            //enemy1C.ReadData();
        }
        else
        {
            BattleManager.Instance.units[3] = null;
        }
        if (enemy2 != null)
        {
            var e2 = Instantiate(enemy2, new Vector3(0f, 1f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
            //  SceneManager.MoveGameObjectToScene(e2, SceneManager.GetSceneByBuildIndex(2));
            CharacterBase enemy2C = e2.GetComponent<CharacterBase>();

            //enemy1C.ChangeEnemyData();

            enemy2C.NO = 4;
            BattleManager.Instance.units[4] = enemy2C;
            //enemy1C.ReadData();
        }
        else
        {
            BattleManager.Instance.units[4] = null;
        }
        if (enemy3 != null)
        {
            var e3 = Instantiate(enemy3, new Vector3(3f, 1f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
            //  SceneManager.MoveGameObjectToScene(e3, SceneManager.GetSceneByBuildIndex(2));
            CharacterBase enemy3C = e3.GetComponent<CharacterBase>();

            //enemy1C.ChangeEnemyData();

            enemy3C.NO = 5;
            BattleManager.Instance.units[5] = enemy3C;
            //enemy1C.ReadData();
        }
        else
        {
            BattleManager.Instance.units[5] = null;
        }
        if (pet1 != null)
        {
            var p1 = Instantiate(pet1, new Vector3(-3f, -2.8f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
            //  SceneManager.MoveGameObjectToScene(p1, SceneManager.GetSceneByBuildIndex(2));
            CharacterBase pet1C = p1.GetComponent<CharacterBase>();
            pet1C.NO = 0;
            BattleManager.Instance.units[0] = pet1C;
        }
        else
        {
            BattleManager.Instance.units[0] = null;
        }
        if (pet2 != null)
        {
            var p2 = Instantiate(pet2, new Vector3(0f, -2.8f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
            //  SceneManager.MoveGameObjectToScene(p2, SceneManager.GetSceneByBuildIndex(2));
            CharacterBase pet2C = p2.GetComponent<CharacterBase>();
            pet2C.NO = 1;
            BattleManager.Instance.units[1] = pet2C;
        }
        else
        {
            BattleManager.Instance.units[1] = null;
        }
        if (pet3 != null)
        {
            var p3 = Instantiate(pet3, new Vector3(3f, -2.8f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
            //   SceneManager.MoveGameObjectToScene(p3, SceneManager.GetSceneByBuildIndex(2));
            CharacterBase pet3C = p3.GetComponent<CharacterBase>();
            pet3C.NO = 2;
            BattleManager.Instance.units[2] = pet3C;
        }
        else
        {
            BattleManager.Instance.units[2] = null;
        }

        UIManager.Instance.Battle.SetActive(true);
        BattleManager.Instance.BattleStart();//TODO开始战斗
    }

    public void AfterClickRewardButtom()//点击跳过奖励之后
    {
        SceneManager.UnloadSceneAsync(4);
        MainSceneFather.SetActive(true);
        UIManager.Instance.ChangeNextLevelButtom();
    }

    public void AfterClickShopButtom()//点击跳过奖励之后
    {
        SceneManager.UnloadSceneAsync(3);
        MainSceneFather.SetActive(true);
        UIManager.Instance.ChangeNextLevelButtom();
    }

    public void AfterClickTreasureButtom()
    {
        SceneManager.UnloadSceneAsync(5);
        MainSceneFather.SetActive(true);
        UIManager.Instance.ChangeNextLevelButtom();
    }

    public void AfterClickPetBarButtom()
    {
        SceneManager.UnloadSceneAsync(6);
        MainSceneFather.SetActive(true);
        UIManager.Instance.ChangeNextLevelButtom();
    }

    public void GetEnemy(GameObject e1, GameObject e2, GameObject e3)
    {
        if (e1 != null)
        {
            enemy1 = e1;
        }
        if (e2 != null)
        {
            enemy2 = e2;
        }
        if (e3 != null)
        {
            enemy3 = e3;
        }
    }

    public void GetEnemyByLevel(int i, bool e)//第几大关,是否是精英敌人
    {
        var count = CFirstLevelNormalEnemyList.Count;//一大关里面普通敌人组合总数

        if (!e)
        {
            if (i == 0)//第一大关
            {
                int chosen = Random.Range(0, count); ;
                enemy1 = null;
                enemy2 = null;
                enemy3 = null;
                if (CFirstLevelNormalEnemyList[chosen].enemy1 != null)
                {
                    enemy1 = CFirstLevelNormalEnemyList[chosen].enemy1;
                }
                if (CFirstLevelNormalEnemyList[chosen].enemy2 != null)
                {
                    enemy2 = CFirstLevelNormalEnemyList[chosen].enemy2;
                }
                if (CFirstLevelNormalEnemyList[chosen].enemy3 != null)
                {
                    enemy3 = CFirstLevelNormalEnemyList[chosen].enemy3;
                }
                CFirstLevelNormalEnemyList.RemoveAt(chosen);
            }
        }
    }

    public void GetBoss(int i)
    {
        enemy1 = null;
        enemy2 = null;
        enemy3 = null;
        enemy2 = KuluBoss;
    }

    [ContextMenu("DebugLog")]
    public void DebugLog()
    {
        Debug.Log("C:" + CFirstLevelNormalEnemyList + "间隔" + CFirstLevelNormalEnemyList.Count);
        Debug.Log("F:" + FirstLevelNormalEnemyLibrary.EnemyInOneLevelList + "间隔" + FirstLevelNormalEnemyLibrary.EnemyInOneLevelList.Count);
    }

    public void OpenPetDesUI()
    {
    }

    [ContextMenu("测试抽关卡")]
    public void RollRoom()
    {
        var max = roomLibraryData.smallLevelList.Count;//一大关里小关的数量

        var sMax = roomLibraryData.smallLevelList[smallLevel].roomCardList.Count;//房间卡的数量
        currentRoomCardLists.Clear();
        int sCount = 0;//正在生成的数量

        foreach (var item in roomLibraryData.smallLevelList[smallLevel].roomCardList)
        {
            if (item.weight == 0)
            {
                CreatRoomCard(item.roomType, sMax);
                sCount++;
            }
            else
            {
                if (item.weight > Random.value)
                {
                    CreatRoomCard(item.roomType, sMax);
                    sCount++;
                }
                else
                {
                }
            }
        }

        CulRoomCardPos(sCount);
        UIManager.Instance.ChangeNextLevelButtom();
        // }
    }

    public void CreatRoomCard(RoomType roomType, int num)//传入的房间类型,小关卡片总数
    {
        var r = Instantiate(RoomCardPrefab, RoomCards.transform);
        var roomCard = r.GetComponent<RoomCard>();
        roomCard.roomType = roomType;
        currentRoomCardLists.Add(roomCard);
    }

    public void CulRoomCardPos(int sCount)
    {
        if (sCount == 1)
        {
            currentRoomCardLists[0].transform.position = Vector3.zero;
        }
        if (sCount == 2)
        {
            currentRoomCardLists[0].transform.position = new Vector3(-2.5f, 0, 0);
            currentRoomCardLists[1].transform.position = new Vector3(2.5f, 0, 0);
        }
        if (sCount == 3)
        {
            currentRoomCardLists[0].transform.position = new Vector3(0f, 2.5f, 0);

            currentRoomCardLists[1].transform.position = new Vector3(-2.5f, -2.5f, 0);
            currentRoomCardLists[2].transform.position = new Vector3(2.5f, -2.5f, 0);
        }
        if (sCount == 4)
        {
            currentRoomCardLists[0].transform.position = new Vector3(-2.5f, 2.5f, 0);
            currentRoomCardLists[1].transform.position = new Vector3(2.5f, 2.5f, 0);

            currentRoomCardLists[2].transform.position = new Vector3(-2.5f, -2.5f, 0);
            currentRoomCardLists[3].transform.position = new Vector3(2.5f, -2.5f, 0);
        }
        if (sCount == 5)
        {
            currentRoomCardLists[0].transform.position = new Vector3(-8f, 0, 0);
            currentRoomCardLists[1].transform.position = new Vector3(-4f, 0, 0);
            currentRoomCardLists[2].transform.position = new Vector3(0, 0, 0);

            currentRoomCardLists[3].transform.position = new Vector3(4f, 0, 0);
            currentRoomCardLists[4].transform.position = new Vector3(8f, 0, 0);
        }
    }

    /// <summary>
    /// 检查房间是否还有战斗房间,没有的话返回false,通知UIManager显示下一关按钮
    /// </summary>
    public bool CheckRoom()
    {
        foreach (var room in currentRoomCardLists)
        {
            if (room.roomType == RoomType.NormalEnemy || room.roomType == RoomType.BigEnemy || room.roomType == RoomType.Boss)
            {
                if (room.isLock == true)
                {
                }
                else
                {
                    return true;
                }
            }
        }
        return false;//开
    }

    public void GetCoin(int i)
    {
        BattleManager.Instance.playerCoin += i;
    }
}