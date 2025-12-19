using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToScene : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private SceneAsset nextScene;
#endif

    private string nextSceneName;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (nextScene != null)
        {
            nextSceneName = nextScene.name;
        }
    }
#endif

    public void MoveToNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next Scene이 설정되지 않았습니다.");
        }
    }
}
