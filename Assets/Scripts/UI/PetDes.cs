using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PetDes : MonoBehaviour
{
    public GameObject PetCardFather;
    public GameObject Prefab;

    public TextMeshProUGUI smallSkillText;
    public TextMeshProUGUI bigSkillText;
    public TextMeshProUGUI exSkillText;

    private void OnEnable()
    {
        Prefab = Instantiate(UIManager.Instance.clickPetPrefab, PetCardFather.transform);
        smallSkillText.text = UIManager.Instance.clickPetData.smallSkill.ToString();
        bigSkillText.text = UIManager.Instance.clickPetData.bigSkill.ToString();
        if (UIManager.Instance.clickPetEX)
        {
            exSkillText.text = UIManager.Instance.clickPetData.exSkill.ToString();
        }
        else
        {
            exSkillText.text = "ÔÝÎ´¾õÐÑ";
        }
    }

    private void OnDisable()
    {
        Destroy(Prefab);
    }
}