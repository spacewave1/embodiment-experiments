using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] private bool loadNextScene;
    [SerializeField] private bool startScene;

    public void call()
    {
        if (loadNextScene)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (startScene)
        {
            Procedure.Instance.initiate();
            Destroy(gameObject);
        }
    }
}
