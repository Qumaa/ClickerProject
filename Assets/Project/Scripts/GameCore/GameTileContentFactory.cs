﻿using UnityEngine;

namespace Assets.Project.Scripts.GameCore
{
    [CreateAssetMenu]
    public class GameTileContentFactory : GameObjectFactory
    {
        [SerializeField] private GameTileContent _destinationPrefab;
        [SerializeField] private GameTileContent _emptyPrefab;
        [SerializeField] private GameTileContent _wallPrefab;
        [SerializeField] private GameTileContent _spawnPrefab;

        public void Reclain(GameTileContent content)
        {
            Destroy(content);
        }

        public GameTileContent Get(GameTileContentType type)
        {
            switch (type)
            {
                case GameTileContentType.Destination:
                    return Get(_destinationPrefab);
                case GameTileContentType.Empty: 
                    return Get(_emptyPrefab);
                case GameTileContentType.Wall:
                    return Get(_wallPrefab);
                case GameTileContentType.SpawnPoint:
                    return Get(_spawnPrefab);
            }

            return null;
        }

        private GameTileContent Get(GameTileContent prefab)
        {
            GameTileContent instance = CreateGameObjectInstance(prefab);
            instance.OriginFactory = this;
            return instance;
        }
    }
}