using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MeshTest : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField, ValidateField(typeof(IVertexCalculator))] ScriptableObject VertexCalculator;
    [SerializeField, ValidateField(typeof(ITriangleCalculator))] ScriptableObject TriangleCalculator;
    [SerializeField, ValidateField(typeof(IHieghtCalculator))] ScriptableObject HieghtCalculator;

    [Header("Scene")]
    [SerializeField] MeshFilter MeshFilter;

    [Header("Settings")]
    [SerializeField] MeshSettingsModel MeshSettings;
    [SerializeField] bool RecalculateOnUpdate = true;

    private Vector3 OldPosition;
    System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();

    private void OnValidate()
    {
        if (MeshSettings.VertexSize <= 0 || MeshSettings.VertexSize >= 50)
            return;
        CreateGrid();
    }

    public void Update()
    {
        if (!RecalculateOnUpdate)
            return;
        if (OldPosition == this.transform.position)
            return;
        OldPosition = this.transform.position;
        RecalculateVertexHight();
    }

    [ContextMenu("CreateGrid")]
    public void CreateGrid()
    {
        CreateAndApplyMeshToFilter(
            verteces: (VertexCalculator as IVertexCalculator).GetVerteces(MeshSettings),
            triangles: (TriangleCalculator as ITriangleCalculator).CreateTriangles(MeshSettings));
    }

    [ContextMenu("RecalculateVertexHight")]
    public void RecalculateVertexHight()
    {
        Mesh oldMesh = MeshFilter.sharedMesh;
        IVertexHieghtCalculator vertexCalculator = VertexCalculator as IVertexHieghtCalculator;

        Stopwatch.Restart();
        Vector3[] newVertices = (HieghtCalculator as IHieghtCalculator).GetVerteces(oldMesh.vertices, this.transform.position);
        Stopwatch.Stop();
        Debug.Log("recalculate time" + Stopwatch.ElapsedMilliseconds);

        CreateAndApplyMeshToFilter(
            verteces: newVertices,
            triangles: oldMesh.triangles);
    }

    private void CreateAndApplyMeshToFilter(Vector3[] verteces, int[] triangles)
    {
        Mesh mesh = new Mesh();
        mesh.vertices = verteces;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        MeshFilter.mesh = mesh;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Mesh mesh = MeshFilter.sharedMesh;
        float step = (MeshSettings.UnitsSize / MeshSettings.VertexSize) * .1f;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(this.transform.position + mesh.vertices[i], step);
        }
    }

}
