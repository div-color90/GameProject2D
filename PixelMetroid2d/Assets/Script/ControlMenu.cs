using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMenu : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void VerCreditos()
    {
        SceneManager.LoadScene("Créditos");
    }
    public void VolverMenu()
    {
        SceneManager.LoadScene("Menú");
    }
    public void Salir()
    {
    #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
