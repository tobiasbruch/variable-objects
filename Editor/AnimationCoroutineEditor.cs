using UnityEditor;
using UnityEngine;

namespace TobiasBruch.VariableObjects
{
    [CustomPropertyDrawer(typeof(AnimationCoroutine), true)]
    public class AnimationCoroutineEditor : PropertyDrawer
    {
        private float _rectYPosition = 0;
        
        private void DrawProperty(Rect position, SerializedProperty property, string guiContentName = null)
        {
            float height = EditorGUI.GetPropertyHeight(property);
            Rect rect = new Rect(position.x, _rectYPosition, position.width, height);
            _rectYPosition += height;
            if(string.IsNullOrEmpty(guiContentName))
            {
                EditorGUI.PropertyField(rect, property, true);
            }
            else 
            {
                EditorGUI.PropertyField(rect, property, new GUIContent(guiContentName), true);
            }
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 16f;
            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                _rectYPosition = position.y + 18f;
                
                SerializedProperty typeProperty = property.FindPropertyRelative("_type");
                DrawProperty(position, typeProperty);

                AnimationCoroutine.Type mode = (AnimationCoroutine.Type)typeProperty.enumValueIndex;
                switch (mode)
                {
                    case AnimationCoroutine.Type.Animation:
                        DrawProperty(position, property.FindPropertyRelative("_animation"));
                        DrawProperty(position, property.FindPropertyRelative("_animationName"));
                        break;
                    case AnimationCoroutine.Type.Animator:
                        DrawProperty(position, property.FindPropertyRelative("_animator"));
                        DrawProperty(position, property.FindPropertyRelative("_animatorState"));
                        DrawProperty(position, property.FindPropertyRelative("_animatorLayer"));
                        break;
#if DOTWEENPRO
                    case AnimationCoroutine.Type.DOTween:
                        DrawProperty(position, property.FindPropertyRelative("_doTweenAnimation"), "Animation");
                        break;
#endif
                    default:
                        break;
                }
                
                DrawProperty(position, property.FindPropertyRelative("_onStart"));
                DrawProperty(position, property.FindPropertyRelative("_onFinish"));
            }
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = 18f;
            if (property.isExpanded)
            {
                SerializedProperty typeProperty = property.FindPropertyRelative("_type");
                height += EditorGUI.GetPropertyHeight(typeProperty);
                AnimationCoroutine.Type mode = (AnimationCoroutine.Type)typeProperty.enumValueIndex;
                switch (mode)
                {
                    case AnimationCoroutine.Type.Animation:
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_animation"));
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_animationName"));
                        break;
                    case AnimationCoroutine.Type.Animator:
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_animator"));
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_animatorState"));
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_animatorLayer"));
                        break;
#if DOTWEENPRO
                    case AnimationCoroutine.Type.DOTween:
                        height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_doTweenAnimation"));
                        break;
#endif
                    default:
                        break;         
                }
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_onStart"));
                height += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("_onFinish"));
            }
            return height;
        }
    }
}