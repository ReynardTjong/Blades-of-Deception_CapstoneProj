using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this line if you need to reference UI components

public class SceneSwitcher : MonoBehaviour
{
    // This method will be called when the button is clicked
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}