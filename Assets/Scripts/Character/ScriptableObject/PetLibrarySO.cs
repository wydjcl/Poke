using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetLibrarySO", menuName = "Pet/PetLibrary")]
public class PetLibrarySO : ScriptableObject
{
    //¶¯Ì¬
    public PetDataSO petData;

    public int level;
    public float ex;
    public EquipmentType equipment;

    [TextArea]
    public string description;
}