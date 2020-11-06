using UnityEngine;

public class Load : MonoBehaviour {
    public MapGenerator mg;

    void Start() {
        LoadLayout();
    }
    
    private void LoadLayout() {
        mg.ClearMap();
        MapLayoutData data = SaveSystem.LoadMap(PersistentData.getLoadLevel());
        mg.height = data.height;
        mg.width = data.width;
        mg.size = data.size;
        mg.InitializeMap();
        mg.InitializeHills(data.hills);
        mg.InitializePortals(data.entrance, data.exit);
        mg.navSurface.BuildNavMesh();
    }
}
