using UnityEditor;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    [CustomPropertyDrawer(typeof(StringReference), true)]
    public class StringReferenceEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 16f;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                float rectYPosition = position.y + 18f;

                SerializedProperty modeProperty = property.FindPropertyRelative("_mode");
                float height = EditorGUI.GetPropertyHeight(modeProperty);
                Rect rect = new Rect(position.x, rectYPosition, position.width, height);
                rectYPosition += height;
                EditorGUI.PropertyField(rect, modeProperty);
                StringReference.Mode mode = (StringReference.Mode)modeProperty.enumValueIndex;
                bool valueIsReadOnly = false;
                switch(mode)
                {
                    case StringReference.Mode.Format:
                        valueIsReadOnly = true;

                        SerializedProperty format = property.FindPropertyRelative("_format");
                        height = EditorGUI.GetPropertyHeight(format);
                        rect = new Rect(position.x, rectYPosition, position.width, height);
                        rectYPosition += height;
                        EditorGUI.PropertyField(rect, format, GUIContent.none);

                        SerializedProperty formatArguments = property.FindPropertyRelative("_formatArguments");
                        if (formatArguments != null)
                        {
                            height = EditorGUI.GetPropertyHeight(formatArguments);
                            rect = new Rect(position.x, rectYPosition, position.width, height);
                            rectYPosition += height;

                            EditorGUI.PropertyField(rect, formatArguments, new GUIContent("Arguments"), formatArguments.isExpanded);
                        }
                        break;
                    default:
                        SerializedProperty variable = property.FindPropertyRelative("_variable");
                        height = EditorGUI.GetPropertyHeight(variable);
                        rect = new Rect(position.x, rectYPosition, position.width, height);
                        rectYPosition += height;
                        EditorGUI.PropertyField(rect, variable);
                        valueIsReadOnly = variable.objectReferenceValue != null;
                        break;
                }

                SerializedProperty value = property.FindPropertyRelative("_value");
                height = EditorGUI.GetPropertyHeight(value);
                rect = new Rect(position.x, rectYPosition, position.width, height);
                rectYPosition += height;
                if (valueIsReadOnly)
                {
                    GUI.enabled = false;
                }
                EditorGUI.PropertyField(rect, value);
                GUI.enabled = true;

                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 18f;
            if (property.isExpanded)
            {
                SerializedProperty modeProperty = property.FindPropertyRelative("_mode");
                height += EditorGUI.GetPropertyHeight(modeProperty);
                StringReference.Mode mode = (StringReference.Mode)modeProperty.enumValueIndex;
                switch(mode)
                {
                    case StringReference.Mode.Format:
                        SerializedProperty format = property.FindPropertyRelative("_format");
                        height += EditorGUI.GetPropertyHeight(format);

                        SerializedProperty formatArguments = property.FindPropertyRelative("_formatArguments");
                        height += EditorGUI.GetPropertyHeight(formatArguments);

                        break;
                    default:
                        SerializedProperty variable = property.FindPropertyRelative("_variable");
                        height += EditorGUI.GetPropertyHeight(variable);
                        break;
                }
                SerializedProperty value = property.FindPropertyRelative("_value");
                height += EditorGUI.GetPropertyHeight(value);
            }
            return height;
        }
    }
}
