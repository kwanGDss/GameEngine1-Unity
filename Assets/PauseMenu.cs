using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseUI;

    public static bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;
        }

        if (paused)
        {
            pauseUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Time.timeScale = 0;
        }

        if (!paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            pauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        paused = !paused;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
