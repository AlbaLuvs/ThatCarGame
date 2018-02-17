using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyRestart : MonoBehaviour {

    MainManager managerScript;

	void Start () {
        managerScript = GameObject.Find("MainManager").GetComponent<MainManager>();
        ParticleSystem PS = GetComponent<ParticleSystem>();
        float t = PS.main.startLifetime.constantMax;
        Invoke("RestartGame", t - 0.1f);
        Destroy(gameObject, t);
	}

    void RestartGame()
    {
        if (managerScript.lives == 0)
        {
            managerScript.level = 1;
            managerScript.hardOn = 0;
            //managerScript.lives = 10;
            managerScript.points = 1000;
            //managerScript.totalPoints = 0;

            //Cargar creditos
            SceneManager.LoadScene(3);
        }
        else
        {
            managerScript.points = 1000;
            SceneManager.LoadScene(2);
        }
    }
}
