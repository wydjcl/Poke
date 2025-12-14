using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SlimeKing : CharacterBase
{
    public GameObject nianyePrefab;
    public CardDataSO nianyeData;

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
        if (target == -1)
        {
            BattleManager.Instance.ShuffleIntoDeckEffect(nianyePrefab, nianyeData, transform);

            BattleManager.Instance.PlayerTakeDamage(attack);
            return;
        }
        else
        {
            //造成攻击力加护甲值的伤害
            BattleManager.Instance.units[target].TakeDamage(attack);
            //TODO写成一个洗入卡组的单例方法

            BattleManager.Instance.ShuffleIntoDeckEffect(nianyePrefab, nianyeData, transform);
        }
    }

    public override void BigSkill()
    {
        var target = FindPetTarget();
        if (target == -1)
        {
            BattleManager.Instance.PlayerTakeDamage(attack * 2);
            return;
        }
        else
        {
            ChangeUI();
            for (int i = 0; i < 3; i++)
            {
                if (BattleManager.Instance.units[i] != null)
                {
                    BattleManager.Instance.units[i].TakeDamage(Mathf.FloorToInt(attack * 2));
                }
            }
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