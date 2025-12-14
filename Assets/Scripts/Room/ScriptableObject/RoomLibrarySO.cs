using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomLibrarySO", menuName = "Room/RoomLibrary")]
public class RoomLibrarySO : ScriptableObject
{
    public List<SmallLevelList> smallLevelList;//一小关
}

[System.Serializable]//必须序列化,不然看不到
public struct SmallLevelList
{
    public List<RoomCardList> roomCardList;//每一小关的卡牌列表
}

[System.Serializable]
public struct RoomCardList
{
    public RoomType roomType;//每一小关列表里的关卡类型
    public float weight;//出现的权重,如果是0就是必出现,最大1
}