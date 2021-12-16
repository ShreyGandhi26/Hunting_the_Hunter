using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static float Score;
    private Scene currentScene;
    public string sceneName;
    public TextMeshProUGUI text;
    //  public TextMeshProUGUI Startext;

    public static GameManager Instance;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (sceneName != "Menu")
        {
            getScore(Score);
            Score = 0;
        }
        if (sceneName != "LevelCompleteScreen")
        {
            getScore(Score);
            Score = 0;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (Instance == null)
        {
            Instance = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
    }



    // Update is called once per frame
    void Update()
    {

    }


    public void Play()
    {
        SceneManager.LoadScene("TutorialLevel");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void getScore(float _score)
    {
        text.text = "Score : " + _score;
    }
    public void LevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
