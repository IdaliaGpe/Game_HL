using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRandomScene : MonoBehaviour{

    public int levelGenerate;

    public void LoadTheLevel()
    {
        levelGenerate = Random.Range(0,10);
        SceneManager.LoadScene(levelGenerate);
    }
}
