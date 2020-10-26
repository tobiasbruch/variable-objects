using UnityEditor;
using UnityEngine;
namespace TobiasBruch.VariableObjects
{
    [CustomPropertyDrawer(typeof(BoolReferenceBase), true)]
    public class BoolReferenceEditor : PropertyDrawer
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

                bool valueIsReadOnly = false;
                BoolReference.Mode mode = (BoolReference.Mode)modeProperty.enumValueIndex;
                switch (mode)
                {
                    case BoolReference.Mode.Value:
                        SerializedProperty variableProperty = property.FindPropertyRelative("_variable");
                        height = EditorGUI.GetPropertyHeight(variableProperty);
                        rect = new Rect(position.x, rectYPosition, position.width, height);
                        rectYPosition += height;
                        EditorGUI.PropertyField(rect, variableProperty);

                        valueIsReadOnly = variableProperty.objectReferenceValue != null;
                        break;
                    case BoolReference.Mode.Comparison:
                        valueIsReadOnly = true;

                        SerializedProperty firstOperandVariableProperty = property.FindPropertyRelative("_firstOperandVariable");
                        height = EditorGUI.GetPropertyHeight(firstOperandVariableProperty);
                        rect = new Rect(position.x, rectYPosition, position.width, height);
                        rectYPosition += height;
                        EditorGUI.PropertyField(rect, firstOperandVariableProperty, new GUIContent("Variable"));

                        SerializedProperty comparatorProperty = property.FindPropertyRelative("_comparator");
                        height = EditorGUI.GetPropertyHeight(comparatorProperty);
                        rect = new Rect(position.x, rectYPosition, position.width, height);
                        rectYPosition += height;
                        EditorGUI.PropertyField(rect, comparatorProperty, new GUIContent(" "));

                        SerializedProperty secondOperandTypeProperty = property.FindPropertyRelative("_secondOperandType");
                        height = EditorGUI.GetPropertyHeight(secondOperandTypeProperty);
                        rect = new Rect(position.x, rectYPosition, EditorGUIUtility.labelWidth, height);

                        EditorGUI.PropertyField(rect, secondOperandTypeProperty, GUIContent.none);

                        BoolReference.OperandType operandType = (BoolReference.OperandType)secondOperandTypeProperty.enumValueIndex;
                        int indentLevel = EditorGUI.indentLevel;
                        EditorGUI.indentLevel = 0;
                        switch (operandType)
                        {
                            case BoolReference.OperandType.Variable:
                                SerializedProperty secondOperandVariableProperty = property.FindPropertyRelative("_secondOperandVariable");
                                height = EditorGUI.GetPropertyHeight(secondOperandVariableProperty);
                                rect = new Rect(position.x + EditorGUIUtility.labelWidth, rectYPosition, position.width - EditorGUIUtility.labelWidth, height);
                                rectYPosition += height;
                                EditorGUI.PropertyField(rect, secondOperandVariableProperty, GUIContent.none);

                                break;
                            case BoolReference.OperandType.Float:
                                SerializedProperty secondOperandFloatProperty = property.FindPropertyRelative("_secondOperandFloat");
                                height = EditorGUI.GetPropertyHeight(secondOperandFloatProperty);
                                rect = new Rect(position.x + EditorGUIUtility.labelWidth, rectYPosition, position.width - EditorGUIUtility.labelWidth, height);
                                rectYPosition += height;
                                EditorGUI.PropertyField(rect, secondOperandFloatProperty, GUIContent.none);

                                break;
                            case BoolReference.OperandType.Int:
                                SerializedProperty secondOperandIntProperty = property.FindPropertyRelative("_secondOperandInt");
                                height = EditorGUI.GetPropertyHeight(secondOperandIntProperty);
                                rect = new Rect(position.x + EditorGUIUtility.labelWidth, rectYPosition, position.width - EditorGUIUtility.labelWidth, height);
                                rectYPosition += height;
                                EditorGUI.PropertyField(rect, secondOperandIntProperty, GUIContent.none);

                                break;
                            case BoolReference.OperandType.String:
                                SerializedProperty secondOperandStringProperty = property.FindPropertyRelative("_secondOperandString");
                                height = EditorGUI.GetPropertyHeight(secondOperandStringProperty);
                                rect = new Rect(position.x + EditorGUIUtility.labelWidth, rectYPosition, position.width - EditorGUIUtility.labelWidth, height);
                                rectYPosition += height;
                                EditorGUI.PropertyField(rect, secondOperandStringProperty, GUIContent.none);

                                break;
                        }
                        EditorGUI.indentLevel = indentLevel;
                        break;
                    case BoolReference.Mode.MultipleConditions:
                        valueIsReadOnly = true;

                        SerializedProperty logicOperatorProperty = property.FindPropertyRelative("_logicOperator");
                        height = EditorGUI.GetPropertyHeight(logicOperatorProperty);
                        rect = new Rect(position.x, rectYPosition, position.width, height);
                        rectYPosition += height;
                        EditorGUI.PropertyField(rect, logicOperatorProperty, new GUIContent("Operator"));

                        SerializedProperty logicOperandsProperty = property.FindPropertyRelative("_conditions");
                        if (logicOperandsProperty != null)
                        {
                            if (logicOperandsProperty.isExpanded)
                            {
                                height = EditorGUI.GetPropertyHeight(logicOperandsProperty);
                                rect = new Rect(position.x, rectYPosition, position.width, height);
                                rectYPosition += height;
                                EditorGUI.PropertyField(rect, logicOperandsProperty, true);
                            }
                            else
                            {
                                height = EditorGUI.GetPropertyHeight(logicOperandsProperty);
                                rect = new Rect(position.x, rectYPosition, position.width, height);
                                rectYPosition += height;
                                EditorGUI.PropertyField(rect, logicOperandsProperty);
                            }
                        }
                        else
                        {
                            height = 18f;
                            rect = new Rect(position.x, rectYPosition, position.width, height);
                            rectYPosition += height;
                            EditorGUI.HelpBox(rect, "Maximum depth reached", MessageType.Warning);
                        }
                        break;
                }

