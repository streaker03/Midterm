using UnityEngine;

public class LaserTurret : MonoBehaviour {
    public PlayLevelController plc;
    public float range;
    public float damagePerSecond;
    public GameObject top;

    private LineRenderer lr;
    private GameObject currentEnemy;
    private EnemyInitializer ei;
    private bool locked;
    // Start is called before the first frame update
    void Awake() {
        locked = false;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, top.transform.position);
        lr.SetPosition(1, top.transform.position);
    }

    // Update is called once per frame
    void Update() {
        if(!locked) {
            foreach(GameObject enemy in plc.spawner.currentlyAliveEnemies) {
                if(Vector3.Distance(transform.position, enemy.transform.position) <= range) {
                    currentEnemy = enemy;
                    locked = true;
                    ei = currentEnemy.GetComponent<EnemyInitializer>();
                }
            }
        } else {
            if(currentEnemy != null) {
                if(Vector3.Distance(transform.position, currentEnemy.transform.position) <= range) {
                    lr.SetPosition(1, currentEnemy.transform.position);
                    if(ei.health - (damagePerSecond * Time.deltaTime) > 0) {
                        ei.health -= (damagePerSecond * Time.deltaTime);
                    } else {
                        Destroy(currentEnemy);
                        lr.SetPosition(1, top.transform.position);
                    }
                    
                } else {
                    lr.SetPosition(1, top.transform.position);
                    locked = false;
                }
            } else {
                lr.SetPosition(1, top.transform.position);
                locked = false;
            }
        }
    }
}
