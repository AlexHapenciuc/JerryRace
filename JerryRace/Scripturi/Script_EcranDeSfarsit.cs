using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Script_EcranDeSfarsit : MonoBehaviour
{
    public Text scor_jucator, scor_1;

    void Awake()
    {
        scor_jucator.text = "Scorul tau: " + PlayerPrefs.GetInt("ScorCurent");
        scor_1.text = "Cel mai bun scor: " + PlayerPrefs.GetInt("HighScore");
    }

    public void Joc_nou()
    {
        SceneManager.LoadScene("Joc");
    }

    public void Meniu()
    {
        SceneManager.LoadScene("Meniu");
    }

    public void Iesire()
    {
        Application.Quit();
    }
}

