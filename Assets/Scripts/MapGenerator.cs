using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour {
    public Tile[,] map;
    public int width;
    public int height;
    public int size;
    public GameObject tilePrefab;
    public GameObject portalPrefab;
    public Color floor;
    public Color hill;
    public float topBound;
    public float leftBound;
    public float rightBound;
    public float bottomBound;
    public GameObject entrance;
    public GameObject exit;
    public NavMeshSurface navSurface;
    // Start is called before the first frame update
    void Start() {
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            InitializeMap();
        }
    }

    // Update is called once per frame
    void Update() {
    }

    private void InitializeColors() {
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                map[x,y].SetIsAHill(false);
            }
        }
    }

    public void InitializeMap() {
        map = new Tile[width, height];
        topBound = 0 - (size / 2);
        leftBound = 0 - (size / 2);
        rightBound = (width * size) + (size / 2);
        bottomBound = (height * size) + (size / 2);
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                GameObject current = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
                current.name = "Tile[" + x + "," + y + "]";
                current.transform.localScale = new Vector3(size, size, size);
                current.transform.SetParent(transform);
                map[x,y] = current.GetComponent<Tile>();
                map[x,y].SetPosition(x*size, y*size);
                map[x,y].SetColors(floor, hill);
            }
        }
        InitializeColors();
    }

    public void InitializeHills(bool[,] hills) {
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                map[x,y].SetIsAHill(hills[x,y]);
            }
        }
    }

    public void InitializePortals(float[] entrance, float[] exit) {
        this.entrance = Instantiate(portalPrefab, Vector3.zero, Quaternion.identity);
        Transform entTransform = this.entrance.transform;
        entTransform.position = new Vector3(entrance[0], entrance[1], entrance[2]);
        entTransform.eulerAngles = new Vector3(0, entrance[3], 0);
        entTransform.SetParent(transform);
        this.entrance.GetComponent<Portal>().setSide(true);
        this.exit = Instantiate(portalPrefab, Vector3.zero, Quaternion.identity);
        Transform exitTransform = this.exit.transform;
        exitTransform.position = new Vector3(exit[0], exit[1], exit[2]);
        exitTransform.eulerAngles = new Vector3(0, exit[3], 0);
        exitTransform.SetParent(transform);
        this.exit.GetComponent<Portal>().setSide(false);
    }

    public void ClearMap() {
        map = new Tile[width, height];
        for(int i = 0; i < transform.childCount; i++) {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
