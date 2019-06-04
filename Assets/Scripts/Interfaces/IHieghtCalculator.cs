using UnityEngine;
using UnityEditor;

public interface IHieghtCalculator
{
    Vector3[] GetVerteces(Vector3[] vertex, Vector3 basePosition);
}