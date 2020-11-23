using System;
using UnityEngine;

public abstract class SerializableObject : MonoBehaviour
{
    public abstract SerializableType SerializableType { get; }
}

[Serializable]
public class SerializedObjects
{
    public SerializedObject[] serializedObjects;

    public SerializedObjects() { }

    public SerializedObjects(SerializableObject[] serializableObjects)
    {
        serializedObjects = new SerializedObject[serializableObjects.Length];
        for (int i = 0; i < serializableObjects.Length; i++)
            serializedObjects[i] = new SerializedObject(serializableObjects[i]);
    }
}

[Serializable]
public class SerializedObject
{
    public SerializableType type;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public SerializedObject() { }

    public SerializedObject(SerializableObject serializableObject)
    {
        type = serializableObject.SerializableType;
        position = serializableObject.transform.position;
        rotation = serializableObject.transform.eulerAngles;
        scale = serializableObject.transform.localScale;
        //position = new SerializableVec3(serializableObject.transform.position);
        //rotation = new SerializableVec3(serializableObject.transform.eulerAngles);
        //scale = new SerializableVec3(serializableObject.transform.lossyScale);
    }
}

//[Serializable]
//public struct SerializableVec3
//{
//    public float x;
//    public float y;
//    public float z;

//    public SerializableVec3(Vector3 vector3)
//    {
//        x = vector3.x;
//        y = vector3.y;
//        z = vector3.z;
//    }
//}

[Serializable]
public enum SerializableType
{
    Robot,
    Item,
    Treadmill,
    Proxy,
    Laser,
}