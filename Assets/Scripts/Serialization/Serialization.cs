using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Serialization : MonoBehaviour
{
    public static Serialization Instance { get; private set; }

    [SerializeField] private Spawner spawner;

    [SerializeField] private string pathToFile;
    private string PathToFile => Application.dataPath + "/" + pathToFile;

    private Dictionary<SerializableType, Action> funcByType;

    private void Start()
    {
        Instance = this;
        funcByType = new Dictionary<SerializableType, Action>()
        {
            { SerializableType.Item, spawner.MakeItem },
            { SerializableType.Laser, spawner.MakeLaser },
            { SerializableType.Proxy, spawner.MakeProxy },
            { SerializableType.Robot, spawner.MakeRobot },
            { SerializableType.Treadmill, spawner.MakeTreadMill },
        };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            Save();

        if (Input.GetKeyDown(KeyCode.B))
            Load();
    }

    public void Save()
    {
        if (!File.Exists(PathToFile))
            File.Create(PathToFile);

        SerializedObjects serializedObjects = new SerializedObjects(spawner.objectsParent.GetComponentsInChildren<SerializableObject>());
        using (StreamWriter streamWriter = new StreamWriter(PathToFile, false))
            streamWriter.Write(JsonUtility.ToJson(serializedObjects, true));
    }

    public void Load()
    {
        for (int i = spawner.objectsParent.childCount - 1; i >= 0; i--)
            Destroy(spawner.objectsParent.GetChild(i).gameObject);

        if (!File.Exists(PathToFile))
            return;

        SerializedObjects serializedObjects;
        using (StreamReader streamReader = new StreamReader(PathToFile))
            serializedObjects = JsonUtility.FromJson<SerializedObjects>(streamReader.ReadToEnd());

        foreach (SerializedObject serializedObject in serializedObjects.serializedObjects)
        {
            funcByType[serializedObject.type].Invoke();
            spawner.clone.position = serializedObject.position;
            spawner.clone.eulerAngles = serializedObject.rotation;
            spawner.clone.localScale = serializedObject.scale;
            spawner.clone = null;
        }
    }
}
