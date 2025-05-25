using UnityEngine;
using UnityEngine.SceneManagement;

public class SubtitlesMover : MonoBehaviour
{
    [SerializeField] private RectTransform _subtitlesPanel;
    [SerializeField] private float _scrollSpeed = 10;
    [SerializeField] private float _endYPosition = 1000;

    private void Update()
    {
        _subtitlesPanel.anchoredPosition += Vector2.up * _scrollSpeed * Time.deltaTime;

        if (_subtitlesPanel.anchoredPosition.y >= _endYPosition)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
}
