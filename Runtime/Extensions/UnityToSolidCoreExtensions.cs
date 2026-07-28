namespace SolidCore.UnityIntegration.Runtime.Extensions
{
    public static class UnityToSolidExtensions
    {
        public static SolidCore.Math.Vector3 SC(this UnityEngine.Vector3 v)
            => new SolidCore.Math.Vector3(v.x, v.y, v.z);

        public static SolidCore.Math.Vector2 SC(this UnityEngine.Vector2 v)
            => new SolidCore.Math.Vector2(v.x, v.y);

        public static SolidCore.Math.Quaternion SC(this UnityEngine.Quaternion q)
            => new SolidCore.Math.Quaternion(q.x, q.y, q.z, q.w);
    
        public static SolidCore.Math.Matrix4x4 SC(this UnityEngine.Matrix4x4 m)
            => new SolidCore.Math.Matrix4x4(
                new SolidCore.Math.Vector4(m.m00, m.m01, m.m02, m.m03),
                new SolidCore.Math.Vector4(m.m10, m.m11, m.m12, m.m13),
                new SolidCore.Math.Vector4(m.m20, m.m21, m.m22, m.m23),
                new SolidCore.Math.Vector4(m.m30, m.m31, m.m32, m.m33)
            );
    
        public static SolidCore.Math.Vector4 SC(this UnityEngine.Vector4 v) => new(v.x, v.y, v.z, v.w);
    
        public static SolidCore.Math.Color SC(this UnityEngine.Color c) => new(c.r, c.g, c.b, c.a);
    }
}
