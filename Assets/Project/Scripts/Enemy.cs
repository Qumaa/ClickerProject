using Assets.Project.Scripts.GameCore;
using UnityEngine;

namespace Assets.Project.Scripts
{
    public class Enemy : MonoBehaviour
    {
        public EnemyFactory OriginFactory { get; set; }

        private GameTile _tileFrom, _tileTo;
        private Vector3 _positionFrom, _positionTo;
        private float _progress;

        public void SpawnOn(GameTile tile)
        {
            transform.position = tile.transform.position;
            _tileFrom = tile;
            _tileTo = tile.NextTileOnPath;
            _positionFrom = _tileFrom.transform.position;
            _positionTo = _tileTo.ExitPoint;
            _progress = 0f;
        }

        public bool GameUpdate()
        {
            _progress += Time.deltaTime;
            while (_progress >= 1)
            {
                _tileFrom = _tileTo;
                _tileTo = _tileTo.NextTileOnPath;
                if (_tileTo == null)
                {
                    OriginFactory.Reclaim(this);
                    return false;
                }
                _positionFrom = _positionTo;
                _positionTo = _tileTo.ExitPoint;
                _progress -= 1f;
            }

            transform.position = Vector3.LerpUnclamped(_positionFrom, _positionTo, _progress);
            return true;
        }
    }
}