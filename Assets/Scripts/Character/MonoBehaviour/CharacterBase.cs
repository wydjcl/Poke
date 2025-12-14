using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Debug = UnityEngine.Debug;
using UnityEngine.EventSystems;

public class CharacterBase : MonoBehaviour, IPointerEnterHandler
{
    [Header("定制参数")]
    public Vector3 orScale;

    public Vector3 takeDamagePos;
    public float takeDamageTextEndPosX;
    public float takeDamageTextEndPosY;
    public float takeDamageTextHeight;

    public float takeHealTextPosX;
    public float takeHealTextPosY;

    private float orBarScale;

    [Header("不需要添加的属性")]
    public int NO;//位于第几格子

    public bool isDead = false;
    public bool isEnemy = false;//是否是怪物,留个扩展
    public bool canGet = true;//可捕捉
    public bool canAddMana = true;//可以增加法力水晶
    /*    public int CurrentHP { get => hp.currentValue; set => hp.SetValue(value); }//赋值的时候启东事件
        public int MaxHP { get => hp.maxValue; }*/

    [Header("需要添加的属性")]
    public PetDataSO enemyData;

    public PetDataSO petData;

    [Header("Buff")]
    public int combo;//剩余攻击次数

    public int comboMax;//总攻击次数,为1攻击两次

    [Header("数值")]
    public int currentHP;

    public int maxHP;
    public int defense;
    public int attack;
    public int petMaxMana;
    public int petMana;
    public bool isEX;//是否觉醒

    [Header("不需要添加的组件")]
    public TextMeshPro healthText;

    public TextMeshPro attackText;

    public TextMeshPro defenseText;

    public TextMeshPro IntentionText;

    public TextMeshPro takeDamageTextT;
    public TextMeshPro manaTextT;

    public GameObject spriteG;//父物体
    public SpriteRenderer spriteS;//子物体sprite
    public GameObject defenseG;
    public GameObject takeDamageText;
    public SpriteRenderer healthBar;
    public SpriteRenderer armorBar;

    /* protected Animator animator;

     public GameObject buff;

     public GameObject debuff;
 */

    public virtual void Start()
    {
        Init();

        // ChangeIntentionUI();
    }

    public virtual void Init()
    {
        //读取组件
        healthText = transform.Find("UI/Health/HealthText").GetComponent<TextMeshPro>();
        attackText = transform.Find("UI/Attack/AttackText").GetComponent<TextMeshPro>();
        defenseText = transform.Find("UI/Defense/DefenseText").GetComponent<TextMeshPro>();
        IntentionText = transform.Find("UI/Intention/IntentionText").GetComponent<TextMeshPro>();
        takeDamageTextT = transform.Find("UI/TakeDamageText/TakeDamageText").GetComponent<TextMeshPro>();
        manaTextT = transform.Find("UI/Mana/ManaText").GetComponent<TextMeshPro>();
        spriteG = transform.Find("Sprite").gameObject;
        spriteS = transform.Find("Sprite/Sprite").GetComponent<SpriteRenderer>();
        defenseG = transform.Find("UI/Defense").gameObject;
        takeDamageText = transform.Find("UI/TakeDamageText").gameObject;
        // manaS = transform.Find("UI/Mana").gameObject.GetComponent<PetMana>();
        healthBar = transform.Find("UI/HealthBar/Health").gameObject.GetComponent<SpriteRenderer>();
        armorBar = transform.Find("UI/HealthBar/ArmorBar").gameObject.GetComponent<SpriteRenderer>();

        orBarScale = healthBar.transform.localScale.x;

        if (isEnemy)//是否是敌人读取两套数据
        {
            // spriteG.transform.localScale = new Vector3(-1f, 1f, 1f);

            maxHP = enemyData.health;
            attack = enemyData.attack;
            petMaxMana = enemyData.maxMana;
        }
        else
        {
            maxHP = petData.health;
            attack = petData.attack;

            petMaxMana = petData.maxMana;
        }
        currentHP = maxHP;
        defense = 0;
        orScale = transform.localScale;
        //  BattleManager.Instance.RegisterUnit(this, NO);//读取值
        // ResetDefense();
        ChangeUI();
    }

    public void GetNO(int no)
    {
        NO = no;
    }

    public void ChangeEnemyData()
    {
        isEnemy = true;
        if (isEnemy)//是否是敌人读取两套数据
        {
            //spriteG.transform.localScale = new Vector3(-1f, 1f, 1f);
            IntentionText.gameObject.SetActive(true);
            maxHP = enemyData.health;
            attack = enemyData.attack;
        }
        else
        {
            maxHP = petData.health;
            attack = petData.attack;
        }
        currentHP = maxHP;
        defense = 0;
        orScale = transform.localScale;

        ChangeUI();
    }

    public virtual void Act(int battleID)
    {
    }//角色行动

    public virtual void SmallSkill()
    {
    }

    public virtual void BigSkill()
    {
    }

    public virtual void ExSkill()
    {
    }

    public virtual void RollSkill()
    {
    }

