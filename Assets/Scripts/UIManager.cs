using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private void Start() { GameManager.OnUpdatePoints += UpdateUi; }
    private void UpdateUi(int score, int highScore)
    {
        if (score >= highScore)
        {
            scoreText.text = score.ToString();
            scoreText.color = Color.green;
        }
        else 
        {
            scoreText.text = score.ToString() + "/" + highScore.ToString();
            scoreText.color = Color.red;
        }
    }
}
