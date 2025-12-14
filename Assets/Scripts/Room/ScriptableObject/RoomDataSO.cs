using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomDataSO", menuName = "Room/RoomData")]
public class RoomDataSO : ScriptableObject
{
    public Sprite roomIcon;
    public RoomType roomType;
}