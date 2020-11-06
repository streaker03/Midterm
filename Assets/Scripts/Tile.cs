using UnityEngine;
using UnityEngine.AI;

public class Tile : MonoBehaviour{
    private int[] position;
    private bool isAHill;
    private Color groundColor;
    private Color hillColor;
    public Color highlightColor;
    private Renderer rend;
    private int id;
    private NavMeshModifier navModifier;
    private bool highlighting;

    public float highlightSpeed;

    void Awake() {
        rend = GetComponent<Renderer>();
        navModifier = GetComponent<NavMeshModifier>();
        highlighting = false;
    }

    private void OnMouseOver() {
        if(rend.material.color == hillColor || rend.material.color == groundColor) {
            highlighting = true;
        }

        if(highlighting && rend.material.color != highlightColor) {
            if(isAHill) {
                rend.material.color = Color.Lerp(rend.material.color, highlightColor, highlightSpeed);
            } else {
                rend.material.color = Color.Lerp(rend.material.color, highlightColor, highlightSpeed);
            }
        } else if(highlighting && rend.material.color == highlightColor) {
            highlighting = false;
        }

        if(isAHill) {
            if(!highlighting && rend.material.color != hillColor) {
                rend.material.color = Color.Lerp(rend.material.color, hillColor, highlightSpeed);
            }
        } else {
            if(!highlighting && rend.material.color != groundColor) {
                rend.material.color = Color.Lerp(rend.material.color, groundColor, highlightSpeed);
            }
        }
    }

    private void OnMouseExit() {
        if(isAHill) {
            rend.material.color = hillColor;
        } else {
            rend.material.color = groundColor;
        }
    }

    public Tile() {
        position = new int[2];
    }

    public void SetPosition(int posX, int posY) {
        position[0] = posX;
        position[1] = posY;
        transform.position = new Vector3(posX, transform.position.y,posY);
    }

    public void SetIsAHill(bool hill) {
        isAHill = hill;
        Vector3 pos = transform.position;
        if(hill) {
            transform.position = new Vector3(pos.x, transform.localScale.y/2, pos.z);
            rend.material.color = hillColor;
            navModifier.area = 1;
        } else {
            transform.position = new Vector3(pos.x, 0, pos.z);
            rend.material.color = groundColor;
            navModifier.area = 0;
        }
    }

    public int[] GetPosition() {
        return position;
    }

    public bool IsAHill() {
        return isAHill;
    }

    public void SetColors(Color ground, Color hill) {
        groundColor = ground;
        hillColor = hill;
    }
}
