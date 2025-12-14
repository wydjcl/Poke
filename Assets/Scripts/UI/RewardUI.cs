using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isTreasure;

    public TextMeshProUGUI coinText;

    private void Start()
    {
        RewardManager.Instance.GetRewardUI(this);
        RewardManager.Instance.coinText = coinText;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}