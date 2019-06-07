using UnityEngine;
using System.Collections;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;

[CreateAssetMenu(fileName = "GridVertexCalculatorJobBurst", menuName = "ScriptableObject/Calculator/GridVertexCalculatorJobBurst", order = 1000)]
public class GridVertexCalculatorJobBurst : ScriptableObject, IVertexCalculator
{
    [SerializeField] int BatchCount = 1;

    Vector3[] IVertexCalculator.GetVerteces(MeshSettingsModel meshSettings)
    {
        float step = meshSettings.UnitsSize / meshSettings.VertexSize;
        float startPoint = (-meshSettings.UnitsSize * .5f);
        int rowPointsAmount = meshSettings.VertexSize + 1;
        int length = rowPointsAmount * rowPointsAmount;

        NativeArray<float3> jobResult = new NativeArray<float3>(length, Allocator.TempJob);

        CalculatorJob jobData = new CalculatorJob
        { Step = step, StartPoint = startPoint, RowPointsAmount = rowPointsAmount, Result = jobResult };
        JobHandle handle = jobData.Schedule(jobResult.Length, BatchCount);
        handle.Complete();

        Vector3[] result = CreateArrayFrom(jobResult);

        jobResult.Dispose();

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
        public float Step;
        public float StartPoint;
        public int RowPointsAmount;
        public NativeArray<float3> Result;

        public void Execute(int index)
        {
            int i = index / RowPointsAmount;
            int j = index % RowPointsAmount;
            float x = StartPoint + Step * i;
            float y = StartPoint + Step * j;
            Result[index] = new Vector3(x, y, 0);
        }
    }
}
