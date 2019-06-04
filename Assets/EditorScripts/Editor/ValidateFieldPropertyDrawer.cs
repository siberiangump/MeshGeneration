using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ValidateField))]
public class ValidateFieldPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ValidateField attribute = this.attribute as ValidateField;
        SingleCheck(property, attribute);
        EditorGUI.PropertyField(position, property, label, true);
    }

    private void SingleCheck(SerializedProperty property, ValidateField attribute)
    {
        Object interationTarget = property.objectReferenceValue;
        if (!CheckElement(interationTarget, attribute))
        {
            property.objectReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
            Debug.LogError("Incorrect component type in field :" + property.displayName);
        }
    }
    
    private bool CheckElement(object target, ValidateField attribute)
    {
        if (target == null)
            return true;

        System.Type targetType = target.GetType();
        return attribute.Type.IsAssignableFrom(targetType);
    }
}