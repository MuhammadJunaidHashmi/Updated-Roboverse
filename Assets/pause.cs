
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{
    public GameObject pauses;
    public GameObject quit;
    // Start is called before the first frame update

    public void clickPause()
    {
        Time.timeScale = 0.0f;
        pauses.SetActive(true);
    }
    public void clickResume()
    {
        Time.timeScale = 1.0f;
        pauses.SetActive(false);
    }
    public void clickQuit()
    {
        quit.SetActive(true);
    }
    public void closeQuit()
    {
        quit.SetActive(false);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
