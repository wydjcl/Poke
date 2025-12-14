using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickShakeEffectUI : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    [Header("使用注意,必须挂载到UI组件上,\n或者在相机添加物理射线")]
    [Header("旋转物体,默认为本体")]
    public GameObject Entry;

    [Header("旋转角度,默认15°")]
    public float angle;

    [Header("旋转时间,默认0.4")]
    public float shakeTime;

    private void Start()
    {
        if (Entry == null)
        {
            Entry = gameObject;
        }
        if (angle == 0)
        {
            angle = 15f;
        }
        if (shakeTime == 0)
        {
            shakeTime = 0.4f;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShakeToLeft();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Entry.transform.DOKill();
        Entry.transform.localRotation = Quaternion.identity;
    }

    private void ShakeToLeft()
    {
        Entry.transform.DORotate(new Vector3(0, 0, angle), shakeTime)
                 .SetEase(Ease.InOutSine) // 平滑曲线
                 .onComplete = () =>
                 {
                     ShakeToRight();
                 };
    }

    private void ShakeToRight()
    {
        Entry.transform.DORotate(new Vector3(0, 0, -angle), shakeTime)
                 .SetEase(Ease.InOutSine) // 平滑曲线
                 .onComplete = () =>
                 {
                     ShakeToLeft();
                 };
    }

    private void OnDisable()
    {
        Entry.transform.DOKill();
    }
}