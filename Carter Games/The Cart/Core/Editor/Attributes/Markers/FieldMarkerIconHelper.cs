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

using UnityEditor;
using UnityEngine;

namespace CarterGames.Cart.Core.Editor
{
	public static class FieldMarkerIconHelper
	{
		public static Texture GetIcon(FieldMarkerIcon icon)
		{
			return icon switch
			{
				FieldMarkerIcon.DiamondMagenta => EditorGUIUtility.IconContent("sv_icon_dot15_pix16_gizmo").image,
				FieldMarkerIcon.DiamondRed => EditorGUIUtility.IconContent("sv_icon_dot14_pix16_gizmo").image,
				FieldMarkerIcon.DiamondOrange => EditorGUIUtility.IconContent("sv_icon_dot13_pix16_gizmo").image,
				FieldMarkerIcon.DiamondYellow => EditorGUIUtility.IconContent("sv_icon_dot12_pix16_gizmo").image,
				FieldMarkerIcon.DiamondGreen => EditorGUIUtility.IconContent("sv_icon_dot11_pix16_gizmo").image,
				FieldMarkerIcon.DiamondTeal => EditorGUIUtility.IconContent("sv_icon_dot10_pix16_gizmo").image,
				FieldMarkerIcon.DiamondBlue => EditorGUIUtility.IconContent("sv_icon_dot9_pix16_gizmo").image,
				FieldMarkerIcon.DiamondWhite => EditorGUIUtility.IconContent("sv_icon_dot8_pix16_gizmo").image,
				FieldMarkerIcon.CircleMagenta => EditorGUIUtility.IconContent("sv_icon_dot7_pix16_gizmo").image,
				FieldMarkerIcon.CircleRed => EditorGUIUtility.IconContent("sv_icon_dot6_pix16_gizmo").image,
				FieldMarkerIcon.CircleOrange => EditorGUIUtility.IconContent("sv_icon_dot5_pix16_gizmo").image,
				FieldMarkerIcon.CircleYellow => EditorGUIUtility.IconContent("sv_icon_dot4_pix16_gizmo").image,
				FieldMarkerIcon.CircleGreen => EditorGUIUtility.IconContent("sv_icon_dot3_pix16_gizmo").image,
				FieldMarkerIcon.CircleTeal => EditorGUIUtility.IconContent("sv_icon_dot2_pix16_gizmo").image,
				FieldMarkerIcon.CircleBlue => EditorGUIUtility.IconContent("sv_icon_dot1_pix16_gizmo").image,
				FieldMarkerIcon.CircleWhite => EditorGUIUtility.IconContent("sv_icon_dot0_pix16_gizmo").image,
				FieldMarkerIcon.Unassigned => EditorGUIUtility.IconContent("sv_icon_dot0_pix16_gizmo").image,
				_ => EditorGUIUtility.IconContent("sv_icon_dot0_pix16_gizmo").image
			};
		}
	}
}