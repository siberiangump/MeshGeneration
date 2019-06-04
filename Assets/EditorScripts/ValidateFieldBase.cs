using UnityEngine;
using System.Collections;
using System;

[Serializable]
public abstract class BaseValidatedField
{
    public abstract Type ObjectType { get; }
    [SerializeField] protected UnityEngine.Object ObjectValue;
}

[Serializable]
public class ValidatedField<T> : BaseValidatedField where T : class
{
    public override Type ObjectType { get { return typeof(T); } }
    public T Value { get { return ObjectValue as T; } }

    public static implicit operator T(ValidatedField<T> target)
    {
        return target.Value as T;
    }
}