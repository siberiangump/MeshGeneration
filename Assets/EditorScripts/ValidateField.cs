using System;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
public class ValidateField : PropertyAttribute
{
    public Type Type;

    public ValidateField(Type type)
    {
        this.Type = type;
    }
}