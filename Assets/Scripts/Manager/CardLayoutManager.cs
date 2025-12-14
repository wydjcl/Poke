using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public float maxWidth = 7f;//
    public float cardSpacing = 1.3f;//卡牌间隙

    [Header("弧形参数")]
    public Vector3 centerPoint;

    [SerializeField]
    private List<Vector3> cardPositions = new();//卡牌坐标

    [SerializeField]
    private List<Quaternion> cardRotations = new();//角度

    private void Awake()
    {
        centerPoint = Vector3.up * -4.5f;// Vector3.up * -21.5f;
    }

    public CardTransForm GetCardTransForm(int index, int totalCards)//第几,总共几
    {
        CalculatePosition(totalCards);

        return new CardTransForm(cardPositions[index], cardRotations[index]);
    }

    private void CalculatePosition(int numberOfCards)
    {
        cardPositions.Clear();
        cardRotations.Clear();//每次计算要清空

        float currentWidth = cardSpacing * (numberOfCards - 1);//卡牌间隙长,如2张牌就只有一个间隙s
        float totalWidth = Mathf.Min(currentWidth, maxWidth);//如果总宽大于我们设定的

        float currentSpacing = totalWidth > 0 ? totalWidth / (numberOfCards - 1) : 0;//卡牌间隙
        for (int i = 0; i < numberOfCards; i++)
        {
            float xPox = 0 - (totalWidth / 2) + i * currentSpacing;//卡坐标
            var pos = new Vector3(xPox, centerPoint.y, -i * 0.01f);
            var rotation = Quaternion.identity;

            cardPositions.Add(pos);
            cardRotations.Add(rotation);
        }
    }

    /*    private Vector3 FanCardPositiong(float angle)
        {
            //数学方法角度转弧度
            return new Vector3(
                centerPoint.x - Mathf.Sin(Mathf.Deg2Rad * angle) * radius,
                centerPoint.y + Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
                0
                );
        }*/
}