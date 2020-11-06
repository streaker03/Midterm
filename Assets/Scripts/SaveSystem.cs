using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {
    public static void SaveMap(MapGenerator mg, string name) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Levels/" + name + ".layout";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        MapLayoutData data = new MapLayoutData(mg);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static MapLayoutData LoadMap(string name) {
        string path = "";
        if(name == "PlayLevel1" || name == "PlayLevel2" || name == "PlayLevel3") {
            path = Application.dataPath + "/StreamingAssets/" + name + ".layout";
        } else {
            path = Application.persistentDataPath + "/Levels/" + name + ".layout";
        }
        if(File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            MapLayoutData data = formatter.Deserialize(stream) as MapLayoutData;
            stream.Close();

            return data;
        } else {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
