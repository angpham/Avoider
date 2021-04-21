using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodbyeSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.Quit();
        Debug.Log("Quitting...");
    }
}
