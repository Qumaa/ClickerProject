using System.Collections;
using UnityEngine;

namespace Assets.Project.Scripts.GameCore
{
    public static class DirectionExtensions
    {
        private static Quaternion[] _rotations =
        {
            Quaternion.identity,
            Quaternion.Euler(0f, 90f, 0f),
            Quaternion.Euler(0f, 180f, 0f),
            Quaternion.Euler(0f, 2700f, 0f)
        };
    }
}