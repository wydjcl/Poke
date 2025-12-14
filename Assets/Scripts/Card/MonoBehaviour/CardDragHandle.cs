using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private bool canMove;

    private Card currentCard;

    [SerializeField]
    private bool canEcecute;//可执行,可使用

    private CharacterBase targetCharacter;
    public GameObject arrowPrefab;
    public PetBox petBox;

    private GameObject currentArrow;
    //  public Player player;

    private void Awake()
    {
        currentCard = GetComponent<Card>();//挂载的卡脚本
        //player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnEnable()
    {
        currentCard.isAvailiable = true;
    }

    private void OnDisable()
    {
        canMove = false;
        canEcecute = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (currentCard.cardData.cost > BattleManager.Instance.playerMana)
        {
            return;
        }
        if (currentCard.isAvailiable == false)
        {
            return;
        }
        if (currentCard.cardData.cardType == CardType.Attack)
        {
            currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            canMove = false;
        }
        if (currentCard.cardData.cardType == CardType.Ability)
        {
            canMove = true;
        }
        if (currentCard.cardData.cardType == CardType.Affect)
        {
            canMove = true;
        }
        if (currentCard.cardData.cardType == CardType.Pet)
        {
            currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

            canMove = false;
        }
        if (currentCard.cardData.cardType == CardType.PAffect)
        {
            currentArrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            canMove = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentCard.cardData.cost > BattleManager.Instance.playerMana)
        {
            return;
        }
        if (currentCard.isAvailiable == false)
        {
            return;
        }
        if (canMove)//效果能力
        {
            //拖卡牌实现
            currentCard.isAni = true;
            Vector3 screenPos = new(Input.mousePosition.x, Input.mousePosition.y, 10);// 屏幕坐标0-1920
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            currentCard.transform.position = worldPos;//世界坐标 0-20

            canEcecute = worldPos.y > -1.8f;//拉到1f上就用用掉
                                            // targetCharacter = player;
        }
        else//宠物,攻击
        {
            if (eventData.pointerEnter == null)
            {
                canEcecute = false;
                targetCharacter = null;
                // Debug.Log("没对象");
                return;
            }
            if (currentCard.cardData.cardType == CardType.Pet)
            {
                UIManager.Instance.ActPetBox();
            }
            if (eventData.pointerEnter.CompareTag("PetBox") && currentCard.cardData.cardType == CardType.Pet)

            {
                petBox = eventData.pointerEnter.GetComponent<PetBox>();
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                if (petBox.isFull == false)
                {
                    canEcecute = true;
                }
                // Debug.Log("找到盒子");
                return;
                //弃置
            }
            if (eventData.pointerEnter.CompareTag("Pet") && currentCard.cardData.cardType == CardType.Attack)
            {
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                if (targetCharacter.isEnemy == true)
                {
                    canEcecute = true;
                }
                return;
            }
            if (eventData.pointerEnter.CompareTag("Pet") && currentCard.cardData.cardType == CardType.PAffect)
            {
                targetCharacter = eventData.pointerEnter.GetComponent<CharacterBase>();
                if (targetCharacter.isEnemy == false)
                {
                    canEcecute = true;
                }
                return;
            }

            canEcecute = false;
            targetCharacter = null;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.DisPetBox();
        if (currentCard.cardData.cost > BattleManager.Instance.playerMana)
        {
            return;
        }
        if (currentCard.isAvailiable == false)
        {
            return;
        }
        if (canEcecute)//效果,能力
        {
            currentCard.ReSetCard();
            currentCard.ExecuteCardEffects(targetCharacter);
        }
        else
        {
            currentCard.ReSetCard();
            currentCard.isAni = false;
        }
        if (currentArrow != null)
        {
            Destroy(currentArrow.gameObject);
        }
    }
}