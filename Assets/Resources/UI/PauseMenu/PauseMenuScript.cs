using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private bool inPause;

    // Start is called before the first frame update
    void Awake()
    {
        inPause = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resume ()
    {
        inPause = true;
    }

    public void Rematch ()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseMode()
    {
        //Imposta le proprietà (canShoot, canMove) degli oggetti nella scena a false
    }

    public bool InPause
    {
        get { return inPause; }
        set { inPause = value; }
    }
}
