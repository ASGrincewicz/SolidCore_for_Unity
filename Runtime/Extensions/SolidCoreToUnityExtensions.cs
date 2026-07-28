
namespace SolidCore.UnityIntegration.Runtime.Extensions
{
    public static class SolidCoreToUnityExtensions
    {
        public static UnityEngine.Vector3 ToUnity(this SolidCore.Math.Vector3 v) => new(v.X, v.Y, v.Z);

        public static UnityEngine.Vector2 ToUnity(this SolidCore.Math.Vector2 v)
            => new UnityEngine.Vector2(v.X, v.Y);

        public static UnityEngine.Quaternion ToUnity(this SolidCore.Math.Quaternion q)
            => new UnityEngine.Quaternion(q.X, q.Y, q.Z, q.W);
        
        public static UnityEngine.Matrix4x4 ToUnity(this SolidCore.Math.Matrix4x4 m)
            => new UnityEngine.Matrix4x4(
                new UnityEngine.Vector4(m.M11, m.M12, m.M13, m.M14),
                new UnityEngine.Vector4(m.M21, m.M22, m.M23, m.M24),
                new UnityEngine.Vector4(m.M31, m.M32, m.M33, m.M34),
                new UnityEngine.Vector4(m.M41, m.M42, m.M43, m.M44)
            );
        
        public static UnityEngine.Vector4 ToUnity(this SolidCore.Math.Vector4 v)
            => new UnityEngine.Vector4(v.X, v.Y, v.Z, v.W);
        
        public static UnityEngine.Color ToUnity(this SolidCore.Math.Color c)
            => new UnityEngine.Color(c.R, c.G, c.B, c.A);
    }
}
