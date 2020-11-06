using UnityEngine;
using UnityEngine.AI;

public class EnemyInitializer : MonoBehaviour {
    public Enemy enemyType;
    public NavMeshAgent agent;
    public PlayLevelController plc;
    public float health;
    // Start is called before the first frame update
    void Start() {
    }

    public void Initialize() {
        //GetComponent<MeshFilter>().mesh = enemyType.mesh;
        //GetComponent<MeshCollider>().sharedMesh = enemyType.mesh;
        //MeshRenderer mr = GetComponent<MeshRenderer>();
        //MeshRenderer desired = enemyType.model.GetComponent<MeshRenderer>();
        //mr.sharedMaterials = desired.sharedMaterials;
        GameObject current = Instantiate(enemyType.model, transform);
        MeshCollider mc = current.AddComponent<MeshCollider>();
        mc.convex = true;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = enemyType.speed;
        agent.SetDestination(plc.mg.exit.transform.position);
        gameObject.tag = "Enemy";
        health = enemyType.maxHealth;
    }

    private void OnDestroy() {
        plc.ChangeScore(enemyType.points);
        plc.ChangeMoney(enemyType.money);
        plc.ChangeEnemies(-1);
        plc.spawner.currentlyAliveEnemies.Remove(gameObject);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
