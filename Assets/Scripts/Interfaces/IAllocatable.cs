using UnityEngine;

public interface IAllocatable
{
    void Allocate(Vector3[] verteces, Vector3 basePosition);
    void Dispose();
}
