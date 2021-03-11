using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptMeniu : MonoBehaviour
{
    public Image fundal_butonMuzica;

    void Awake()
    {
        if (PlayerPrefs.GetString("setareMuzica") == "nu")
            fundal_butonMuzica.enabled = false;
        else
            fundal_butonMuzica.enabled = true;
    }

    public void Joc_nou()
    {
        SceneManager.LoadScene("Joc");
    }

    public void Muzica()
    {
        if (PlayerPrefs.GetString("setareMuzica") == "nu")
        {
            PlayerPrefs.SetString("setareMuzica", "da");
            fundal_butonMuzica.enabled = true;
        }
        else
        {
            PlayerPrefs.SetString("setareMuzica", "nu");
            fundal_butonMuzica.enabled = false;
        }
    }

    public void Iesire()
    {
        Application.Quit();
    }
}