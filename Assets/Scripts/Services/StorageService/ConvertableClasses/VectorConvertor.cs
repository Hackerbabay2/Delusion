using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VectorConvertor
{
    public float X;
    public float Y;
    public float Z;

    public VectorConvertor(Vector3 vector)
    {
        X = vector.x; 
        Y = vector.y; 
        Z = vector.z;
    }

    public Vector3 ApplyToVector3()
    {
        return new Vector3(X, Y, Z);
    }
}
