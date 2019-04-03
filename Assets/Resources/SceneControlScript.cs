using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControlScript : MonoBehaviour
{
    Transform pauseMenu;

    // Start is called before the first frame update
    void Awake()
    {
        pauseMenu = GameObject.Find("PauseMenu").transform;
    }

    // Update is called once per frame
    void Update()
    {
        PauseClick();
    }

    private void PauseClick ()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pauseMenu.GetComponent<PauseMenuScript>().InPause)
            {
                pauseMenu.GetChild(0).gameObject.SetActive(true);
                pauseMenu.GetChild(1).gameObject.SetActive(true);
                pauseMenu.GetComponent<PauseMenuScript>().InPause = true;
                pauseMenu.GetComponent<PauseMenuScript>().PauseMode();
            }
            else
            {
                pauseMenu.GetChild(0).gameObject.SetActive(false);
                pauseMenu.GetChild(1).gameObject.SetActive(false);
                pauseMenu.GetComponent<PauseMenuScript>().InPause = false;
                pauseMenu.GetComponent<PauseMenuScript>().PauseMode();
            }
        }
    }
}
