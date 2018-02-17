using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarIntro : MonoBehaviour {

    public float speed, torqueSpeed;
    Rigidbody2D carRB;

    public Transform obstacles, startPos, normalOption, hardOption, button, title;
    public Text normalText, hardText, instrucText;

    public GameObject tireFadeGO, tire1, tire2, tire3, tire4;

    MainManager managerScript;

    AudioSource derrapeAudio;

    void Start()
    {
        managerScript = GameObject.Find("MainManager").GetComponent<MainManager>();
        derrapeAudio = GetComponent<AudioSource>();

        //Poner Menu, o pantalla next
        if (managerScript.level > 1)
        {
            obstacles.GetChild(0).gameObject.SetActive(false);
            obstacles.GetChild(1).gameObject.SetActive(false);
            obstacles.GetChild(6).transform.position = new Vector3(1.3f, -1.2f, 0);
            startPos.gameObject.SetActive(false);
            normalOption.gameObject.SetActive(false);
            hardOption.gameObject.SetActive(false);
            title.gameObject.SetActive(false);
            normalText.gameObject.SetActive(false);
            hardText.gameObject.SetActive(false);
            instrucText.text = "Finish Points: 1,000\nTime Points: "
                                + managerScript.points.ToString("#,##0")
                                + "\n \nTotal this round: " + (1000 + managerScript.points).ToString("#,##0")
                                + "\n \nTotal Points: " + managerScript.totalPoints.ToString("#,##0");

            button.gameObject.SetActive(true);
        } else
        {
            managerScript.hardOn = 0;
            managerScript.lives = 10;
            managerScript.points = 1000;
            managerScript.totalPoints = 0;
        }

        carRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (carRB.velocity.y < 5)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                carRB.AddRelativeForce(Vector2.up * speed);
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                carRB.AddRelativeForce(Vector2.down * speed / 2);
            }
        }

        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            carRB.AddTorque(torqueSpeed);
            DrawTireBurn();
            if (!derrapeAudio.isPlaying)
            {
                derrapeAudio.Play();
            }
        }
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            carRB.AddTorque(-torqueSpeed);
            DrawTireBurn();
            if (!derrapeAudio.isPlaying)
            {
                derrapeAudio.Play();
            }
        }

        if (Input.GetAxisRaw("Horizontal") == 0 && derrapeAudio.isPlaying)
        {
            derrapeAudio.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "NormalMode":
                managerScript.hardOn = 0;
                Invoke("ChangeLevel", 0.5f);
                break;
            case "HardMode":
                managerScript.hardOn = 1;
                Invoke("ChangeLevel", 0.5f);
                break;
        }
    }

    public void ChangeLevel()
    {
        managerScript.points = 1000;
        SceneManager.LoadScene(2);
    }

    void DrawTireBurn()
    {
        Instantiate(tireFadeGO, tire1.transform.position, transform.rotation);
        Instantiate(tireFadeGO, tire2.transform.position, transform.rotation);
        Instantiate(tireFadeGO, tire3.transform.position, transform.rotation);
        Instantiate(tireFadeGO, tire4.transform.position, transform.rotation);
    }
}