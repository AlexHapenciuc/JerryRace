using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptulPrincipal : MonoBehaviour
{
    private int xMin, xMax;
    public GameObject Muzica;


    void Awake()
    {
        //1.
        xMin = -6;
        xMax = 7;
        scor = 0;
        distMax = 0;
        nivel = -1;
        timp = 100;
        cronometruTimp = cronometruActiuni = 0;
        //2.
        yMin = 1; yMax = 11;
        Generator();
        nivel += 1;  yMin = yMax + 1;  yMax += 20;
        //3.
        if (PlayerPrefs.GetString("setareMuzica") == "da")
            Instantiate(Muzica);
    }


    public Text Scor, Timp;
    public GameObject CameraPrincipala, Jucator, Platforma;
    private int  scor, timp, distMax;
    private float cronometruActiuni, cronometruTimp;
    void Update()
    {
        /*1.*/if (Jucator.transform.position.y > distMax)
                foreach (GameObject Soricel in ListaSoricei)
                {
                    scor += 5;
                    distMax = (int)Jucator.transform.position.y;
                }     
        /*2.*/
        Scor.text = "Scor: " + scor;
        Timp.text = "Timp: " + timp;
        cronometruTimp += Time.deltaTime;
        cronometruActiuni += Time.deltaTime;
        /*3.*/if (timp <= 0)
        {
            if (scor > PlayerPrefs.GetInt("HighScore", 0))
                PlayerPrefs.SetInt("HighScore", scor);
            PlayerPrefs.SetInt("ScorCurent", scor);

            SceneManager.LoadScene("EcranDeSfarsit");
        }
        /*4.*/if (Jucator.transform.position.y >= 0)
                if (cronometruTimp >= 0.25f)
                {
                    timp -= 1;
                    cronometruTimp = 0;
                }
        /*5.*/if (cronometruActiuni >= 0.25f)
        ActiuneJucator();
        /*6.*/
        if (Jucator.transform.position.y >= 3 + 20 * nivel)
        {
            Generator();

            nivel += 1;
            yMin = yMax + 1;
            yMax += 20;
        }
        /*7.*/if (Jucator.transform.position.y >= 3)
        {
            CameraPrincipala.transform.position = new Vector3(0, Jucator.transform.position.y, -10);
            Platforma.transform.position = new Vector3(0, Jucator.transform.position.y + 0.5f, 5);
        }
    }

    
    private GameObject[] ListaSoricei, ListaMotani, ListaSofa, ListaPian, ListaDulap, ListaCapcana, ListaCascaval;
    private void ActiuneJucator()
    {
        ListaSoricei  = GameObject.FindGameObjectsWithTag("Soricei");
        ListaMotani   = GameObject.FindGameObjectsWithTag("Motani");  ListaSofa = GameObject.FindGameObjectsWithTag("Sofa");
        ListaPian     = GameObject.FindGameObjectsWithTag("Pian");    ListaDulap = GameObject.FindGameObjectsWithTag("Dulap");
        ListaCapcana  = GameObject.FindGameObjectsWithTag("Capcana"); ListaCascaval = GameObject.FindGameObjectsWithTag("Cascaval");
        
        if (Input.GetKey("up"))
        {
            foreach (GameObject Soricel in ListaSoricei)
            {
                Verificare_Motani(Soricel, 0, 1);
                bool ok = Verificare_Soricei(Soricel, 0, 1);
                if (ok) ok = Verificare_Sofa1(Soricel, 1);
                if (ok) ok = Verificare_Pian1(Soricel, 1);
                if (ok) ok = Verificare_Dulap(Soricel, 0, 1);
                if (ok) ok = Verificare_Capcana(Soricel, 0, 1);
                if (ok)
                {
                    Verificare_Cascaval(Soricel, 0, 1);
                    Soricel.transform.position = new Vector3(Soricel.transform.position.x, Soricel.transform.position.y + 1, 0);
                }
            }

            cronometruActiuni = 0;
        }
        else if (Input.GetKey("down"))
        {
            foreach (GameObject Soricel in ListaSoricei)
                if ((Jucator.transform.position.y >= 0 && Soricel.transform.position.y >= 1) || (Jucator.transform.position.y < 0 && Soricel.transform.position.y >= -4)) 
                {
                    Verificare_Motani(Soricel, 0, -1);
                    bool ok = Verificare_Soricei(Soricel, 0, -1);
                    if (ok) ok = Verificare_Sofa1(Soricel, -2);
                    if (ok) ok = Verificare_Pian1(Soricel, -1);
                    if (ok) ok = Verificare_Dulap(Soricel, 0, -1);
                    if (ok) ok = Verificare_Capcana(Soricel, 0, -1);
                    if (ok)
                    {
                        Verificare_Cascaval(Soricel, 0, -1);
                        Soricel.transform.position = new Vector3(Soricel.transform.position.x, Soricel.transform.position.y - 1, 0);
                    }
                }

            cronometruActiuni = 0;
        }
        else if (Input.GetKey("left"))
        {
            foreach (GameObject Soricel in ListaSoricei)
                if (Soricel.transform.position.x > xMin)
                {
                    Verificare_Motani(Soricel, -1, 0);
                    bool ok = Verificare_Soricei(Soricel, -1, 0);
                    if (ok) ok = Verificare_Sofa2(Soricel, -2);
                    if (ok) ok = Verificare_Pian2(Soricel, -2);
                    if (ok) ok = Verificare_Dulap(Soricel, -1, 0);
                    if (ok) ok = Verificare_Capcana(Soricel, -1, 0);
                    if (ok)
                    {
                        Verificare_Cascaval(Soricel, -1, 0);
                        Soricel.transform.position = new Vector3(Soricel.transform.position.x - 1, Soricel.transform.position.y, 0);
                    }
                }

            cronometruActiuni = 0;
        }
        else if (Input.GetKey("right"))
        {
            foreach (GameObject Soricel in ListaSoricei)
                if (Soricel.transform.position.x < xMax-1)
                {
                    Verificare_Motani(Soricel, 1, 0);
                    bool ok = Verificare_Soricei(Soricel, 1, 0);
                    if (ok) ok = Verificare_Sofa2(Soricel, 2);
                    if (ok) ok = Verificare_Pian2(Soricel, 2);
                    if (ok) ok = Verificare_Dulap(Soricel, 1, 0);
                    if (ok) ok = Verificare_Capcana(Soricel, 1, 0);
                    if (ok)
                    {
                        Verificare_Cascaval(Soricel, 1, 0);
                        Soricel.transform.position = new Vector3(Soricel.transform.position.x + 1, Soricel.transform.position.y, 0);
                    }
                }

            cronometruActiuni = 0;
        }
    }

    private int yMin, yMax, nivel;
    public GameObject Sofa, Pian, Tom, Dulap, Capcana, Cascaval;
    private void Generator()
    {
        int ySofa, yPian, yTom;
        int xSofa, xPian, xTom, xDulap, xCascaval, xObiect3;

        ySofa = Random.Range(yMin, yMax - 1);
        yPian = Random.Range(yMin, yMax);
        yTom  = Random.Range(yMin, yMax);
        while (yPian == ySofa || yPian == ySofa + 1) 
            yPian = Random.Range(yMin, yMax);
        while (yTom == ySofa || yTom == ySofa + 1 || yTom == yPian)
            yTom = Random.Range(yMin, yMax);

        for (int y = yMin; y <= yMax; y++)
        {
            if (y == ySofa)
            {
                xSofa    = Random.Range(xMin + 1, xMax - 1);
                xDulap   = Random.Range(xMin, xMax);
                xObiect3 = Random.Range(xMin, xMax);
                while (xDulap == xSofa - 1 || xDulap == xSofa || xDulap == xSofa + 1)
                    xDulap   = Random.Range(xMin, xMax);
                while (xObiect3 == xSofa - 1 || xObiect3 == xSofa || xObiect3 == xSofa + 1 || xObiect3 == xDulap)
                    xObiect3 = Random.Range(xMin, xMax);

                Instantiate(Sofa,  new Vector3(xSofa,  y, 0),  Sofa.transform.rotation);
                Instantiate(Dulap, new Vector3(xDulap, y,  0), Dulap.transform.rotation);

                if (Random.Range(1, 100) < 75)
                    Instantiate(Dulap,   new Vector3(xObiect3, y, 0), Dulap.transform.rotation);
                else
                    Instantiate(Capcana, new Vector3(xObiect3, y, 0), Capcana.transform.rotation);

                y++;
            }
            else if (y == yPian)
            {
                xPian    = Random.Range(xMin + 1, xMax - 1);
                xDulap   = Random.Range(xMin, xMax);
                xObiect3 = Random.Range(xMin, xMax);
                while (xDulap == xPian - 1 || xDulap == xPian || xDulap == xPian + 1)
                    xDulap   = Random.Range(xMin, xMax);
                while (xObiect3 == xPian - 1 || xObiect3 == xPian || xObiect3 == xPian + 1 || xObiect3 == xDulap)
                    xObiect3 = Random.Range(xMin, xMax);

                Instantiate(Pian,  new Vector3(xPian,  y, 0), Sofa.transform.rotation);
                Instantiate(Dulap, new Vector3(xDulap, y, 0), Dulap.transform.rotation);
                if (Random.Range(1, 100) < 75)
                    Instantiate(Dulap,   new Vector3(xObiect3, y, 0), Dulap.transform.rotation);
                else
                    Instantiate(Capcana, new Vector3(xObiect3, y, 0), Capcana.transform.rotation);
            }
            else if (y == yTom)
            {
                xTom     = Random.Range(xMin, xMax);
                xDulap   = Random.Range(xMin, xMax);
                xObiect3 = Random.Range(xMin, xMax);
                while (xDulap == xTom)
                    xDulap   = Random.Range(xMin, xMax);
                while (xObiect3 == xTom || xObiect3 == xDulap)
                    xObiect3 = Random.Range(xMin, xMax);

                Instantiate(Tom,   new Vector3(xTom,   y, 0), Tom.transform.rotation);
                Instantiate(Dulap, new Vector3(xDulap, y, 0), Dulap.transform.rotation);

                if (Random.Range(1, 100) < 75)
                    Instantiate(Dulap,   new Vector3(xObiect3, y, 0), Dulap.transform.rotation);
                else
                    Instantiate(Capcana, new Vector3(xObiect3, y, 0), Capcana.transform.rotation);
            }
            else
            {
                xCascaval = Random.Range(xMin, xMax);
                xDulap    = Random.Range(xMin, xMax);
                xObiect3  = Random.Range(xMin, xMax);
                while (xDulap == xCascaval)
                    xCascaval = Random.Range(xMin, xMax);    
                while (xObiect3 == xCascaval || xObiect3 == xDulap)
                    xObiect3  = Random.Range(xMin, xMax);

                Instantiate(Cascaval, new Vector3(xCascaval, y, 0), Cascaval.transform.rotation);
                Instantiate(Dulap,    new Vector3(xDulap,    y, 0), Dulap.transform.rotation);

                if (Random.Range(1, 100) < 75)
                    Instantiate(Dulap,   new Vector3(xObiect3, y, 0), Dulap.transform.rotation);
                else
                    Instantiate(Capcana, new Vector3(xObiect3, y, 0), Capcana.transform.rotation);
            }
        }
    }

    
    private void Verificare_Motani(GameObject Soricel, int x, int y)
    {
        foreach (GameObject xM in ListaMotani)
            if ((xM.transform.position.x == Soricel.transform.position.x + x) 
                && (xM.transform.position.y == Soricel.transform.position.y + y))
            {
                if (Soricel.name == "Jerry")
                {
                    if (scor > PlayerPrefs.GetInt("HighScore", 0))
                        PlayerPrefs.SetInt("HighScore", scor);
                    PlayerPrefs.SetInt("ScorCurent", scor);

                    SceneManager.LoadScene("EcranDeSfarsit");
                }
                else
                {
                    Destroy(Soricel);
                    scor -= 20;
                    timp -= 20;
                }
            }
    }

    private bool Verificare_Soricei(GameObject Soricel, int x, int y)
    {
        foreach (GameObject xS in ListaSoricei)
            if ((xS.transform.position.x == Soricel.transform.position.x + x) 
                && (xS.transform.position.y == Soricel.transform.position.y + y))
                return false;

        return true;
    }

    private bool Verificare_Sofa1(GameObject Soricel, int y)
    {
        foreach (GameObject xS in ListaSofa)
            if (xS.transform.position.y == Soricel.transform.position.y + y)
            {
                     if (xS.transform.position.x == Soricel.transform.position.x - 1) return false;
                else if (xS.transform.position.x == Soricel.transform.position.x)     return false;
                else if (xS.transform.position.x == Soricel.transform.position.x + 1) return false;
            }

        return true;
    }

    private bool Verificare_Sofa2(GameObject Soricel, int x)
    {
        foreach (GameObject xS in ListaSofa)
            if (xS.transform.position.x == Soricel.transform.position.x + x)
            {
                     if (xS.transform.position.y == Soricel.transform.position.y)     return false;
                else if (xS.transform.position.y == Soricel.transform.position.y - 1) return false;
            }

        return true;
    }

    private bool Verificare_Pian1(GameObject Soricel, int y)
    {
        foreach (GameObject xP in ListaPian)
            if (xP.transform.position.y == Soricel.transform.position.y + y)
            {
                     if (xP.transform.position.x == Soricel.transform.position.x - 1) return false;
                else if (xP.transform.position.x == Soricel.transform.position.x)     return false;
                else if (xP.transform.position.x == Soricel.transform.position.x + 1) return false;
            }

        return true;
    }

    private bool Verificare_Pian2(GameObject Soricel, int x)
    {
        foreach (GameObject xP in ListaPian)
            if ((xP.transform.position.x == Soricel.transform.position.x + x) && (xP.transform.position.y == Soricel.transform.position.y))
                return false;

        return true;
    }

    private bool Verificare_Dulap(GameObject Soricel, int x, int y)
    {
        foreach (GameObject xD in ListaDulap)
            if ((xD.transform.position.x == Soricel.transform.position.x + x) && (xD.transform.position.y == Soricel.transform.position.y + y))
                return false;

        return true;
    }

    private bool Verificare_Capcana(GameObject Soricel, int x, int y)
    {
        foreach (GameObject xC in ListaCapcana)
            if ((xC.transform.position.x == Soricel.transform.position.x + x) && (xC.transform.position.y == Soricel.transform.position.y + y))
            {
                if (Soricel.name == "Jerry")
                {
                    timp -= 20;
                    scor -= 20;
                    Destroy(xC);
                    Soricel.transform.position = new Vector3(Soricel.transform.position.x + x, Soricel.transform.position.y + y, 0);
                    cronometruActiuni = 0;
                }
                else
                {
                    Destroy(Soricel);
                    scor -= 20;
                }

                return false;
            }

        return true;
    }

    private void Verificare_Cascaval(GameObject Soricel, int x, int y)
    {
        foreach (GameObject xC in ListaCascaval)
            if ((xC.transform.position.x == Soricel.transform.position.x + x) 
                && (xC.transform.position.y == Soricel.transform.position.y + y))
            {
                scor += 10;
                timp += 5;
                Destroy(xC);
            }
    }
}