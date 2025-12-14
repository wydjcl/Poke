using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    public Image healthBar;

    public TextMeshProUGUI coinText;

    public GameObject MainSceneFather;
    public GameObject PetBattleSpace;

    public TextMeshProUGUI CardDesText;
    public TextMeshProUGUI PetDesText;
    public TextMeshProUGUI PetDataDesText;
    public TextMeshProUGUI PetNameDesText;

    public GameObject PetBox1;
    public GameObject PetBox2;
    public GameObject PetBox3;

    public GameObject Battle;//放战斗场景的东西,平常关闭以不遮挡

    public GameObject NextLevelButtom;

    public GameObject RoomCards;

    private void Awake()
    {
        // BattleManager.Instance.GetMainUICom(healthText, healthBar, coinText);
        GameManager.Instance.GetMainSceneFather(MainSceneFather);//讨巧在这里传
        GameManager.Instance.RoomCards = RoomCards;
        UIManager.Instance.GetMainDes(CardDesText, PetDesText, PetDataDesText, PetNameDesText);
        UIManager.Instance.GetPetBox(PetBox1, PetBox2, PetBox3);
        UIManager.Instance.healthText = healthText;
        UIManager.Instance.coinText = coinText;
        UIManager.Instance.PetBattleSpace = PetBattleSpace;
        UIManager.Instance.NextLevelButtom = NextLevelButtom;
        UIManager.Instance.Battle = Battle;
    }
}