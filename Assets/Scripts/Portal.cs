using UnityEngine;

public class Portal : MonoBehaviour {
    private bool side;
    private Renderer rend;

    public Color entranceColor;
    public Color exitColor;
    public PlayLevelController plc;
    
    void Awake() {
        rend = GetComponent<Renderer>();
    }

    void OnCollisionEnter(Collision col) {
        if(isTheExit()) {
            if(col.gameObject.CompareTag("Enemy")) {
                plc.ChangeLives(-1);
                Destroy(col.gameObject);
            }
        }
    }

    public bool isTheEntrance() {
        return side;
    }

    public bool isTheExit() {
        return !side;
    }

    public void setSide(bool side) {
        this.side = side;
        if(side) {
            rend.material.color = entranceColor;
        } else {
            rend.material.color = exitColor;
        }
    }

    
}
