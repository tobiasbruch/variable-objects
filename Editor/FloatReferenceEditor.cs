﻿using UnityEditor;
using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [CustomPropertyDrawer(typeof(FloatReferenceBase), true)]
    public class FloatReferenceEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 16f;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                float rectYPosition = position.y + 18f;

                SerializedProperty variable = property.FindPropertyRelative("_variable");

                float height = EditorGUI.GetPropertyHeight(variable);
                Rect rect = new Rect(position.x, rectYPosition, position.width, height);
                rectYPosition += height;
                EditorGUI.PropertyField(rect, variable);

                SerializedProperty value = property.FindPropertyRelative("_value");
                height = EditorGUI.GetPropertyHeight(value);
                rect = new Rect(position.x, rectYPosition, position.width, height);
                rectYPosition += height;
                if (variable.objectReferenceValue != null)
                {
                    GUI.enabled = false;
                    EditorGUI.PropertyField(rect, value);
                    GUI.enabled = true;
                }
                else
                {
                    EditorGUI.PropertyField(rect, value);
                }

                SerializedProperty operation = property.FindPropertyRelative("_operation");
                if (operation != null)
                {
                    height = EditorGUI.GetPropertyHeight(operation);
                    rect = new Rect(position.x, rectYPosition, position.width, height);
                    rectYPosition += height;
                    EditorGUI.PropertyField(rect, operation);

                    if (operation.enumValueIndex > 0)
                    {
                        SerializedProperty operand = property.FindPropertyRelative("_operand");

                        if (operand != null)
                        {
                            height = EditorGUI.GetPropertyHeight(operand);
                            rect = new Rect(position.x, rectYPosition, position.width, height);
                            rectYPosition += height;
                            EditorGUI.PropertyField(rect, operand);
                        }
                    }
                }

                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 18f;
            if (property.isExpanded)
            {
                SerializedProperty variable = property.FindPropertyRelative("_variable");
                height += EditorGUI.GetPropertyHeight(variable);
                SerializedProperty value = property.FindPropertyRelative("_value");
                height += EditorGUI.GetPropertyHeight(value);
                SerializedProperty operation = property.FindPropertyRelative("_operation");
                if (operation != null)
                {
                    height += EditorGUI.GetPropertyHeight(value);
                    if (operation.enumValueIndex > 0)
                    {
                        SerializedProperty operand = property.FindPropertyRelative("_operand");
                        if (operand != null)
                        {
                            height += EditorGUI.GetPropertyHeight(operand);
                        }
                    }
                }
            }

            return height;
        }
    }
}