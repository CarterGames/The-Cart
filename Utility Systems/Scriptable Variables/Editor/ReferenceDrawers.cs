// ----------------------------------------------------------------------------
// ReferenceDrawers.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using UnityEditor;

namespace DependencyLibrary.Editor
{
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class BoolReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(DoubleReference))]
    public class DoubleReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(Vector3Reference))]
    public class Vector3ReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(Vector2Reference))]
    public class Vector2ReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(Vector4Reference))]
    public class Vector4ReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(ColorReference))]
    public class ColorReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(ShortReference))]
    public class ShortReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(LongReference))]
    public class LongReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(SpriteReference))]
    public class SpriteReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferenceDrawer : GenericReferenceDrawer
    {}
    
    [CustomPropertyDrawer(typeof(QuaternionReference))]
    public class QuaternionReferenceDrawer : GenericReferenceDrawer
    {}
}