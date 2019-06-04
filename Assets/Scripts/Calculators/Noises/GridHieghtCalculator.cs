
using System.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "GridHieghtCalculator", menuName = "ScriptableObject/Calculator/GridHieghtCalculator", order = 1000)]
class GridHieghtCalculator : ScriptableObject, IHieghtCalculator
{
    private float Scale = 1;

    public Vector3[] GetVerteces(Vector3[] vertex, Vector3 basePosition)
    {
        Vector3[] result = new Vector3[vertex.Length];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = ModifyVectorZ(vertex[i], basePosition);
        }
        return result;

        Vector3 ModifyVectorZ(Vector3 vector, Vector3 _base)
        {
            Vector2 point2d = (_base + vector) * Scale;
            return new Vector3(vector.x, vector.y, noise.cnoise(point2d));
        }
    }


}
