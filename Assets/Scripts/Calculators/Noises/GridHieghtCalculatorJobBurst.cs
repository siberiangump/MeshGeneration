
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "GridHieghtCalculatorJobBurst", menuName = "ScriptableObject/Calculator/GridHieghtCalculatorJobBurst", order = 1000)]
class GridHieghtCalculatorJobBurst : ScriptableObject, IHieghtCalculator
{
    [SerializeField] float Scale = 1;
    [SerializeField] int BatchCount = 1;

    System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();

    public Vector3[] GetVerteces(Vector3[] vertex, Vector3 basePosition)
    {
        NativeArray<float3> vectors = CreateNativeArrayFrom(vertex);
        NativeArray<float3> jobResult = new NativeArray<float3>(vectors.Length, Allocator.TempJob);

        CalculatorJob jobData = new CalculatorJob
        { Vertices = vectors, BasePosition = basePosition, Scale = Scale, Result = jobResult };
        JobHandle handle = jobData.Schedule(jobResult.Length, BatchCount);
        handle.Complete();

        Vector3[] result = CreateArrayFrom(jobResult);
        vectors.Dispose();
        jobResult.Dispose();

        return result;
    }

    private NativeArray<float3> CreateNativeArrayFrom(Vector3[] vectors)
    {
        NativeArray<float3> result = new NativeArray<float3>(vectors.Length, Allocator.TempJob);
        for (int i = 0; i < vectors.Length; i++)
            result[i] = vectors[i];
        return result;
    }

    private Vector3[] CreateArrayFrom(NativeArray<float3> vectors)
    {
        Vector3[] result = new Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
            result[i] = vectors[i];
        return result;
    }

    [BurstCompile]
    public struct CalculatorJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<float3> Vertices;
        public float3 BasePosition;
        public float Scale;
        public NativeArray<float3> Result;

        public void Execute(int index)
        {
            float2 point2d;
            point2d.x = (BasePosition.x + Vertices[index].x) * Scale;
            point2d.y = (BasePosition.y + Vertices[index].y) * Scale;
            Result[index] = new Vector3(Vertices[index].x, Vertices[index].y, noise.cnoise(point2d));
        }
    }
}
