using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneUI : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI healthText;

    public Image healthBar;

    public TextMeshProUGUI manaText;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI contextText;
    public TextMeshProUGUI cardDeckText;

    // Start is called before the first frame update
    private void Start()
    {
        // BattleManager.Instance.GetBattleUICom(attackText, manaText, contextText, cardDeckText);
        UIManager.Instance.attackText = attackText;
        UIManager.Instance.manaText = manaText;
        UIManager.Instance.contextText = contextText;
        UIManager.Instance.cardDeckText = cardDeckText;

        UIManager.Instance.ChangeUIInBattle();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}