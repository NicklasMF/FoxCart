using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public delegate void GameStarted();
    public event GameStarted startGame;

    public bool raceStarted = false;
    public float time = 0f;

    [Header("InRace")]
    [SerializeField] GameObject TimeContent;
    [SerializeField] GameObject TimePrefab;

    [SerializeField] string[] CarNumbers;
    [SerializeField] GameObject StartPosition;
    [SerializeField] GameObject CarPrefab;
    List<GameObject> Cars = new List<GameObject>();

      // Use this for initialization
    void Start () {
        CreateCars();
	}

    void Update() {
        if (raceStarted) {
            time += Time.deltaTime;
        }
    }

    public void StartGame() {
        raceStarted = true;
        print("StartRace");
        startGame();
    }

    void CreateCars() {
        List<Transform> StartPositions = new List<Transform>();
        foreach(Transform child in StartPosition.transform) {
            StartPositions.Add(child);
        }

        for(int i = 0; i < CarNumbers.Length; i++) {
            GameObject car = Instantiate(CarPrefab);
            Cars.Add(car);
            car.transform.position = StartPositions[i].position;
            car.transform.rotation = StartPositions[i].rotation;
        }

        UpdateHighscore();
    }

    void UpdateHighscore() {
        foreach(Transform child in TimeContent.transform) {
            Destroy(child.gameObject);
        }

        int i = 0;
        foreach(GameObject car in Cars) {
            GameObject time = Instantiate(TimePrefab, TimeContent.transform);
            time.GetComponent<UIHighscoreTime>().name.text = CarNumbers[i];
            i++;
            time.GetComponent<UIHighscoreTime>().position.text = i.ToString();
            time.GetComponent<UIHighscoreTime>().time.text = "0:00:00";
        }

    }

}
