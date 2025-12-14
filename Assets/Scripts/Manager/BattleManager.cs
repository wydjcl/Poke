using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("UI")]
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI manaText;
    public TextMeshProUGUI contextText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI cardDeckText;
    public Image healthBar;

    [Header("各项数据")]
    public int playerHP;

    public int playerMaxHp;
    public int playerMana;
    public int playerMaxMana;
    public bool isPlayerDead = false;
    public int playerSettingManaMax;
    public int playerSettingAtk;
    public int playerAttack;
    public int playerCoin;

    public bool isBattle;//是否在战斗
    public bool isPlayerTurn = false;//是否是玩家回合
    public bool isEnemyTurn = false;
    public int turnCount = 0;//回合数
    private float timeCounter;//计时器
    public float enemyTurnDuration;//敌人回合等待时间
    public float playerTurnDuration;//玩家回合等待时间

    [Header("战斗")]
    public List<bool> isCharacter;//战斗逻辑格子

    public List<int> characterList;//战斗格子
    public int battleCount;
    public List<CharacterBase> units;//存储战斗场景的对象,根据对象身上的NO排序

    [Header("广播")]
    public ObjectEventSO playerTurnBegin;

    public ObjectEventSO playerTurnDown;

    public IntEventSO characterAct;//广播,传递int ,int为第几个格子行动

    public ObjectEventSO enemeyAllDie;//敌人死完了

    // Start is called before the first frame update
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

        // units.Add();
    }

    private void Start()
    {
        playerHP = playerMaxHp;
        playerCoin = 0;
        Init();
        UIManager.Instance.ChangeMainUIInBattle();//测试用,应该在选完角色后触发,用于改变金币和血
    }

    public void Update()
    {
        MainTurn();
    }

    private void Init()
    {
        // ChangeUI();
        //不在awake里改UI,因为UI层在start里才传入,故改做UI层触发changeUI
    }

    public void MainTurn()
    {
        if (!isBattle)
        {
            return;
        }
        if (isPlayerTurn)
        {
            return;
        }
        if (isEnemyTurn)
        {
            /* timeCounter += Time.deltaTime;
             Debug.Log(timeCounter);
             if (timeCounter > enemyTurnDuration)//TODO敌人动画播完回合结束,而不是时间
             {
                 timeCounter = 0f;
                 //玩家回合开始
                 PlayerTurnBegin();
                 isEnemyTurn = false;
             }*/
            isEnemyTurn = false;
            EnemyTurnBegin();
        }
    }

    public void PlayerTurnBegin()
    {
        isPlayerTurn = true;
        playerMana = playerMaxMana;
        turnCount++;
        playerTurnBegin.RaisEvent(this, this);

        UIManager.Instance.ChangeUIInBattle();
        CheckEnemyAlive();
    }

    public void PlayerTurnDown()
    {
        //Debug.Log("触发一次回合结束");
        playerTurnDown.RaisEvent(this, this);
        UIManager.Instance.ChangeUIInBattle();
    }

    public void EnemyTurnBegin()
    {
        {
            for (int i = 0; i < 6; i++)
            {
                if (units[i] != null)
                {
                    if (units[i].isDead == false)
                    {
                        isCharacter[i] = true;
                    }
                }
            }
        }//测试用
        battleCount = 0;
        characterList.Clear();
        for (int i = 0; i < 6; i++)
        {
            if (isCharacter[i])
            {
                characterList.Add(i);//如果那个为true,存入,比如045有东西,list里为0,4,5
            }
        }
        characterAct.RaisEvent(characterList[0], this);//广播位置第零个
    }

    public void RaisEventNext()//角色执行完后广播用这个接听接着执行下一个
    {
        if (battleCount < characterList.Count - 1)

        {
            characterAct.RaisEvent(characterList[battleCount + 1], this);
        }
        else
        {
            PlayerTurnBegin();
        }

        battleCount += 1;
    }

    /*  [ContextMenu("ChangeUI")]
      public void ChangeUI()
      {
          ChangeMainUI();
          manaText.text = playerMana.ToString();
          attackText.text = playerAttack.ToString();
          cardDeckText.text = CardDeck.Instance.drawDeck.Count.ToString();
          if (isEnemyTurn)
          {
              contextText.text = "敌人回合";
          }
          if (isPlayerTurn)
          {
              contextText.text = "你的回合";
          }
      }

      public void ChangeMainUI()
      {
          healthText.text = playerHP.ToString();
          coinText.text = playerCoin.ToString();
          float maxHP = playerMaxHp;
          float HP = playerHP;
          healthBar.fillAmount = HP / maxHP; //TODO 血条慢慢扣
      }
  */

    public void PlayerTakeDamage(int damage)
    {
        playerHP = playerHP - damage;
        if (playerHP < 0)
        {
            playerHP = 0;
            isPlayerDead = true;
        }
        UIManager.Instance.ChangeUIInBattle();
        return;
    }

    public void RegisterUnit(CharacterBase unit, int NO)//把战斗场景的角色注册进来
    {
        units[NO] = unit;
        Debug.Log("成功赋值unit" + NO);
    }

    public void UnregisterUnit(CharacterBase unit)
    {
        units.Remove(unit);
    }

    //把战斗场景的UIText Get了,顺序为血量,法力 攻击力,回合,
    //这里把main的UI也get了,包括血量和血条,金币,注意
    public void GetMainUICom(TextMeshProUGUI t1, Image h1, TextMeshProUGUI c1)
    {
        healthText = t1;
        coinText = c1;
        healthBar = h1;
    }

    public void GetBattleUICom(TextMeshProUGUI t2, TextMeshProUGUI t3, TextMeshProUGUI t4, TextMeshProUGUI t5)
    {
        attackText = t2;
        manaText = t3;

        contextText = t4;
        cardDeckText = t5;
    }

    /// <summary>
    /// 检查战斗场景里是否还有敌人,没有就加载胜利场景
    /// </summary>
    public void CheckEnemyAlive()
    {
        if (!isCharacter[3] && !isCharacter[4] && !isCharacter[5])
        {
            isBattle = false;
            CardDeck.Instance.DiscardAllCard();
            GameManager.Instance.LoadRewardScene();
            UIManager.Instance.Battle.SetActive(false);
            //  enemeyAllDie.RaisEvent(this, this);//触发game manager里的loadreward
        }
        else
        {
        }
    }

    public void FindAllPet()
    {
    }

    public void FindAllEnemy()
    {
    }

    [ContextMenu("测试游戏开始")]
    public void Test()
    {
    }

    public void BattleStart()
    {
        isBattle = true;
        turnCount = 1;
        isPlayerTurn = true;
        playerAttack = playerSettingAtk;
        playerMaxMana = playerSettingManaMax;
        playerMana = playerMaxMana;
        // PlayerTurnBegin();

        CardDeck.Instance.InitDeck();
        CardDeck.Instance.DrawCard(4);//TODO 抽卡数量这个属性写在角色data

        //初始化的时候读取
        UIManager.Instance.ChangeUIInBattle();
        InitCharacterList();
    }

    public void InitCharacterList()
    {
        for (int i = 0; i < 6; i++)
        {
            isCharacter.Add(false);

            //units[i].isDead = true;
            //isCharacter[i] = false;
        }

        for (int i = 0; i < 6; i++)
        {
            if (units[i] != null)
            {
                if (units[i].isDead == false)
                {
                    isCharacter[i] = true;
                }
            }
            else
            {
                isCharacter[i] = false;
            }
        }

        characterList.Clear();

        for (int i = 0; i < 6; i++)
        {
            if (isCharacter[i])
            {
                characterList.Add(i);//如果那个为true,存入,比如045有东西,list里为0,4,5
            }
        }
    }

    public void ChangeCharacterLIst()
    {
        characterList.Clear();
        for (int i = 0; i < 6; i++)
        {
            if (isCharacter[i])
            {
                characterList.Add(i);//如果那个为true,存入,比如045有东西,list里为0,4,5
            }
        }
    }

    public void ShuffleIntoDeckEffect(GameObject cardPrefab, CardDataSO cardData, Transform trans)
    {
        CardDeck.Instance.drawDeck.Add(cardData);
        for (int i = 0; i < CardDeck.Instance.drawDeck.Count; i++)
        {
            //打乱顺序
            CardDataSO temp = CardDeck.Instance.drawDeck[i];//
            int randomIndex = Random.Range(i, CardDeck.Instance.drawDeck.Count);
            CardDeck.Instance.drawDeck[i] = CardDeck.Instance.drawDeck[randomIndex];//
            CardDeck.Instance.drawDeck[randomIndex] = temp;
        }
        var c = Instantiate(cardPrefab, trans);
        // Vector3 worldPos = Camera.main.ScreenToWorldPoint(trans.);
        // 创建一个 Tween 序列
        c.transform.position = new Vector3(c.transform.position.x, c.transform.position.y + 4f, c.transform.position.z);
        Sequence seq = DOTween.Sequence();

        // 移动到目标位置值不通用
        seq.Join(c.transform.DOMove(new Vector3(-8.5f, -3f, 0), 0.5f).SetEase(Ease.Linear)).OnComplete(() =>
        {
            UIManager.Instance.ChangeUIInBattle();
            c.transform.DOKill();
            Destroy(c);
        }); ;

        // 同时缩小到 0
        seq.Join(c.transform.DOScale(Vector3.zero * 0.4f, 0.5f).SetEase(Ease.InQuad));

        // 完成后销毁自己

        seq.Play();
    }
}