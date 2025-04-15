using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneManagerUtils
{
    public static void LoadSceneByName(string sceneName) { SceneManager.LoadScene(sceneName); }
    public static void LoadSceneByIndex(int sceneIndex) { SceneManager.LoadScene(sceneIndex); }
    public static void LoadActiveScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    public static Scene GetActiveScene() { return SceneManager.GetActiveScene(); }
}
