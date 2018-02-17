using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class finalPoints : MonoBehaviour {

	void Start () {
        MainManager managerScript = GameObject.Find("MainManager").GetComponent<MainManager>();
        int livesBonus = managerScript.lives * 1000;
        int hardBonus = managerScript.hardOn * 5000;
        string finalPoints = (managerScript.totalPoints + livesBonus + hardBonus).ToString("#,##0");
        GetComponent<Text>().text = "Your final score is: " + finalPoints;
    }
}
