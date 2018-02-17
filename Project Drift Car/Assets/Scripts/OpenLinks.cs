using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenLinks : MonoBehaviour {

    public void openItch()
    {
        Application.OpenURL("https://alberto-luviano.itch.io/");
    }

    public void openNG()
    {
        Application.OpenURL("https://geckomla19097.newgrounds.com/");
    }

    public void openTwitter()
    {
        Application.OpenURL("https://twitter.com/AlbaLuvs");
    }

    public void openKofi()
    {
        Application.OpenURL("https://ko-fi.com/X8X05YVF");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
