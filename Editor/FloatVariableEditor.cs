using UnityEditor;

namespace TobiasBruch.VariableObjects
{
    [CustomEditor(typeof(FloatVariable), true)]
    [CanEditMultipleObjects]
    public class FloatVariableEditor : VariableEditor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SerializedProperty _clamp = serializedObject.FindProperty("_clamp");
            SerializedProperty _minValue = serializedObject.FindProperty("_minValue");
            SerializedProperty _maxValue = serializedObject.FindProperty("_maxValue");

            serializedObject.Update();
            EditorGUILayout.PropertyField(_clamp);
            if (_clamp.boolValue)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.PropertyField(_minValue);
                EditorGUILayout.PropertyField(_maxValue);
                EditorGUI.indentLevel--;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}