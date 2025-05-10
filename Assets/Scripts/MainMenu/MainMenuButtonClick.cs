using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class MainMenuButtonClick : MonoBehaviour
{
    private ShouldLoadFlag _shouldLoadFlag;
    
    [Inject]
    public void Construct(ShouldLoadFlag shouldLoadFlag)
    {
        _shouldLoadFlag = shouldLoadFlag;
    }

    public void OnContinueButtonClicked()
    {
        if (_shouldLoadFlag == null)
        {
            Debug.LogError("ShouldLoadFlag не инжектирован!");
            return;
        }
        
        _shouldLoadFlag.IsNeedToLoad(true);
        SceneManager.LoadScene("SampleScene");
    }

    public void OnNewGameButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
