using UnityEngine;

[System.Serializable]
public class MapLayoutData {
    public bool[,] hills;
    public int width;
    public int height;
    public int size;
    public float[] entrance;
    public float[] exit;

    public MapLayoutData(MapGenerator mg) {
        width = mg.width;
        height = mg.height;
        hills = new bool[width, height];
        size = mg.size;
        entrance = new float[5];
        exit = new float[4];
        Transform entTransform = mg.entrance.transform;
        entrance[0] = entTransform.position.x;
        entrance[1] = entTransform.position.y;
        entrance[2] = entTransform.position.z;
        entrance[3] = entTransform.eulerAngles.y;
        Transform exitTransform = mg.exit.transform;
        exit[0] = exitTransform.position.x;
        exit[1] = exitTransform.position.y;
        exit[2] = exitTransform.position.z;
        exit[3] = exitTransform.eulerAngles.y;
        for(int x = 0; x < width; x++) {
            for(int y = 0; y < height; y++) {
                hills[x, y] = mg.map[x, y].IsAHill();
            }
        }
    }
}
