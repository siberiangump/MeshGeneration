
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "GridHieghtCalculatorJob", menuName = "ScriptableObject/Calculator/GridHieghtCalculatorJob", order = 1000)]
class GridHieghtCalculatorJob : ScriptableObject, IHieghtCalculator
{
    [SerializeField] float Scale = 1;
    [SerializeField] int BatchCount = 1;

    public Vector3[] GetVerteces(Vector3[] vertex, Vector3 basePosition)
    {
        NativeArray<Vector3> vectors = CreateNativeArrayFrom(vertex);
        NativeArray<Vector3> jobResult = new NativeArray<Vector3>(vectors.Length, Allocator.TempJob);

        CalculatorJob jobData = new CalculatorJob
            { Vertices = vectors, BasePosition = basePosition, Scale = Scale, Result = jobResult };
        JobHandle handle = jobData.Schedule(jobResult.Length, BatchCount);
        handle.Complete();

        Vector3[] result = jobResult.ToArray();
        vectors.Dispose();
        jobResult.Dispose();

        return result;
    }

    private NativeArray<Vector3> CreateNativeArrayFrom(Vector3[] vectors)
    {
        NativeArray<Vector3> result = new NativeArray<Vector3>(vectors.Length, Allocator.TempJob);
        for (int i = 0; i < vectors.Length; i++)
            result[i] = vectors[i];
        return result;
    }

    public struct CalculatorJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<Vector3> Vertices;
        public Vector3 BasePosition;
        public float Scale;
        public NativeArray<Vector3> Result;

        public void Execute(int index)
        {
            Vector2 point2d = (BasePosition + Vertices[index]) * Scale;
            Result[index] = new Vector3(Vertices[index].x, Vertices[index].y, noise.cnoise(point2d));
        }
    }
}
