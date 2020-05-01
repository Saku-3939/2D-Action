using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private LifeManager lifeManager;
    public GameObject lifeController;
    public GameObject gameOverScreen;

    public Text scoreText;
    public Text resultText;
    public int score;
    public int life;
    public static int resultScore;
    public bool isPlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        lifeManager = lifeController.GetComponent<LifeManager>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        life = lifeManager.lifeCount;
        scoreText.text = "スコア : " + score.ToString();
        if (life <= 0)
        {
            isPlaying = false;
            resultScore = score;
            resultText.text = "今回のスコア ; " + resultScore.ToString();
            gameOverScreen.SetActive(true);
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
