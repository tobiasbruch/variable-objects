using UnityEditor;
namespace TobiasBruch.VariableObjects
{
    [CustomEditor(typeof(VariableBase), true)]
    public class VariableEditor : Editor
    {
        SerializedProperty _value;
        SerializedProperty _resetToDefaultValue;
        SerializedProperty _defaultValue;
        protected virtual void OnEnable()
        {
            SerializedObject so = new SerializedObject(serializedObject.targetObject);
            _value = so.FindProperty("_value");
            _resetToDefaultValue = so.FindProperty("_resetToDefaultValue");
            _defaultValue = so.FindProperty("_defaultValue");
        }

        public override void OnInspectorGUI()
        {
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