using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.UIElements;

public class RoomCard : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public bool isChosen;
    public bool isLock;
    public RoomType roomType;
    public SpriteRenderer sp;
    public GameObject lockSp;
    public GameObject Entry;

    [Header("战斗用")]
    public ObjectEventSO startBattle;

    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemyMain;
    private CharacterBase enemy1C;
    private CharacterBase enemy2C;
    private CharacterBase enemy3C;
    private CharacterBase enemyMainC;

    [Header("导入的图片")]
    public Sprite spTreasure;

    public Sprite spEnemy;

    public Sprite spBoss;

    public Sprite spBar;

    public Sprite spShop;

    private void Start()
    {
        //  Debug.Log(roomType);
        if (roomType == RoomType.NormalEnemy)
        {
            sp.sprite = spEnemy;
        }
        if (roomType == RoomType.Treasure)
        {
            sp.sprite = spTreasure;
        }
        if (roomType == RoomType.PetBar)
        {
            sp.sprite = spBar;
        }
        if (roomType == RoomType.Shop)
        {
            sp.sprite = spShop;
        }
        if (roomType == RoomType.Boss)
        {
            sp.sprite = spBoss;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLock)
        {
            return;
        }
        ShakeToLeft();

        if (isChosen == false)
        {
            isChosen = true;

            return;
        }
        if (isChosen == true)
        {
            if (roomType == RoomType.NormalEnemy || roomType == RoomType.BigEnemy || roomType == RoomType.Boss)
            {
                if (roomType == RoomType.NormalEnemy)
                {
                    GameManager.Instance.GetEnemyByLevel(GameManager.Instance.bigLevel, false);
                }
                if (roomType == RoomType.Boss)
                {
                    GameManager.Instance.GetBoss(GameManager.Instance.bigLevel);
                }
                // GameManager.Instance.GetEnemy(enemy1, enemy2, enemy3);

                startBattle.RaisEvent(this, this);
            }
            if (roomType == RoomType.Treasure)
            {
                GameManager.Instance.LoadTreasureScene();
            }
            if (roomType == RoomType.Shop)
            {
                GameManager.Instance.LoadShopScene();
            }
            if (roomType == RoomType.PetBar)
            {
                GameManager.Instance.LoadBarScene();
            }

            isLock = true;
            lockSp.SetActive(true);
            Entry.transform.DOKill();
            Entry.transform.localRotation = Quaternion.identity;
            UIManager.Instance.NextLevelButtom.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isChosen = false;
        Entry.transform.DOKill();
        Entry.transform.localRotation = Quaternion.identity;
    }

    public void GetEnemyCharacter()
    {
        if (enemy1 != null)
        {
            enemy1C = enemy1.GetComponent<CharacterBase>();
            enemy1C.isEnemy = true;
        }
        if (enemy2 != null)
        {
            enemy2C = enemy2.GetComponent<CharacterBase>();
            enemy2C.isEnemy = true;
        }

        if (enemy3 != null)
        {
            enemy3C = enemy3.GetComponent<CharacterBase>();
            enemy3C.isEnemy = true;
        }
    }

    private void ShakeToLeft()
    {
        Entry.transform.DORotate(new Vector3(0, 0, 15f), 0.35f)
                 .SetEase(Ease.InOutSine) // 平滑曲线
                 .onComplete = () =>
                 {
                     ShakeToRight();
                 };
    }

    private void ShakeToRight()
    {
        Entry.transform.DORotate(new Vector3(0, 0, -15f), 0.35f)
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