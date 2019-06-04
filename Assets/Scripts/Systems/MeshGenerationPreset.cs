using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "MeshGenerationPreset", menuName = "ScriptableObject/MeshGenerationPreset", order = 1000)]
public class MeshGenerationPreset : ScriptableObject
{
    [SerializeField, ValidateField(typeof(IVertexHieghtCalculator))] ScriptableObject VertexCalculator; 
}