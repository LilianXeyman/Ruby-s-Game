using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitAndReload : MonoBehaviour
{
    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detiene el juego en el editor
#else
        Application.Quit(); // Cierra la aplicación en la build
#endif
    }
}
