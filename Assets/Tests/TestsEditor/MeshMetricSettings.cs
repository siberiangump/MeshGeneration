using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MeshMetricSettings", menuName = "ScriptableObject/Test/MeshMetricSettings", order = 1000)]
public class MeshMetricSettings : ScriptableObject
{
    [SerializeField, ValidateField(typeof(IHieghtCalculator))] ScriptableObject Sync;
    [SerializeField, ValidateField(typeof(IHieghtCalculator))] ScriptableObject Task;
    [SerializeField, ValidateField(typeof(IHieghtCalculator))] ScriptableObject Job;
    [SerializeField, ValidateField(typeof(IHieghtCalculator))] ScriptableObject JobBurst;
    [SerializeField, ValidateField(typeof(IHieghtCalculator))] ScriptableObject Compute;

    [SerializeField, ValidateField(typeof(IVertexCalculator))] ScriptableObject VertexCalculator;

    [SerializeField] MeshSettingsModel MeshSettings;

    public IHieghtCalculator GetSyncCalculator() => Sync as IHieghtCalculator;
    public IHieghtCalculator GetTaskCalculator() => Task as IHieghtCalculator;
    public IHieghtCalculator GetJobCalculator() => Job as IHieghtCalculator;
    public IHieghtCalculator GetJobBurstCalculator() => JobBurst as IHieghtCalculator;

    public IHieghtCalculator GetComputeCalculator() => Compute as IHieghtCalculator;

    public IVertexCalculator GetVertexCalculator() => VertexCalculator as IVertexCalculator;

    public MeshSettingsModel GetMeshSettings() => MeshSettings;
}
