using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetInBarUI : MonoBehaviour
{
    public int ID;
    public Image PetIcon;
    public TextMeshProUGUI PetDes;

    // Start is called before the first frame update
    private void Start()
    {
        if (ID == 1)
        {
            RewardManager.Instance.petInBarUI1 = this;
        }

        if (ID == 2)
        {
            RewardManager.Instance.petInBarUI2 = this;
        }

        if (ID == 3)
        {
            RewardManager.Instance.petInBarUI3 = this;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}