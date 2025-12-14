using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class SnowFoxP : CharacterBase
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (isEnemy)
        {
            IntentionText.text = "普通攻击\"10\"";
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
                    RollSkill();
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
            BattleManager.Instance.units[target].TakeDamage(attack / 2);
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
            BattleManager.Instance.units[target].TakeDamage(attack);
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