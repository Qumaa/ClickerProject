using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Meta
{
    public static class Extensions
    {
        /// <summary>
        /// Transform a 2D (x, y) vector into a 3D (x, 0, y) vector
        /// </summary>
        public static Vector3 XYtoXoY(this Vector2 vec) => new(vec.x, 0, vec.y);
        /// <summary>
        /// Transform a 2D (x, y) vector into a 3D (x, 0, y) vector
        /// </summary>
        public static Vector3 XYtoXoY(this Vector2Int vec) => vec.ToFloatVec2().XYtoXoY();
        public static Vector2 ToFloatVec2(this Vector2Int vec) => new(vec.x, vec.y);
        public static Vector2Int ToIntVec2(this Vector2 vec) => Vector2Int.FloorToInt(vec);
        /// <summary>
        /// Transform a 3D (x, y, z) vector into a 2D (x, z) vector
        /// </summary>
        public static Vector2 XYZtoXZ(this Vector3 vec) => new(vec.x, vec.z);
        public static Vector2Int XYZtoXZint(this Vector3 vec) => Vector2Int.FloorToInt(vec.XYZtoXZ());
    }
}