using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(BaseValidatedField), true)]
public class BaseValidatedFieldPropertyDrawed : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty p = property.FindPropertyRelative("ObjectValue");
        BaseValidatedField b = fieldInfo.GetValue(property.serializedObject.targetObject) as BaseValidatedField;
        SingleCheck(p, b.ObjectType);
        EditorGUI.PropertyField(position, p, label, true);
    }

    private void SingleCheck(SerializedProperty property, Type attribute)
    {
        UnityEngine.Object interationTarget = property.objectReferenceValue;
        if (!CheckElement(interationTarget, attribute))
        {
            property.objectReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
            Debug.LogError("Incorrect component type in field :" + property.displayName);
        }
    }

    private bool CheckElement(object target, Type type)
    {
        if (target == null)
            return true;

        System.Type targetType = target.GetType();
        return type.IsAssignableFrom(targetType);
    }
}