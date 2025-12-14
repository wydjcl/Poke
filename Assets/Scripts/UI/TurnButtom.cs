using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnButtom : MonoBehaviour, IPointerClickHandler
{
    public Image image; // 在 Inspector 里拖拽赋值

    // 设置透明度

    private void Awake()
    {
    }

    private void Update()
    {
        if (BattleManager.Instance.isPlayerTurn != true)
        {
            var a = gameObject.GetComponent<ClickEffect>();
            a.enabled = false;
            Color c = image.color;
            c.a = Mathf.Clamp01(0.3f); // 保证在 0~1 之间
            image.color = c;
        }
        else
        {
            var a = gameObject.GetComponent<ClickEffect>();
            a.enabled = true;

            Color c = image.color;
            c.a = Mathf.Clamp01(1f); // 保证在 0~1 之间
            image.color = c;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        BattleManager.Instance.CheckEnemyAlive();
        if (BattleManager.Instance.isPlayerTurn != true)
        {
            Debug.Log("失败点一次");
            return;
        }

        BattleManager.Instance.isEnemyTurn = true;
        BattleManager.Instance.isPlayerTurn = false;

        BattleManager.Instance.PlayerTurnDown();
    }

    private void OnDestroy()
    {
        transform.DOKill();
        image.DOKill();
    }
}