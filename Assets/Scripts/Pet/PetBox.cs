using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PetBox : CharacterBase
{
    [Header("PetBox")]
    public bool isFull;// «∑Ò”–≥ËŒÔ

    public GameObject ui;

    private void Awake()
    {
        isFull = false;
        gameObject.SetActive(false);
    }

    public override void Init()
    {
    }

    public override void SummonPet(int id, GameObject petPrefab)
    {
        if (isFull == false)
        {
            var p = Instantiate(petPrefab, transform.position, Quaternion.identity, UIManager.Instance.PetBattleSpace.transform);
            CharacterBase c = p.GetComponent<CharacterBase>();
            c.NO = NO;
            BattleManager.Instance.units[NO] = c;
            BattleManager.Instance.isCharacter[NO] = true;
            BattleManager.Instance.ChangeCharacterLIst();
            // p.transform.localScale = new Vector3(7, 7, 7);

            isFull = true;
            gameObject.SetActive(false);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
    }
}