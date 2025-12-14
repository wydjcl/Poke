using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDesUI : MonoBehaviour
{
    public GameObject PetDes;

    public GameObject PetCardPrefab;

    public void OpenPetDesr()
    {
        PetDes.SetActive(true);
    }
}