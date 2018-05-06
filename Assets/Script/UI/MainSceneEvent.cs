using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneEvent : MonoBehaviour {

    public void OnClickGameStart()
    {
        SceneManager.LoadScene("stage1");
    }
    public void OnClickToArmory()
    {
        SceneManager.LoadScene("armory");
    }
}
