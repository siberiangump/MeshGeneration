
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "GridHieghtCalculatorComputeShader", menuName = "ScriptableObject/Calculator/GridHieghtCalculatorComputeShader", order = 1000)]
class GridHieghtCalculatorComputeShader : ScriptableObject, IHieghtCalculator
{
    [SerializeField] ComputeShader Shader;

    private ComputeBuffer Vertex;
    private ComputeBuffer Result;
    private ComputeBuffer BasePosition;
    private float Scale = 1;

    private int CurrenBufferSize = -1;
    private int treadSize = 64;

    public Vector3[] GetVerteces(Vector3[] vertex, Vector3 basePosition)
    {
        InitBuffers(vertex.Length);
        Vertex.SetData(vertex);
        Shader.SetBuffer(0, "Vertices", Vertex);
        Shader.SetBuffer(0, "Result", Result);
        Shader.SetFloats("BasePosition", basePosition.x, basePosition.y, basePosition.z);
        Shader.SetFloats("Scale", Scale);
        Shader.SetInt("GridSize", treadSize);
        int xWaves = vertex.Length / treadSize + (vertex.Length % treadSize > 0 ? 1 : 0);
        Shader.Dispatch(0, xWaves, 1, 1);
        Vector3[] result = new Vector3[vertex.Length];
        Result.GetData(result, 0, 0, vertex.Length);
        ReleaseBuffers();
        return result;
    }

    private void InitBuffers(int count)
    {
        if (CurrenBufferSize == count && Vertex != null)
            return;
        Vertex = new ComputeBuffer(count, sizeof(float) * 3);
        Result = new ComputeBuffer(count, sizeof(float) * 3);
    }

    private void ReleaseBuffers()
    {
        if (Vertex == null)
            return;
        Vertex.Release();
        Result.Release();
    }
}
