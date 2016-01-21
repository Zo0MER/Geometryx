using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {

    public void OpenLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
