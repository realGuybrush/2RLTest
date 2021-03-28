using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    Scene mainScene;
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadSceneAsync("MainScene");
        }
    }
}
