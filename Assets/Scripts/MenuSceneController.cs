using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    public SceneChanger sceneChanger;
    
    public void Play()
    {
        sceneChanger.ChangeScene("GameScene");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}