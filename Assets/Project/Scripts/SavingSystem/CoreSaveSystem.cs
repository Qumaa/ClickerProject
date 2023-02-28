using Assets.Project.Scripts.SavingSystem.SaveStructs;
using UnityEngine;
using System.IO;

namespace Assets.Project.Scripts.SavingSystem
{
    public class CoreSaveSystem : MonoBehaviour
    {
        [Header("Levels")]
        [SerializeField] private int _levelIndex;
        [SerializeField] private LevelStruct[] _levels;

        [Header("Save Config")]
        [SerializeField] private string _savePath;
        [SerializeField] private string _saveFileName = "dataJson";

        public void SaveToFile()
        {
            GameCoreStruct gameCore = new GameCoreStruct
            {
                _levelIndex = this._levelIndex,
                _levels = this._levels
            };

            string json = JsonUtility.ToJson(gameCore, true);

            try
            {
                File.WriteAllText(_savePath, json);
            }
            catch 
            {
                Debug.Log("File does not exist");
            }           
        }

        public void LoadFromFile()
        {
            if (!File.Exists(_savePath))
            {
                Debug.Log("File not found");
                return;
            }

            try
            {
                string json = File.ReadAllText(_savePath);

                GameCoreStruct gameCoreFromJson = JsonUtility.FromJson<GameCoreStruct>(json);

                this._levelIndex = gameCoreFromJson._levelIndex;
                this._levels = gameCoreFromJson._levels;
            }
            catch
            {
                Debug.Log("Unluck");
            }
            
        }

        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _savePath = Path.Combine(Application.persistentDataPath, _saveFileName)
#else
            _savePath = Path.Combine(Application.dataPath, _saveFileName);
#endif 
            LoadFromFile();
        }

        private void OnApplicationQuit()
        {
            SaveToFile();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                SaveToFile();
            }
        }
    }
}

