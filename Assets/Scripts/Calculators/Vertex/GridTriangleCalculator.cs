using UnityEngine;

[CreateAssetMenu(fileName = "GridTriangleCalculator", menuName = "ScriptableObject/Calculator/GridTriangleCalculator", order = 1000)]
class GridTriangleCalculator: ScriptableObject, ITriangleCalculator
{
    public int[] CreateTriangles(MeshSettingsModel meshSettings)
    {
        int squareAmount = meshSettings.VertexSize;
        int colums = meshSettings.VertexSize + 1;
        int[] triangles = new int[squareAmount * squareAmount * 6];

        for (int index = 0; index < squareAmount * squareAmount; index++)
        {
            int i = index / squareAmount;
            int j = index % squareAmount;
            CreateSquare(i * colums + j, colums, index, index * 6);
        }

        return triangles;

        void CreateSquare(int vertexBaseIndex, int size, int row, int trianglesBaseIndex)
        {
            triangles[trianglesBaseIndex] = vertexBaseIndex;
            triangles[trianglesBaseIndex + 1] = vertexBaseIndex + 1;
            triangles[trianglesBaseIndex + 2] = vertexBaseIndex + size;

            triangles[trianglesBaseIndex + 3] = vertexBaseIndex + 1;
            triangles[trianglesBaseIndex + 4] = vertexBaseIndex + 1 + size;
            triangles[trianglesBaseIndex + 5] = vertexBaseIndex + size;
        }
    }
}
