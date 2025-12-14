using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KuluBoss : CharacterBase
{
    // Start is called before the first frame update
    [Header("骷髅女皇")]
    public GameObject KuluPrefab;

    public GameObject KuluBombPrefab;
    public GameObject KuluMagicPrefab;
    public bool isSum;//发动了召唤
    public bool isTun;//吞噬小怪
    private List<GameObject> KuluList = new List<GameObject>();

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (isEnemy)
        {
        }
        KuluList.Clear();
        KuluList.Add(KuluMagicPrefab);
        KuluList.Add(KuluBombPrefab);
        KuluList.Add(KuluPrefab);
    }

    // Update is called once per frame
    public override void Act(int battleID)
    {
        base.Act(battleID);//不是这个id退出
        if (battleID != NO || isDead)
        {
            return;//不是自己的格子不执行
        }

        if (!isEnemy)
        {
            transform.DOScale(orScale * 0.7f, 0.2f).onComplete = () =>
            {
                transform.DOScale(orScale, 0.2f).onComplete = () =>
                {
                    if (petMana == petMaxMana)
                    {
                        canAddMana = false;
                        petMana = 0;
                        //manaS.ChangeManaUI(petMana);
                        BigSkill();
                    }
                    else
                    {
                        SmallSkill();
                    }
                    BattleManager.Instance.RaisEventNext();
                };
            };
            ;
            //
            /* if (BattleManager.Instance.battleCount <= BattleManager.Instance.characterList.Count)//如果下一个广播对象没超数字
             {
                 BattleManager.Instance.RaisEventNext(BattleManager.Instance.battleCount);
             }*/
        }
        if (isEnemy)
        {
            var target = FindPetTarget();
            transform.DOScale(orScale * 0.7f, 0.2f).onComplete = () =>
            {
                transform.DOScale(orScale, 0.2f).onComplete = () =>
                {
                    if (petMana == petMaxMana)
                    {
                        canAddMana = false;
                        if (BattleManager.Instance.isCharacter[3] != false || BattleManager.Instance.isCharacter[5] != false)
                        {
                            petMana = 0;
                            BigSkill();//如果有空位,就执行吞噬
                        }
                        else
                        {
                            SmallSkill();//一个空位都没有,执行小技能
                        }
                    }
                    else
                    {
                        SmallSkill();//水晶不满执行小技能
                    }
                    BattleManager.Instance.RaisEventNext();
                };
            };
            ;
        }
    }

    //一般概率召唤,一半平a
    public override void SmallSkill()
    {
        var target = FindPetTarget();
        int r = UnityEngine.Random.Range(0, 2);
        if (r == 0)
        {
            if (BattleManager.Instance.isCharacter[3] == false || BattleManager.Instance.isCharacter[5] == false)
            {
                isSum = true;
            }
            else
            {
                if (target == -1)
                {
                    BattleManager.Instance.PlayerTakeDamage(attack);
                    return;
                }
                else
                {
                    BattleManager.Instance.units[target].TakeDamage(attack);
                }
            }
        }
        else
        {
            if (target == -1)
            {
                BattleManager.Instance.PlayerTakeDamage(attack);
                return;
            }
            else
            {
                BattleManager.Instance.units[target].TakeDamage(attack);
            }
        }
    }

    public override void BigSkill()
    {
        if (BattleManager.Instance.isCharacter[3] != false || BattleManager.Instance.isCharacter[5] != false)
        {
            isTun = true;
        }
    }

    public override void ExSkill()
    {
        var target = FindEnemyTarget();
        if (target == -1)
        {
            return;
        }
        else
        {
            BattleManager.Instance.units[target].TakeDamage(attack * 2);
        }
    }

    public override void ChangeIntentionUI()
    {
        base.ChangeIntentionUI();
        if (!isEnemy)
        {
            return;
        }
        if (BattleManager.Instance.turnCount % 2 == 0)//偶数回合
        {
            IntentionText.text = "";
        }
        else
        {
            IntentionText.text = "";
        }
    }

    public GameObject RollSum()
    {
        int i = UnityEngine.Random.Range(0, 3);
        Debug.Log("i" + i);
        if (i == 0)
        {
            return KuluBombPrefab.gameObject;
        }
        if (i == 1)
        {
            return KuluMagicPrefab.gameObject;
        }
        if (i == 2)
        {
            return KuluPrefab.gameObject;
        }
        else
        {
            return KuluPrefab.gameObject;
        }
    }

    public void SummonKulu()
    {
        if (isSum)
        {
            if (BattleManager.Instance.isCharacter[3] == false)
            {
                var p = Instantiate(RollSum(), new Vector3(-3f, 1f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
                CharacterBase c = p.GetComponent<CharacterBase>();
                c.NO = 3;
                BattleManager.Instance.units[3] = c;
                BattleManager.Instance.isCharacter[3] = true;
                BattleManager.Instance.ChangeCharacterLIst();
            }
            if (BattleManager.Instance.isCharacter[5] == false)
            {
                var p = Instantiate(RollSum(), new Vector3(3f, 1f, 0), Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
                CharacterBase c = p.GetComponent<CharacterBase>();
                c.NO = 5;
                BattleManager.Instance.units[5] = c;
                BattleManager.Instance.isCharacter[5] = true;
                BattleManager.Instance.ChangeCharacterLIst();
            }
            isSum = false;
        }
    }

    public void TunKulu()
    {
        if (isTun)
        {
            if (BattleManager.Instance.isCharacter[3] != false)
            {
                maxHP += BattleManager.Instance.units[3].currentHP;
                currentHP += BattleManager.Instance.units[3].currentHP;
                attack += BattleManager.Instance.units[3].attack;

                ChangeUI();
                BattleManager.Instance.units[3].TakeDamage(9999);
            }
            if (BattleManager.Instance.isCharacter[5] != false)
            {
                maxHP += BattleManager.Instance.units[5].currentHP;
                currentHP += BattleManager.Instance.units[5].currentHP;
                attack += BattleManager.Instance.units[5].attack;

                ChangeUI();
                BattleManager.Instance.units[5].TakeDamage(9999);
            }
            isTun = false;
        }
    }

    public override void PlayerTurnBegin()
    {
        base.PlayerTurnBegin();
        SummonKulu();
        TunKulu();
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}