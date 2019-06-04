using UnityEngine;
using UnityEditor;

public interface IVertexCalculator
{
    Vector3[] GetVerteces(MeshSettingsModel meshSettings);
}