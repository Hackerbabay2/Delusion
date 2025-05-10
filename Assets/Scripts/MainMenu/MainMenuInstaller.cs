using System.IO;
using ModestTree;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuInstaller : MonoBehaviour
{
    [SerializeField] private Button _loadButton; 
    
    private void Awake()
    {
        string savePath = Application.persistentDataPath;
        
        bool hasSaveFiles = Directory.Exists(savePath) && 
                            (Directory.GetFiles(savePath).Length > 0 || 
                             Directory.GetDirectories(savePath).Length > 0);
        
        _loadButton.interactable = hasSaveFiles;
    }
}