    public virtual int TakeDamage(int damage)
    {
        if (defense > damage)
        {
            defense -= damage;
            damage = 0;
            return 0;
        }
        else
        {
            currentHP = currentHP + defense - damage;

            damage = damage - defense;
            defense = 0;

            if (currentHP <= 0)
            {
                currentHP = 0;
                isDead = true;
                if (NO == 0)
                {
                    UIManager.Instance.PetBox1.isFull = false;
                }
                if (NO == 1)
                {
                    UIManager.Instance.PetBox2.isFull = false;
                }
                if (NO == 2)
                {
                    UIManager.Instance.PetBox3.isFull = false;
                }

                BattleManager.Instance.isCharacter[NO] = false;
                BattleManager.Instance.characterList.Remove(NO);
                // UnityEngine.Debug.Log("死了第" + NO);
                transform.gameObject.SetActive(false);
            }
        }
        takeDamagePos = takeDamageText.transform.position;

        takeDamageTextT.text = (-damage).ToString();

        takeDamageTextT.color = Color.red;
        takeDamageText.SetActive(true);
        takeDamageText.transform.DOLocalJump(new Vector3(takeDamageTextEndPosX, takeDamageTextEndPosY), 1f, 1, 0.4f).onComplete = () =>
        {
            takeDamageText.SetActive(false);
            takeDamageText.transform.position = takeDamagePos;
        };
        //takeDamageEffect.enabled = true;
        spriteS.DOFade(0.5f, 0.1f).onComplete = () =>
        {
            spriteS.DOFade(1f, 0.1f).onComplete = () =>
            {
                spriteS.DOKill();
            };
            //takeDamageEffect.enabled = false;
        };
        /*      takeDamageText.transform.DOMove(new Vector3(takeDamageTextEndPosX - (takeDamageTextEndPosX-takeDamageTextPosX)/2,takeDamageTextPosY+takeDamageTextHeight),takeDamageText.transform.position.z,0.5f)*/

        // takeDamageEffect.DOFade(0f, 2f);
        ChangeUI();
        return damage;
    }

    public virtual void TakeDefense(int i)
    {
        defense += i;
        ChangeUI();
    }

    public virtual void SummonPet(int id, GameObject petPrefab)
    {
    }

    public void UpdateDefense()
    {
        // var value = defense.currentValue + amount;
        //defense.SetValue(value);
        defense = 0;
        ChangeUI();
    }

    public void ResetDefense()
    {
        //  defense.SetValue(0);
        ChangeUI();
    }

    public void HealHealth(int heal)
    {
        currentHP += heal;
        if (currentHP > maxHP)
        {
            heal = maxHP - currentHP;
            currentHP = maxHP;
        }
        takeDamagePos = takeDamageText.transform.position;

        takeDamageTextT.text = (heal).ToString();
        takeDamageTextT.color = Color.green;
        takeDamageText.SetActive(true);
        takeDamageText.transform.DOLocalJump(new Vector3(takeDamageTextEndPosX, takeDamageTextEndPosY), 1f, 1, 0.4f).onComplete = () =>
        {
            takeDamageText.SetActive(false);
            takeDamageText.transform.position = takeDamagePos;
            takeDamageText.transform.DOKill();
        };
        ChangeUI();
    }

    [ContextMenu("ChangeUI")]
    public void ChangeUI()
    {
        healthText.text = currentHP.ToString();
        attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
        manaTextT.text = (petMana + "/" + petMaxMana);
        // healthBar.transform.localScale = new Vector3(currentHP * orBarScale / maxHP, healthBar.transform.localScale.y, healthBar.transform.localScale.z);

        if (isEnemy)
        {
            // spriteG.transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (defense > 0)
        {
            defenseG.gameObject.SetActive(true);
            armorBar.gameObject.SetActive(true);
        }
        else
        {
            defenseG.gameObject.SetActive(false);
            armorBar.gameObject.SetActive(false);
        }
    }

    public virtual void ChangeIntentionUI()
    {
        if (!isEnemy)
        {
            return;
        }
        IntentionText.gameObject.SetActive(true);
    }

    public void DisableIntenTionUI()
    {
        if (!isEnemy)
        {
            return;
        }
        //UnityEngine.Debug.Log("执行一次");
        IntentionText.gameObject.SetActive(false);
    }

    public int FindPetTarget()//找第几个格子有宠物,都没有就返回-1
    {
        if (BattleManager.Instance.isCharacter[0] == true)
        {
            return 0;
        }
        else
        {
            if (BattleManager.Instance.isCharacter[1] == true)
            {
                return 1;
            }
            else
            {
                if (BattleManager.Instance.isCharacter[2] == true)
                {
                    return 2;
                }
                else
                {
                    return -1;
                }
            }
        }
    }

    public int FindEnemyTarget()//找第几个格子有敌人,都没有就返回-1
    {
        if (BattleManager.Instance.isCharacter[3] == true)
        {
            return 3;
        }
        else
        {
            if (BattleManager.Instance.isCharacter[4] == true)
            {
                return 4;
            }
            else
            {
                if (BattleManager.Instance.isCharacter[5] == true)
                {
                    return 5;
                }
                else
                {
                    return -1;
                }
            }
        }
    }

    public void ChangeMana()//回合开始水晶+1
    {
        if (!canAddMana)
        {
            canAddMana = true;
            return;
        }
        if (petMana < petMaxMana)
        {
            petMana++;//回合开始水晶增加
            //TextEffectManager.Instance.TextUpEffect("法力水晶增长", gameObject);
            ChangeUI();
        }
    }

    private void OnDisable()
    {
        transform.DOKill();
        this.DOKill();
    }

    public virtual void OnDestroy()
    {
        // 杀掉所有和这个对象相关的 Tween
        transform.DOKill();
        this.DOKill();
    }

    public virtual void PlayerTurnBegin()
    {
        ChangeMana();
        //  ChangeIntentionUI();
        UpdateDefense();
        combo = comboMax;
    }

    public virtual void PlayerTurnEnd()
    {
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (!isEnemy)
        {
            UIManager.Instance.ChangeCardDesText(petData.skillDes);
            UIManager.Instance.ChangePetDataText(petData.des);
            UIManager.Instance.ChangPetNameText(petData.petName);
        }
        else
        {
            UIManager.Instance.ChangeCardDesText(enemyData.skillDes);
            UIManager.Instance.ChangePetDataText(enemyData.des);
            UIManager.Instance.ChangPetNameText(enemyData.petName);
        }
    }
}