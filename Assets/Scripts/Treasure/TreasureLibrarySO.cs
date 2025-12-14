using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreasureLibrarySO", menuName = "Treasure/TreasureLibrary")]
public class TreasureLibrarySO : ScriptableObject
{
    public List<TreasureDataSO> treasureDataList;
}