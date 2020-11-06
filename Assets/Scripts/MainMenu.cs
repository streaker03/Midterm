using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject titleMenu;
    public GameObject levelSelect;
    public TMP_InputField levelName;
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Play() {
        titleMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void MapMaker() {
        SceneManager.LoadScene(2);
    }

    public void Quit() {
        Application.Quit();
    }

    public void PlayLevel() {
        if(levelName.text == "PlayLevel1" || levelName.text == "PlayLevel2" || levelName.text == "PlayLevel3") {
            PersistentData.setLoadLevel(levelName.text);
            SceneManager.LoadScene(1);
        } else {
            if(System.IO.File.Exists(Application.persistentDataPath + "/Levels/" + levelName.text + ".layout")) {
                PersistentData.setLoadLevel(levelName.text);
                SceneManager.LoadScene(1);
            }
        }
    }

    public void Back() {
        levelSelect.SetActive(false);
        titleMenu.SetActive(true);
    }

    public void PlayLevel1() {
        levelName.text = "PlayLevel1";
        PlayLevel();
    }

    public void PlayLevel2() {
        levelName.text = "PlayLevel2";
        PlayLevel();
    }

    public void PlayLevel3() {
        levelName.text = "PlayLevel3";
        PlayLevel();
    }
}
