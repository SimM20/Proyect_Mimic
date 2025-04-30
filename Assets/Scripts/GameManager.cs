using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private GameObject ballSpawner;
    private BallController actualBall;
    private int score = 0;
    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void OnEnable()
    {
        actualBall = ballSpawner.GetComponent<BallSpawner>().SpawnBall();
        actualBall.OnOutOfBounds += OnDefeat;
    }

    public void OnDefeat() 
    { 
        /*Aca iria todo lo que tiene que ver con la derrota en si*/ 
        actualBall.OnOutOfBounds -= OnDefeat;
        SaveSystem.SaveScore(score);
        SceneManagerUtils.LoadActiveScene(); 
    }

    public void UpdatePoints()
    {
        score += 1;
        //Aviso al UI Manager? (implementable cuando veamos UI)
    }

    private void OnDisable() {  actualBall.OnOutOfBounds -= OnDefeat; }
}
