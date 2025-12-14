using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreasureDataSO", menuName = "Treasure/TreasureDataSO")]
public class TreasureDataSO : ScriptableObject
{
    public string treasureName;
    public Sprite sprite;
    public int ID;
    public int cost;

    [TextArea]
    public string treasureDescriptionEasy;

    [TextArea]
    public string treasureDescription;
}