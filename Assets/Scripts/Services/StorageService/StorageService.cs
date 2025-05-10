using System.Collections.Generic;
using UnityEngine;
using Storage.Scripts;
using Zenject;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.Events;

public class StorageService : MonoBehaviour
{
    public GameData GameData => _gameData;

    [SerializeField] private List<GameObject> dynamicPrefabs;
    [SerializeField] private UnityEvent _onLoad;
    [SerializeField] private UnityEvent _onLoadComplete;
    [SerializeField] private UnityEvent _onSaveComplete;

    [Inject] private IStorageService _storageService;
    [Inject] private ShouldLoadFlag _shouldLoadFlag;
    [Inject] private DiContainer _container;

    private KeyInputService _keyInputService;
    private GameData _gameData;
    private bool _isLoad;

    private Dictionary<string, GameObject> _prefabDictionary;
    private readonly List<BaseStorage> _seveables = new List<BaseStorage>();
    private bool _isCanStorage = true;

    private int _loadSteps;
    private int _currentStep;
    private int _loadProgress;

    public int LoadProgress => _loadProgress;

    private void Awake()
    {
        _keyInputService = new KeyInputService();
        _gameData = new GameData(new Dictionary<string, SaveData>());
        _isLoad = false;
        _currentStep = 0;

        _prefabDictionary = new Dictionary<string, GameObject>();

        foreach (var prefab in dynamicPrefabs)
        {
            var saveable = prefab.GetComponent<SaveableDynamicObject>();

            if (saveable != null)
            {
                _prefabDictionary.Add(saveable.PrefabId, prefab);
            }
            else
            {
                Debug.LogError($"Prefab {prefab.name} doesn't have SaveableDynamicObject component");
            }
        }
    }

    private void Start()
    {
        if (_shouldLoadFlag.NeedToLoad)
        {
            StartCoroutine(LoadGame());
        }
    }

    public void RegisterSaveable(BaseStorage saveable)
    {
        if (!_seveables.Contains(saveable))
        {
            _seveables.Add(saveable);
        }
    }

    private void Update()
    {
        if (_keyInputService.IsSavePressed() && _isCanStorage)
        {
            StartCoroutine(SaveGame());
        }

        if (_keyInputService.IsLoadPressed() && _isCanStorage)
        {
            StartCoroutine(LoadGame());
        }
    }

    public void UnregisterSaveable(BaseStorage saveable)
    {
        _seveables.Remove(saveable);
    }

    public IEnumerator SaveGame()
    {
        _onSaveComplete?.Invoke();

        foreach (var saveable in _seveables)
        {
            saveable.InitData(saveable.SaveDataValue);
            yield return null;
        }

        _storageService.Save(GetKey(), _gameData);
        Debug.Log("Save complete");
    }

    public IEnumerator LoadGame()
    {
        _onLoad?.Invoke();
        _isLoad = true;

        _storageService.Load<GameData>(GetKey(), data =>
        {
            if (data != null)
            {
                _gameData = new GameData(data.SaveableObjects);
            }
        });

        foreach (var saveableObject in _gameData.SaveableObjects)
        {
            if (saveableObject.Value.IsDynamic)
            {
                var dynamicData = saveableObject.Value as DynamicData;

                bool alreadyExist = _seveables.Any(s => s.GetSaveKey() == saveableObject.Key);

                if (alreadyExist)
                {
                    continue;
                }

                if (dynamicData != null && _prefabDictionary.TryGetValue(dynamicData.PrefabID, out var prefab))
                {
                    var newObj = _container.InstantiatePrefab(prefab);
                    newObj.name = dynamicData.Name;
                    yield return null;
                }
                else
                {
                    Debug.LogError($"Prefab with ID {dynamicData?.PrefabID} not found");
                }
            }
            yield return null;
        }

        yield return new WaitUntil(() => _seveables != null);

        _loadSteps = _seveables.Count;

        foreach (var saveable in _seveables)
        {
            var saveKey = saveable.GetSaveKey();

            if (_gameData.SaveableObjects.TryGetValue(saveKey, out var savedData))
            {
                saveable.Load(savedData);
            }
            _currentStep++;
            _loadProgress = 1 / (_loadSteps / _currentStep);
            yield return null;
        }

        _onLoadComplete?.Invoke();
        _loadProgress = 0;
        _currentStep = 0;
        _isLoad = false;
        Debug.Log("Load complete");
    }

    private string GetKey()
    {
        return $"{gameObject.scene.name}_{gameObject.name}";
    }

    public void StorageDisable()
    {
        _isCanStorage = false;
    }

    public void StorageEnable()
    {
        _isCanStorage = true;
    }
}

[Serializable]
public class GameData
{
    public Dictionary<string, SaveData> SaveableObjects;

    public GameData(Dictionary<string, SaveData> saveableObjects)
    {
        SaveableObjects = saveableObjects;
    }

    public void AddData(string id, SaveData ObjectData)
    {
        if (!SaveableObjects.ContainsKey(id))
        {
            SaveableObjects.Add(id, ObjectData);
        }
    }

    public T GetData<T>(string id) where T : SaveData
    {
        if (SaveableObjects.TryGetValue(id, out SaveData data) && data is T typedData)
            return typedData;
        return null;
    }
}