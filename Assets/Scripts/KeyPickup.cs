using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public SceneChanger sceneChanger;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Player")
        {
            Destroy(this.gameObject);
            Debug.Log("Key Collected!");
            sceneChanger.ChangeScene("MenuScene");
        }
    }  
}
