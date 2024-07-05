/*
 Copyright 2024 absencee_

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the “Software”), to deal in the 
Software without restriction, including without limitation the rights to use, copy, modify, 
merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to 
whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, 
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. 
IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY 
CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, 
TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using com.absence.soundsystem.imported;
using UnityEditor;
using UnityEngine;

namespace com.absence.soundsystem.editor.imported
{
    [CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
    internal class MinMaxSliderPropertyDrawer : PropertyDrawer
    {
        MinMaxSliderAttribute m_minMax => attribute as MinMaxSliderAttribute;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = EditorGUI.PrefixLabel(position, label);

            const float fixedFieldSize = 1f;
            const float fixedSliderSize = 3f;
            Rect[] rects = Helpers.SliceRectHorizontally(position, 3, Helpers.K_SPACING, 0f, fixedFieldSize, fixedSliderSize, fixedFieldSize);
            Rect minValueRect = rects[0];
            Rect sliderRect = rects[1];
            Rect maxValueRect = rects[2];

            var tempMin = 0f;
            var tempMax = 0f;

            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                tempMin = property.vector2Value.x;
                tempMax = property.vector2Value.y;
            }

            else if (property.propertyType == SerializedPropertyType.Vector2Int)
            {
                tempMin = property.vector2IntValue.x;
                tempMax = property.vector2IntValue.y;
            }

            EditorGUI.BeginChangeCheck();

            tempMin = EditorGUI.FloatField(minValueRect, tempMin);

            EditorGUI.MinMaxSlider(sliderRect, ref tempMin, ref tempMax, m_minMax.min, m_minMax.max);

            tempMax = EditorGUI.FloatField(maxValueRect, tempMax);

            if (tempMin < m_minMax.min) tempMin = m_minMax.min;
            else if (tempMin > tempMax) tempMin = tempMax;

            if (tempMax > m_minMax.max) tempMax = m_minMax.max;
            else if (tempMax < tempMin) tempMax = tempMin;

            if (EditorGUI.EndChangeCheck())
            {
                if (property.propertyType == SerializedPropertyType.Vector2)
                {
                    Vector2 result = new Vector2(tempMin, tempMax);
                    property.vector2Value = result;
                }

                else if (property.propertyType == SerializedPropertyType.Vector2Int)
                {
                    Vector2Int result = new Vector2Int(Mathf.FloorToInt(tempMin), Mathf.FloorToInt(tempMax));
                    property.vector2IntValue = result;
                }
            }
        }
    }
}
