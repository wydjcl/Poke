using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Kulu : CharacterBase
{
    [Header("是否是自爆兵")]
    public bool isNormal;

    public bool isBomb;
    public bool isMagic;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (isEnemy)
        {
            //IntentionText.text = "普通攻击\"10\"";
        }
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
        }
    }

    public override void SmallSkill()
    {
        var target = FindPetTarget();
        if (isBomb)
        {
            if (target == -1)
            {
                BattleManager.Instance.PlayerTakeDamage(attack);
                return;
            }
            else
            {
                //造成攻击力加护甲值的伤害
                BattleManager.Instance.units[target].TakeDamage(attack);
                //TODO写成一个洗入卡组的单例方法
            }
        }
        if (isNormal)
        {
            if (target == -1)
            {
                BattleManager.Instance.PlayerTakeDamage(attack);
                return;
            }
            else
            {
                //造成攻击力加护甲值的伤害
                BattleManager.Instance.units[target].TakeDamage(attack);
                //TODO写成一个洗入卡组的单例方法
            }
        }
        if (isMagic)
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
        var target = FindPetTarget();
        var targetE = FindEnemyTarget();
        if (isBomb)//如果是炸弹兵,死了造成生命值*2的伤害
        {
            if (target == -1)
            {
                BattleManager.Instance.PlayerTakeDamage(currentHP * 2);
                return;
            }
            else
            {
                BattleManager.Instance.units[target].TakeDamage(Mathf.FloorToInt(currentHP * 2));
                ChangeUI();
                TakeDamage(999);
            }
        }
        if (isNormal)
        {
            if (target == -1)
            {
                BattleManager.Instance.PlayerTakeDamage(Mathf.FloorToInt(attack * 1.5f));
                return;
            }
            else
            {
                ChangeUI();
                BattleManager.Instance.units[target].TakeDamage(Mathf.FloorToInt(attack * 1.5f));
            }
        }
        if (isMagic)
        {
            BattleManager.Instance.units[targetE].HealHealth(10);
            BattleManager.Instance.units[targetE].attack += 5;
            BattleManager.Instance.units[targetE].ChangeUI();
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

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}