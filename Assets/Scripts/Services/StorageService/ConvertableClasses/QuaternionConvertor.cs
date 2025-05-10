using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class QuaternionConvertor
{
    public float XRotation;
    public float YRotation;
    public float ZRotation;
    public float WRotation;

    public QuaternionConvertor(Quaternion quaternion)
    {
        XRotation = quaternion.x;
        YRotation = quaternion.y;
        ZRotation = quaternion.z;
        WRotation = quaternion.w;
    }

    public Quaternion ApplyToQuaterniuon()
    {
        return new Quaternion(XRotation,YRotation,ZRotation,WRotation);
    }
}
