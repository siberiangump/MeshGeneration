using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "GridVertexCalculator", menuName = "ScriptableObject/Calculator/GridVertexCalculator", order = 1000)]
public class GridVertexCalculator : ScriptableObject, IVertexCalculator
{
    Vector3[] IVertexCalculator.GetVerteces(MeshSettingsModel meshSettings)
    {
        float step = meshSettings.UnitsSize / meshSettings.VertexSize;
        float startPoint = (-meshSettings.UnitsSize * .5f);
        int rowPointsAmount = meshSettings.VertexSize + 1;
        Vector3[] points = new Vector3[rowPointsAmount * rowPointsAmount];
        for (int index = 0; index < points.Length; index++)
        {
            int i = index / rowPointsAmount;
            int j = index % rowPointsAmount;
            float x = startPoint + step * i;
            float y = startPoint + step * j;
            points[index] = new Vector3(x, y, 0);
        }
        return points;
    }
}
