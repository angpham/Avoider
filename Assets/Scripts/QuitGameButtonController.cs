using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameButtonController : MonoBehaviour
{
    public SceneChanger sceneChanger;

    public void Quit()
    {
        sceneChanger.ChangeScene("GoodbyeScene");
    }
}
