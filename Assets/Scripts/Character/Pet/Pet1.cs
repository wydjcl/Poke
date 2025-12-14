using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pet1 : CharacterBase
{
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
        var num = battleID;
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
                        //  manaS.ChangeManaUI(petMana);
                        BigSkill();
                    }
                    else
                    {
                        SmallSkill();
                    }
                    if (combo > 0)
                    {
                        combo -= 1;
                        petMana += 1;
                        ChangeUI();
                        Act(num);
                    }
                    else
                    {
                        BattleManager.Instance.RaisEventNext();
                    }
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
                    if (BattleManager.Instance.turnCount % 2 == 0)//偶数回合
                    {
                        HealHealth(10);
                    }
                    else
                    {
                        if (target != -1)
                        {
                            BattleManager.Instance.units[target].TakeDamage(attack);
                        }
                        else
                        {
                            BattleManager.Instance.PlayerTakeDamage(attack);
                        }
                    }
                    BattleManager.Instance.RaisEventNext();
                };
            };
            ;
        }
    }

    public override void SmallSkill()
    {
        var target = FindEnemyTarget();
        if (target == -1)
        {
            return;
        }
        else
        {
            BattleManager.Instance.units[target].TakeDamage(attack);
        }
    }

    public override void BigSkill()
    {
        var target = FindEnemyTarget();
        if (target == -1)
        {
            return;
        }
        else
        {
            //造成攻击力伤害的1.5倍,并减少敌人一个法力水晶
            if (BattleManager.Instance.units[target].petMana > 0)
            {
                BattleManager.Instance.units[target].petMana -= 1;
                // BattleManager.Instance.units[target].manaS.ChangeManaUI(BattleManager.Instance.units[target].petMana);
            }
            BattleManager.Instance.units[target].TakeDamage(Mathf.FloorToInt(attack * 1.5f));
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
            IntentionText.text = "治疗\"10\"";
        }
        else
        {
            IntentionText.text = "普通攻击\"10\"";
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}