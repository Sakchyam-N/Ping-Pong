using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI aiScoreText;
    private int playerScore;
    private int aiScore;


    void Start()
    {
        playerScore = 0;
        aiScore = 0;
        UpdateScoreText();
    }

    public void IncrementScore()
    {
        playerScore++;
        UpdateScoreText();
    }

    public void IncrementAiScore()
    {
        aiScore++;
        UpdateScoreText();
    }

    public void ResetScore()
    {
        playerScore = 0;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = playerScore.ToString();
        aiScoreText.text = aiScore.ToString();
    }
}
