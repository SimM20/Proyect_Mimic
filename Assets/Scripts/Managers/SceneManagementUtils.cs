using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public static class SceneManagementUtils
{
    public static Action<string> _OnSceneChanged;
    public static void LoadSceneByName(string sceneName) { SceneManager.LoadScene(sceneName); }
    public static void LoadSceneByIndex(int sceneIndex) { SceneManager.LoadScene(sceneIndex); }
    public static void LoadActiveScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    public static Scene GetActiveScene() { return SceneManager.GetActiveScene(); }

    static SceneManagementUtils() { SceneManager.activeSceneChanged += OnSceneChanged; }

    public static void AsyncLoadSceneByName(string sceneName, GameObject loadingScreenPrefab, MonoBehaviour mono)
    {
        GameObject loadingScreen = UnityEngine.Object.Instantiate(loadingScreenPrefab);
        Slider slider = loadingScreen.GetComponentInChildren<Slider>();
        mono.StartCoroutine(LazyLoad(sceneName, loadingScreen, slider));
    }

    private static IEnumerator LazyLoad(string sceneName, GameObject loadingScreen, Slider slider)
    {
        Animator animator = loadingScreen.GetComponent<Animator>();

        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            while (stateInfo.length == 0)
            {
                yield return null;
                stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            }
            yield return new WaitForSeconds(stateInfo.length);
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            slider.value = progress;

            if (asyncLoad.progress >= 0.9f)
            {
                slider.value = 1f;
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    private static void OnSceneChanged(Scene oldScene, Scene newScene) {  _OnSceneChanged?.Invoke(newScene.name); }
}
