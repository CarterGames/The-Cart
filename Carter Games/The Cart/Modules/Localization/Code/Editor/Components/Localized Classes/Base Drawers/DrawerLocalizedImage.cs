﻿#if CARTERGAMES_CART_MODULE_LOCALIZATION && UNITY_EDITOR

/*
 * Copyright (c) 2025 Carter Games
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using CarterGames.Cart.Core.Editor;
using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Modules.Localization.Editor
{
    [CustomPropertyDrawer(typeof(LocalizedImage), true)]
    public class DrawerLocalizedImage : PropertyDrawer
    {
	    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	    {
		    label = EditorGUI.BeginProperty(position, label, property);
		    
		    var pos = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
		    property.isExpanded = EditorGUI.BeginFoldoutHeaderGroup(pos, property.isExpanded, label, new GUIStyle("foldout"));

		    if (property.isExpanded)
		    {
			    EditorGUI.indentLevel++;
			    pos.y += pos.height + 1.5f;
			    DrawElements(pos, property, position);
			    EditorGUI.indentLevel--;
		    }

		    EditorGUI.EndFoldoutHeaderGroup();
		    EditorGUI.EndProperty();

		    property.serializedObject.ApplyModifiedProperties();
		    property.serializedObject.Update();
	    }


	    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }


	    protected virtual void DrawElements(Rect pos, SerializedProperty property, Rect ogPosition)
	    {
		    EditorGUI.PropertyField(pos, property.Fpr("image"));
	    }
    }
}

#endif