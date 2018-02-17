using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

    public int level = 1, hardOn, lives = 10, points = 1000, totalPoints;
    public TextMesh tributeText;
    public Color start, end;

	void Start () {
        Invoke("LoadMainMenu",9);
        DontDestroyOnLoad(gameObject);
        StartCoroutine(Introduction());
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(3);
    }

    IEnumerator Introduction()
    {
        while (Time.time < 8)
        {
            tributeText.color = Color.Lerp(start, end, Mathf.Sin((Time.time - 1) / 2));
            yield return null;
        }
    }
}