                bool invert = false;
                if (valueIsReadOnly)
                {
                    SerializedProperty invertProperty = property.FindPropertyRelative("_invert");
                    height = EditorGUI.GetPropertyHeight(invertProperty);
                    rect = new Rect(position.x, rectYPosition, position.width, height);
                    rectYPosition += height;
                    EditorGUI.PropertyField(rect, invertProperty);
                    invert = invertProperty.boolValue;

                    GUI.enabled = false;
                }
                SerializedProperty valueProperty = property.FindPropertyRelative("_value");
                height = EditorGUI.GetPropertyHeight(valueProperty);
                rect = new Rect(position.x, rectYPosition, position.width, height);
                rectYPosition += height;
                if(valueIsReadOnly)
                {
                    EditorGUI.Toggle(rect, new GUIContent("Value"), invert ? !valueProperty.boolValue : valueProperty.boolValue);
                }
                else
                {
                    valueProperty.boolValue = EditorGUI.Toggle(rect, new GUIContent("Value"), invert ? !valueProperty.boolValue : valueProperty.boolValue);
                }
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
                BoolReference.Mode mode = (BoolReference.Mode)modeProperty.enumValueIndex;
                bool valueIsReadOnly = false;
                switch (mode)
                {
                    case BoolReference.Mode.Value:
                        SerializedProperty variableProperty = property.FindPropertyRelative("_variable");
                        height += EditorGUI.GetPropertyHeight(variableProperty);

                        valueIsReadOnly = variableProperty.objectReferenceValue != null;
                        break;
                    case BoolReference.Mode.Comparison:
                        valueIsReadOnly = true;

                        SerializedProperty firstOperandVariableProperty = property.FindPropertyRelative("_firstOperandVariable");
                        height += EditorGUI.GetPropertyHeight(firstOperandVariableProperty);

                        SerializedProperty comparatorProperty = property.FindPropertyRelative("_comparator");
                        height += EditorGUI.GetPropertyHeight(comparatorProperty);

                        SerializedProperty secondOperandTypeProperty = property.FindPropertyRelative("_secondOperandType");

                        BoolReference.OperandType operandType = (BoolReference.OperandType)secondOperandTypeProperty.enumValueIndex;
                        switch (operandType)
                        {
                            case BoolReference.OperandType.Variable:
                                SerializedProperty secondOperandVariableProperty = property.FindPropertyRelative("_secondOperandVariable");
                                height += EditorGUI.GetPropertyHeight(secondOperandVariableProperty);
                                break;
                            case BoolReference.OperandType.Float:
                                SerializedProperty secondOperandFloatProperty = property.FindPropertyRelative("_secondOperandFloat");
                                height += EditorGUI.GetPropertyHeight(secondOperandFloatProperty);
                                break;
                            case BoolReference.OperandType.Int:
                                SerializedProperty secondOperandIntProperty = property.FindPropertyRelative("_secondOperandInt");
                                height += EditorGUI.GetPropertyHeight(secondOperandIntProperty);
                                break;
                            case BoolReference.OperandType.String:
                                SerializedProperty secondOperandStringProperty = property.FindPropertyRelative("_secondOperandString");
                                height += EditorGUI.GetPropertyHeight(secondOperandStringProperty);
                                break;
                        }
                        break;
                    case BoolReference.Mode.MultipleConditions:
                        valueIsReadOnly = true;

                        SerializedProperty logicOperatorProperty = property.FindPropertyRelative("_logicOperator");
                        height += EditorGUI.GetPropertyHeight(logicOperatorProperty);

                        SerializedProperty logicOperandsProperty = property.FindPropertyRelative("_conditions");
                        if (logicOperandsProperty != null)
                        {
                            height += EditorGUI.GetPropertyHeight(logicOperandsProperty);
                        }
                        else
                        {
                            height += 18f;
                        }
                        break;
                }

                if (valueIsReadOnly)
                {
                    SerializedProperty invertProperty = property.FindPropertyRelative("_invert");
                    height += EditorGUI.GetPropertyHeight(invertProperty);
                }

                SerializedProperty valueProperty = property.FindPropertyRelative("_value");
                height += EditorGUI.GetPropertyHeight(valueProperty);
            }
            return height;
        }
    }
}