using System;
using UnityEngine;

public class RobotManager : SerializableObject
{
    public override SerializableType SerializableType => SerializableType.Robot;

    public int rank = 0;

    public bool ShowSim { get; set; }
    public SimAndNormalPart[] parts;

    [Space(10)]
    [SerializeField] private float toleranceForHidingSim = 0.01f;

    private void Start()
    {
        ShowSim = true;
        foreach (SimAndNormalPart simAndNormalPart in parts)
            simAndNormalPart.simPart.transform.position = simAndNormalPart.normalPart.transform.position;
    }

    private void Update()
    {
        foreach (SimAndNormalPart simAndNormalPart in parts)
        {
            if (Vector3.Distance(simAndNormalPart.normalPart.transform.position, simAndNormalPart.simPart.transform.position) < toleranceForHidingSim)
                simAndNormalPart.simPart.SetActive(false);
            else
                simAndNormalPart.simPart.SetActive(ShowSim);
        }
    }

    [Serializable]
    public class SimAndNormalPart
    {
        public GameObject normalPart;
        public GameObject simPart;
    }
}
