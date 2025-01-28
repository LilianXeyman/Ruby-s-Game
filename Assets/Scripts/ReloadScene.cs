using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    public static ReloadScene instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void RechargeScene()
    {
        // Obtén el nombre de la escena activa
        string currentSceneName = SceneManager.GetActiveScene().name;

        // Recarga la escena activa
        SceneManager.LoadScene(currentSceneName);
    }
}
