using UnityEngine;
using UnityEngine.SceneManagement;

public class ToRecipe : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("RecipeScene");
    }
}
