using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public PlayLevelController plc;
    public float timeBetweenSpawning;
    public List<GameObject> currentlyAliveEnemies;

    private List<Enemy> waveEnemies;
    // Start is called before the first frame update
    void Start() {
        currentlyAliveEnemies = new List<GameObject>();
        waveEnemies = new List<Enemy>();
        plc.ChangeWave(1);
    }

    public void CreateNextWave() {
        waveEnemies.Clear();
        int waveCost = plc.GetWave();
        while(waveCost > 0) {
            for(int x = plc.enemyTypes.Length-1; x >= 0; x--) {
                if(waveCost - plc.enemyTypes[x].cost >= 0) {
                    waveCost -= plc.enemyTypes[x].cost;
                    waveEnemies.Insert(0, plc.enemyTypes[x]);
                }
            }
        }
        plc.ChangeEnemies(waveEnemies.Count);
    }

    public void StartWave() {
        InvokeRepeating(nameof(SpawnEnemy), 0, timeBetweenSpawning);
    }

    private void SpawnEnemy() {
        GameObject currentEnemy = Instantiate(plc.enemyPrefab, transform.position, Quaternion.identity);
        EnemyInitializer ei = currentEnemy.GetComponent<EnemyInitializer>();
        ei.enemyType = waveEnemies[0];
        ei.plc = plc;
        ei.Initialize();
        waveEnemies.RemoveAt(0);
        if(waveEnemies.Count == 0) {
            CancelInvoke();
        }
        currentlyAliveEnemies.Add(currentEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
