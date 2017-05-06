using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject gameController;

    public string name;
    float startTime;
    public float bestTime;

    bool hasCrossedFirstTime = false;
    public bool raceStarted = false;

    void Start() {
        raceStarted = false;

        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameController.GetComponent<GameController>().startGame += Drive;
    }

    void Drive() {
        raceStarted = true;
        GetComponent<CarEngine>().CreatePath();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("GoalLine")) {
            if (hasCrossedFirstTime) {
                //print(GetTimeString());
               // CheckBestTime();
                startTime = gameController.GetComponent<GameController>().time;
            } else {
                hasCrossedFirstTime = true;
            }
        }
    }

    /*public string GetTimeString() {
        int minutes = (int) Mathf.Floor(time / 60);
        int seconds = (int) time % 60;
        float mSeconds = (float) Mathf.Floor((time % 1) * 1000f);

        return minutes + ":" + seconds + ":" + mSeconds;
    }

    void CheckBestTime() {
        if (bestTime == 0) {
            bestTime = time;
        } else if (time < bestTime) {
            bestTime = time;
            print("Ny bedste tid");
        }
    }*/

}