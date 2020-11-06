using UnityEngine;

public class ShockTurret : MonoBehaviour {
    public PlayLevelController plc;
    public float range;
    public float damagePerSecond;
    public GameObject shockEffect;
    
    
    // Start is called before the first frame update
    void Awake() {
        
    }

    // Update is called once per frame
    void Update() {
        foreach(GameObject enemy in plc.spawner.currentlyAliveEnemies) {
            if(Vector3.Distance(transform.position, enemy.transform.position) <= range) {
                EnemyInitializer ei = enemy.GetComponent<EnemyInitializer>();
                if(ei.health - (damagePerSecond * Time.deltaTime) <= 0) {
                    Destroy(enemy);
                } else {
                    ei.health -= (damagePerSecond * Time.deltaTime);
                    bool check = false;
                    foreach(Transform child in enemy.transform) {
                        if(child.gameObject.CompareTag("Shocked")) {
                            check = true;
                        }
                    }
                    if(!check) {
                        Instantiate(shockEffect, enemy.transform);
                    }
                }
            } else {
                bool check = false;
                GameObject toRemove = null;
                foreach(Transform child in enemy.transform) {
                    if(child.gameObject.CompareTag("Shocked")) {
                        check = true;
                        toRemove = child.gameObject;
                    }
                }
                if(check) {
                    Destroy(toRemove);
                }
            }
        }
    }
}
