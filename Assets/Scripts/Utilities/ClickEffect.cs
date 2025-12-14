using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /*注意!该脚本需挂载到UI层
    或者在相机添加PhysicsRaycaster,在里面添加Layer
    需提前导入DOTween包*/
    private Vector3 orScale;

    [Header("缩小程度,默认0.9")]
    public float degree = 0.9f;

    [Header("缩小时间,默认0.15")]
    public float time = 0.15f;

    [Header("点击后的广播")]
    public ObjectEventSO objectEventSO;

    // public IntEventSO intEventSO;

    private void Awake()
    {
        orScale = transform.localScale;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(orScale * 1f, time);
        if (objectEventSO != null)
        {
            objectEventSO.RaisEvent(this, this);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(orScale * degree, time);
    }

    private void OnDestroy()
    {
        // 杀掉所有和这个对象相关的 Tween
        transform.DOKill();
    }
}