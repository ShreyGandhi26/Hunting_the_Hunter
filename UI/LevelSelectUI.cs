using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;



public class LevelSelectUI : MonoBehaviour
{
    public TextMeshProUGUI starText;
    private void Start()
    {
        starText.text = "X " + LevelCompleteUI.Starscount;
    }
    public void level1()
    {
        SceneManager.LoadScene("TutorialLevel");
    }
    public void level2()
    {
        SceneManager.LoadScene("Level1");
    }
    public void level3()
    {
        SceneManager.LoadScene("Level_2");
    }
    public void level4()
    {
        SceneManager.LoadScene("Level3");
    }
    public void level5()
    {
        SceneManager.LoadScene("Level4");
    }
    public void level6()
    {
        SceneManager.LoadScene("Level5");
    }
    public void level7()
    {
        SceneManager.LoadScene("Level6");
    }
    public void level8()
    {
        SceneManager.LoadScene("Level7");
    }
    public void menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
