using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader {
    public enum Scene {
        MainMenu,
        GameScene,
    }

    static Action onLoaderCallback;

    public static void Load(Scene scene)
    {   

        string sceneName = scene.ToString();

        BaseEnemy.ResetStaticData();

        if (FadeTransition.Instance != null)
        {
            FadeTransition.Instance.FadeInOnSceneChange();

            // Set the loader callback action to load the target scene
            onLoaderCallback = () =>
            {
                SceneManager.LoadSceneAsync(sceneName);
                FadeTransition.Instance.FadeOut();
            };
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
        


    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}
