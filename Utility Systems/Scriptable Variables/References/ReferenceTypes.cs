// ----------------------------------------------------------------------------
// ReferenceTypes.cs
// 
// Author: Jonathan Carter (A.K.A. J)
// Date: 25/10/2021
// ----------------------------------------------------------------------------

using System;
using UnityEngine;

namespace DependencyLibrary
{
    [Serializable]
    public class BoolReference : GenericReference<bool>
    {
        public new BoolVariable variable;
        public override bool Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<bool> GetVariable => variable;
    }
    
    [Serializable]
    public class IntReference : GenericReference<int>
    {
        public new IntVariable variable;
        public override int Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<int> GetVariable => variable;
    }
    
    [Serializable]
    public class FloatReference : GenericReference<float>
    {
        public new FloatVariable variable;
        public override float Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<float> GetVariable => variable;
    }
    
    [Serializable]
    public class Vector3Reference : GenericReference<Vector3>
    {
        public new Vector3Variable variable;
        public override Vector3 Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<Vector3> GetVariable => variable;
    }
    
    [Serializable]
    public class DoubleReference : GenericReference<double>
    {
        public new DoubleVariable variable;
        public override double Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<double> GetVariable => variable;
    }
    
    [Serializable]
    public class Vector2Reference : GenericReference<Vector2>
    {
        public new Vector2Variable variable;
        public override Vector2 Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<Vector2> GetVariable => variable;
    }
    
    [Serializable]
    public class Vector4Reference : GenericReference<Vector4>
    {
        public new Vector4Variable variable;
        public override Vector4 Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<Vector4> GetVariable => variable;
    }
    
    [Serializable]
    public class ColorReference : GenericReference<Color>
    {
        public new ColorVariable variable;
        public override Color Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<Color> GetVariable => variable;
    }
    
    [Serializable]
    public class ShortReference : GenericReference<short>
    {
        public new ShortVariable variable;
        public override short Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<short> GetVariable => variable;
    }
    
    [Serializable]
    public class LongReference : GenericReference<long>
    {
        public new LongVariable variable;
        public override long Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<long> GetVariable => variable;
    }
    
    [Serializable]
    public class SpriteReference : GenericReference<Sprite>
    {
        public new SpriteVariable variable;
        public override Sprite Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<Sprite> GetVariable => variable;
    }
    
        
    [Serializable]
    public class StringReference : GenericReference<string>
    {
        public new StringVariable variable;
        public override string Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<string> GetVariable => variable;
    }
    
    
    [Serializable]
    public class QuaternionReference : GenericReference<Quaternion>
    {
        public new QuaternionVariable variable;
        public override Quaternion Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<Quaternion> GetVariable => variable;
    }
    
    
    [Serializable]
    public class GameObjectReference : GenericReference<GameObject>
    {
        public new GameObjectVariable variable;
        public override GameObject Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<GameObject> GetVariable => variable;
    }
    
    [Serializable]
    public class MaterialReference : GenericReference<Material>
    {
        public new MaterialVariable variable;
        public override Material Value => useConstant ? constantValue : variable.Value;
        public override IDependencyLibVariable<Material> GetVariable => variable;
    }
}