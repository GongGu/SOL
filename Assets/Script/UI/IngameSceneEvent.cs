using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameSceneEvent : MonoBehaviour {

    public static IngameSceneEvent ingameScene;

    public Canvas canvas;

    private void Awake()
    {
        ingameScene = this;

        canvas.enabled = true;

        gameObject.SetActive(false);
    }

    public void OnClickGameOver()
    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
