using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public PetDataSO clickPetData;//点击的宠物数据
    public int clickHealth;
    public GameObject clickPetPrefab;//点击的宠物预制体
    public bool clickPetEX;

    public GameObject PetBattleSpace;

    [Header("主界面")]
    public TextMeshProUGUI cardDesText;

    public TextMeshProUGUI petDesText;
    public TextMeshProUGUI petNameText;
    public TextMeshProUGUI petDataText;

    [Header("战斗场景人物")]
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI manaText;
    public TextMeshProUGUI contextText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI cardDeckText;

    public PetBox PetBox1;
    public PetBox PetBox2;
    public PetBox PetBox3;

    public GameObject NextLevelButtom;
    public GameObject Battle;

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
    }

    public void GetMainDes(TextMeshProUGUI cardDes, TextMeshProUGUI petDes, TextMeshProUGUI petDataDes, TextMeshProUGUI petNameDes)
    {
        cardDesText = cardDes;
        petDesText = petDes;
        petDataText = petDataDes;
        petNameText = petNameDes;
    }

    public void GetPetBox(GameObject B1, GameObject B2, GameObject B3)
    {
        PetBox1 = B1.GetComponent<PetBox>();
        PetBox2 = B2.GetComponent<PetBox>();
        PetBox3 = B3.GetComponent<PetBox>();
    }

    public void ChangeCardDesText(string s)
    {
        cardDesText.text = s;
    }

    public void ChangePetDesText(string s)
    {
        petDesText.text = s;
    }

    public void ChangePetDataText(string s)
    {
        petDataText.text = s;
    }

    public void ChangPetNameText(string s)
    {
        petNameText.text = s;
    }

    public void ActPetBox()
    {
        if (PetBox1.isFull)
        {
            PetBox1.gameObject.SetActive(false);
        }
        else
        {
            PetBox1.gameObject.SetActive(true);
        }
        if (PetBox2.isFull)
        {
            PetBox2.gameObject.SetActive(false);
        }
        else
        {
            PetBox2.gameObject.SetActive(true);
        }
        if (PetBox3.isFull)
        {
            PetBox3.gameObject.SetActive(false);
        }
        else
        {
            PetBox3.gameObject.SetActive(true);
        }
    }

    public void DisPetBox()
    {
        PetBox1.gameObject.SetActive(false);
        PetBox2.gameObject.SetActive(false);
        PetBox3.gameObject.SetActive(false);
    }

    public void ChangeNextLevelButtom()
    {
        if (GameManager.Instance.CheckRoom())//如果有敌人
        {
            NextLevelButtom.SetActive(false);
        }
        else
        {
            NextLevelButtom.SetActive(true);
        }
    }

    /// <summary>
    /// 改变攻击力,法力值,卡组剩余
    /// </summary>
    public void ChangeUIInBattle()
    {
        ChangeMainUIInBattle();
        manaText.text = BattleManager.Instance.playerMana.ToString();
        attackText.text = BattleManager.Instance.playerAttack.ToString();
        cardDeckText.text = CardDeck.Instance.drawDeck.Count.ToString();
        if (BattleManager.Instance.isEnemyTurn)
        {
            contextText.text = "敌人回合";
        }
        if (BattleManager.Instance.isPlayerTurn)
        {
            contextText.text = "你的回合";
        }
    }

    /// <summary>
    /// 改变生命值和金钱
    /// </summary>
    public void ChangeMainUIInBattle()
    {
        healthText.text = BattleManager.Instance.playerHP.ToString();
        coinText.text = BattleManager.Instance.playerCoin.ToString();
        /*  float maxHP = playerMaxHp;
          float HP = playerHP;
          healthBar.fillAmount = HP / maxHP; //TODO 血条慢慢扣*/
    }
}