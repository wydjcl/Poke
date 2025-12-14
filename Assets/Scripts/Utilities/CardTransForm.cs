using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardTransForm
{
    public Vector3 pos;
    public Quaternion rotation;

    public CardTransForm(Vector3 position, Quaternion quaternion)
    {
        pos = position;
        rotation = quaternion;
    }
}