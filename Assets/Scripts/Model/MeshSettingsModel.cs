using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct MeshSettingsModel
{
    public float UnitsSize;
    [Range(0,255)]
    public int VertexSize;
}