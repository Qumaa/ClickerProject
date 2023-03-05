using UnityEngine;

namespace Assets.Project.Scripts.GameCore
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Vector2Int _boardSize;
        [SerializeField] private GameBoard _board;
        [SerializeField] private Camera _camera;

        [Header("Factories")]
        [SerializeField] private GameTileContentFactory _contentfactory;
        [SerializeField] private EnemyFactory _enemyFactory;

        [SerializeField, Range(0.1f, 10f)] private float _spawnSpeed;

        private float _spawnProgress;

        private EnemyCollection _enemies = new EnemyCollection();

        private Ray _touchRay => _camera.ScreenPointToRay(Input.mousePosition);

        private void Start()
        {
            _board.Initialize(_boardSize, _contentfactory);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                HandleAlternativeTouch();
            }
            IncreaseSpawnProgress();
        }

        private void HandleTouch()
        {
            GameTile tile = _board.GetTile(_touchRay);
            if (tile != null)
            {
                _board.ToggleDestination(tile);
            }
        }
        
        private void HandleAlternativeTouch()
        {
            GameTile tile = _board.GetTile(_touchRay);
            if (tile != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _board.ToggleWall(tile);
                }
                else
                {
                    _board.ToggleSpawnPoint(tile);
                }               
            }
        }

        private void IncreaseSpawnProgress()
        {
            _spawnProgress += _spawnSpeed * Time.deltaTime;
            while (_spawnProgress >= 1f)
            {
                _spawnProgress -= 1f;
                SpawnEnemy();
            }

            _enemies.GameUpdate();
        }

        private void SpawnEnemy()
        {
            GameTile spawnPoint = _board.GetSpawnPoint(Random.Range(0, _board.SpawnPointCount));
            Enemy enemy = _enemyFactory.Get();
            enemy.SpawnOn(spawnPoint);
            _enemies.AddEnemy(enemy);
        }
    }
}