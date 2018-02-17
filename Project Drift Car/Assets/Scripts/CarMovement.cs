using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovement : MonoBehaviour {

    public float speed, torqueSpeed;
    public GameObject explosionPS;
    Rigidbody2D carRB;

    [SerializeField]
    int level;
    public Transform StartPos, EndPos, Levels;

    public TextMesh lifesRemaining;

    public GameObject tireFadeGO, tire1, tire2, tire3, tire4;

    public Transform cameraPosRot;

    float points = 1000;

    MainManager managerScript;

    AudioSource derrapeAudio;

    void Start () {
        managerScript = GameObject.Find("MainManager").GetComponent<MainManager>();
        derrapeAudio = GetComponent<AudioSource>();

        //PlayerPrefs.SetInt("HardOn", 1);
        carRB = GetComponent<Rigidbody2D>();

        //PlayerPrefs.SetInt("Level", 7);
        level = managerScript.level;
        Levels.GetChild(level - 1).gameObject.SetActive(true);

        switch (level)
        {
            case 1:
            case 2:
                StartPos.position = new Vector3(-9.7f, -6f, 0.3f);
                EndPos.position = new Vector3(9.7f, 6f, 0.3f);
                break;
            case 7:
                StartPos.position = new Vector3(-9.7f, -6f, 0.3f);
                EndPos.position = new Vector3(1.5f, 5.5f, 0.3f);
                break;
            case 3:
                StartPos.position = new Vector3(-9.7f, -6f, 0.3f);
                EndPos.position = new Vector3(-9.7f, 6f, 0.3f);
                break;
            case 4:
                StartPos.position = new Vector3(-9.7f, -6f, 0.3f);
                EndPos.position = new Vector3(9.7f, 6f, 0.3f);
                break;
            case 5:
                StartPos.position = new Vector3(-9.7f, -6f, 0.3f);
                EndPos.position = new Vector3(9.7f, 6f, 0.3f);
                break;
            case 6:
                StartPos.position = new Vector3(-9.7f, -6f, 0.3f);
                EndPos.position = new Vector3(-0.64f, 6f, 0.3f);
                break;
            case 8:
                StartPos.position = new Vector3(-9.7f, -6f, 0.3f);
                EndPos.position = new Vector3(-2.5f, -1f, 0.3f);
                break;
        }

        transform.position = new Vector2(StartPos.position.x, StartPos.position.y);

        if (managerScript.hardOn == 1)
        {
            cameraPosRot.parent = transform;
            cameraPosRot.localPosition = new Vector3(0, 0, -0.15f);
            cameraPosRot.localRotation = Quaternion.Euler(-90, 0 ,0);
        }
	}
	
	void Update () {
        if (carRB.velocity.y < 5)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                carRB.AddRelativeForce(Vector2.up * speed);
            }

            if (Input.GetAxis("Vertical") < 0)
            {
                carRB.AddRelativeForce(Vector2.down * speed/2);
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

        if (points > 0)
        {
            points -= 0.3f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.collider.tag)
        {
            case "Danger":
                if (managerScript.hardOn == 1)
                {
                    cameraPosRot.parent = null;
                    cameraPosRot.position = new Vector3(0, 0, -15f);
                    cameraPosRot.rotation = Quaternion.Euler(0, 0, 0);
                }

                Instantiate(explosionPS,transform.position,transform.rotation);

                //Escribe en pantalla las vidas restantes
                managerScript.lives = managerScript.lives - 1;
                if (managerScript.lives == 0)
                {
                    lifesRemaining.text = "Game Over";
                }
                else
                {
                    lifesRemaining.text = "Lives " + managerScript.lives.ToString("00");
                }
                lifesRemaining.gameObject.SetActive(true);

                Destroy(gameObject);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "End":
                managerScript.points = Mathf.RoundToInt(points);
                managerScript.totalPoints = 1000 + managerScript.points + managerScript.totalPoints;
                GetComponent<Collider2D>().enabled = false;
                Invoke("ChangeLevel", 0.5f);
                carRB.drag = 5;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "ChangeEnd":
                switch (level)
                {
                    case 7:
                        EndPos.position = new Vector3(9.5f, -6f, 0.3f);
                        break;
                    case 8:
                        switch (Random.Range(0, 4))
                        {
                            case 1:
                                EndPos.position = new Vector3(5.5f, -3.6f, 0.3f);
                                break;
                            case 2:
                                EndPos.position = new Vector3(9.5f, 6f, 0.3f);
                                break;
                            case 3:
                                EndPos.position = new Vector3(-9.7f, -4f, 0.3f);
                                break;
                        }
                        break;
                }
                break;
        }
    }

    void ChangeLevel()
    {
        if (level < Levels.childCount)
        {
            level++;
            managerScript.level = level;
            SceneManager.LoadScene(1);
        } else
        {
            level = 1;
            managerScript.level = level;
            //cargar creditos
            SceneManager.LoadScene(3);
        }
    }

    void DrawTireBurn()
    {
        Instantiate(tireFadeGO, tire1.transform.position, transform.rotation);
        Instantiate(tireFadeGO, tire2.transform.position, transform.rotation);
        Instantiate(tireFadeGO, tire3.transform.position, transform.rotation);
        Instantiate(tireFadeGO, tire4.transform.position, transform.rotation);
    }
}
