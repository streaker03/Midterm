using UnityEngine;

public class Missile : MonoBehaviour {
    public GameObject target;
    public float speed;
    public float damage;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if(target != null) {
            transform.LookAt(target.transform.position, transform.forward);
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        } else {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.CompareTag("Enemy")) {
            EnemyInitializer ei = col.gameObject.GetComponent<EnemyInitializer>();
            if(ei.health - damage <= 0) {
                Destroy(col.gameObject);
            } else {
                ei.health -= damage;
            }
            Destroy(gameObject);
        }
    }
}
