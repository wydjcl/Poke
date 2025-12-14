using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUI : MonoBehaviour
{
    public bool isUpDownShake;
    public Vector3 orPos;

    private void Start()
    {
        orPos = transform.position;
        // 目标位置：当前位置 + Y轴方向10
        if (isUpDownShake)
        {
            ToUp();
        }
    }

    public void ToUp()//往上服气
    {
        // 使用 DOTween 移动到目标位置
        transform.DOMove(orPos + Vector3.up * 35, 1.8f)   // 2 秒完成
                  .SetEase(Ease.InOutSine)
                  .onComplete = () => { ToDown(); }
                  ;
    }

    public void ToDown()//往上服气
    {
        // 使用 DOTween 移动到目标位置
        transform.DOMove(orPos, 1.8f)   // 2 秒完成
                  .SetEase(Ease.InOutSine)
                  .onComplete = () => { ToUp(); }
                  ;
    }

    public void OnDestroy()
    {
        transform.DOKill();
    }
}