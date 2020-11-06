using UnityEngine;

public class MissileTurret : MonoBehaviour {
    public PlayLevelController plc;
    public GameObject bulletPrefab;
    public Transform missileSpawnPoint;
    public GameObject turretTop;
    public GameObject turretBot;
    public float range;
    public float fireRate;

    private GameObject currentEnemy;
    private bool locked;
    // Start is called before the first frame update
    void Awake() {
        locked = false;
    }

    // Update is called once per frame
    void Update() {
        if(!locked) {
            foreach(GameObject enemy in plc.spawner.currentlyAliveEnemies) {
                if(Vector3.Distance(transform.position, enemy.transform.position) <= range) {
                    currentEnemy = enemy;
                    locked = true;
                    InvokeRepeating(nameof(FireMissile), 0, fireRate);
                }
            }
        } else {
            if(currentEnemy != null) {
                Vector3 botTarget = new Vector3(currentEnemy.transform.position.x, turretBot.transform.position.y, currentEnemy.transform.position.z);
                turretBot.transform.LookAt(botTarget);
                turretTop.transform.LookAt(currentEnemy.transform.position, Vector3.up);
            }
        }
    }

    private void FireMissile() {
        if(currentEnemy != null) {
            if(Vector3.Distance(transform.position, currentEnemy.transform.position) <= range) {
                GameObject missile = Instantiate(bulletPrefab, missileSpawnPoint.position, Quaternion.identity);
                missile.GetComponent<Missile>().target = currentEnemy;
            }
            else {
                CancelInvoke();
                locked = false;
            }
        }
        else {
            CancelInvoke();
            locked = false;
        }
    }
}
