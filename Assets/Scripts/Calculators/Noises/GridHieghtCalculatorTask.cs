
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "GridHieghtCalculatorTask", menuName = "ScriptableObject/Calculator/GridHieghtCalculatorTask", order = 1000)]
class GridHieghtCalculatorTask : ScriptableObject, IHieghtCalculator
{
    [SerializeField] float Scale = 1;
    [SerializeField] int Tasks = 3;

    public Vector3[] GetVerteces(Vector3[] vertex, Vector3 basePosition)
    {
        Vector3[] result = new Vector3[vertex.Length];
        int amountInChunk = vertex.Length / Tasks;
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < Tasks; i++)
        {
            int from = amountInChunk *i;
            int to = amountInChunk * (i+1);
            to = to > vertex.Length ? vertex.Length : to;
            tasks.Add(Task.Run(() => CalculateFromToIndex(from, to)));
        }
        Task.WaitAll(tasks.ToArray());
        return result;

        void CalculateFromToIndex(int from, int to)
        {
            for (int i = from; i < to; i++)
            {
                result[i] = ModifyVectorZ(vertex[i], basePosition);
            }
        }

        Vector3 ModifyVectorZ(Vector3 vector, Vector3 _base)
        {
            Vector2 point2d = (_base + vector) * Scale;
            return new Vector3(vector.x, vector.y, noise.cnoise(point2d));
        }
    }
}
