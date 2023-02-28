using System;

namespace Assets.Project.Scripts.SavingSystem.SaveStructs
{
    [Serializable]
    public struct GameCoreStruct
    {
        public int _levelIndex;
        public LevelStruct[] _levels;
    }
}