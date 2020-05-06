using UnityEditor;

namespace TobiasBruch.VariableObjects
{
    [CustomEditor(typeof(VariableBase), true)]
    public class VariableEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            SerializedProperty _value = serializedObject.FindProperty("_value");
            SerializedProperty _resetToDefaultValue = serializedObject.FindProperty("_resetToDefaultValue");
            SerializedProperty _defaultValue = serializedObject.FindProperty("_defaultValue");

            serializedObject.Update();
            EditorGUILayout.PropertyField(_value);
            EditorGUILayout.PropertyField(_resetToDefaultValue);

            if (_resetToDefaultValue.boolValue)
            {
                EditorGUILayout.PropertyField(_defaultValue);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}