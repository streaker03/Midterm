using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayLevelController : MonoBehaviour {
    public MapGenerator mg;
    public GameObject enemyPrefab;
    public Turret[] turretTypes;
    public Enemy[] enemyTypes;
    public float timeBetweenWaves;
    public float timeBetweenSpawns;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI waveText;
    public Spawner spawner;
    public GameObject exitMenu;
    public GameObject gameOverMenu;

    private int wave;
    private int score;
    private int money;
    private int lives;
    private int enemies;
    private bool placingMissileTurret;
    private bool placingLaserTurret;
    private bool placingShockTurret;
    private Camera cam;
    private Turret currentTurretType;
    
    void Start() {
        wave = 0;
        score = 0;
        money = 0;
        ChangeMoney(250);
        lives = 10;
        mg.entrance.GetComponent<Portal>().plc = this;
        mg.exit.GetComponent<Portal>().plc = this;
        spawner = mg.entrance.AddComponent<Spawner>();
        spawner.timeBetweenSpawning = timeBetweenSpawns;
        spawner.plc = this;
        cam = Camera.main;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            exitMenu.SetActive(true);
        }
        if(Input.GetMouseButtonDown(0)) {
            if(placingLaserTurret || placingMissileTurret || placingShockTurret) {
                RaycastHit hit;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit, Mathf.Infinity)) {
                    Tile selected = hit.transform.gameObject.GetComponent<Tile>();
                    if(selected.IsAHill()) {
                        if(money - currentTurretType.cost >= 0) {
                            Transform tileTransform = selected.transform;
                            Vector3 spawnPos = new Vector3(tileTransform.position.x, tileTransform.position.y + (tileTransform.localScale.x / 2), tileTransform.position.z);
                            GameObject turret = Instantiate(currentTurretType.prefab, spawnPos, Quaternion.identity);
                            if(placingMissileTurret) {
                                turret.GetComponent<MissileTurret>().plc = this;
                            } else if(placingLaserTurret) {
                                turret.GetComponent<LaserTurret>().plc = this;
                            } else if(placingShockTurret) {
                                turret.GetComponent<ShockTurret>().plc = this;
                            }
                            ChangeMoney(currentTurretType.cost * -1);
                            placingLaserTurret = false;
                            placingMissileTurret = false;
                            placingShockTurret = false;
                        }
                    }
                }
            }
        }
    }

    private void GameOver() {
        gameOverMenu.SetActive(true);
    }

    public void ChangeWave(int amount) {
        wave += amount;
        spawner.CreateNextWave();
        StartCoroutine(Timer(timeBetweenWaves));
    }

    public void ChangeScore(int amount) {
        score += amount;
    }

    public void ChangeMoney(int amount) {
        money += amount;
        moneyText.text = "Money: " + money;
    }

    public void ChangeLives(int amount) {
        lives += amount;
        livesText.text = "Lives: " + lives;
        if(lives == 0) {
            GameOver();
        }
    }

    public void ChangeEnemies(int amount) {
        enemies += amount;
        if(enemies == 0) {
            ChangeWave(1);
        }
    }
    
    public int GetWave() {
        return wave;
    }

    public int GetScore() {
        return score;
    }

    public int GetMoney() {
        return money;
    }

    public int GetLives() {
        return lives;
    }

    public int GetEnemies() {
        return enemies;
    }

    private IEnumerator Timer(float seconds) {
        yield return new WaitForSecondsRealtime(seconds);
        spawner.StartWave();
        waveText.text = "Wave: " + wave;
    }

    public void SetPlacingMissileTurret() {
        if(placingMissileTurret) {
            placingMissileTurret = false;
        } else {
            placingMissileTurret = true;
            currentTurretType = turretTypes[0];
            placingShockTurret = false;
            placingLaserTurret = false;
        }
    }
    
    public void SetPlacingLaserTurret() {
        if(placingLaserTurret) {
            placingLaserTurret = false;
        } else {
            placingMissileTurret = false;
            placingShockTurret = false;
            placingLaserTurret = true;
            currentTurretType = turretTypes[1];
        }
    }
    
    public void SetPlacingShockTurret() {
        if(placingShockTurret) {
            placingShockTurret = false;
        } else {
            placingMissileTurret = false;
            placingShockTurret = true;
            currentTurretType = turretTypes[2];
            placingLaserTurret = false;
        }
    }

    public void Resume() {
        exitMenu.SetActive(false);
    }

    public void Exit() {
        SceneManager.LoadScene(0);
    }

    public void Restart() {
        SceneManager.LoadScene(1);
    }
}
