using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Meta
{
    public static class AssetMenuPaths
    {
        public const string Root = "Project";
        public static class Data
        {
            public const string Root = AssetMenuPaths.Root + "/Data";

            public static class Level
            {
                public const string Root = Data.Root + "/Level";

                public const string LoadData = Root + "/Load data";
                public const string MetaData = Root + "/Meta data";
            }
        }
    }
}