using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject[] PauseUI;

    private void Awake() {
        Cursor.visible = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            foreach (var item in PauseUI) {
                if (item == null)
                    return;
                item.SetActive(true);
            }
            Time.timeScale = 0;
        }
    }
    public void Continue() {
        Debug.Log("get!");
        Time.timeScale = 1;
        foreach (var item in PauseUI) {
                item.SetActive(false);
            }
    }
    public void LoadNewScene(int index) {
        SceneManager.LoadScene(index);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
