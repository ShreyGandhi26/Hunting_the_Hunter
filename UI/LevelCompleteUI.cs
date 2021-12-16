using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelCompleteUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI starText;
    public Image Stars;
    public static float Starscount;
    public float Star1Score;
    public float Star2Score;
    public float Star3Score;
    public string sceneName;

    private void Awake()
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        Scene name = SceneManager.GetActiveScene();
        string curname = name.name;
        if (curname == "Menu")
        {
            starText.text = "X " + Starscount;
        }
        else
        {
            text.text = "Your Score : " + GameManager.Score;
            Stars.fillAmount = 0;
            if (GameManager.Score > Star1Score && GameManager.Score < Star2Score)
            {
                Starscount += 1;
                Stars.fillAmount = .3333f;
                starText.text = "X " + Starscount;
                GameManager.Score = 0;
            }
            else if (GameManager.Score >= Star2Score && GameManager.Score < Star3Score)
            {
                Starscount += 2;
                Stars.fillAmount = .6666f;
                starText.text = "X " + Starscount;
                GameManager.Score = 0;
            }
            else if (GameManager.Score >= Star3Score)
            {
                Starscount += 3;
                Stars.fillAmount = 1;
                starText.text = "X " + Starscount;
                GameManager.Score = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Next()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
