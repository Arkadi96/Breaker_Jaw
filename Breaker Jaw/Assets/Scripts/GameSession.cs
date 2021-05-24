using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    //configuration parameters
    [Range(0.1f,10f)][SerializeField] private float gameSpeed=1f;
    [SerializeField] private int pointsPerBlockDestroyed=5;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private bool isAutoPlayEnabled;

    //state variables
    [SerializeField] private int currentScore=0;

    //singleton 
    private void Awake()
    {
        int countGameStatus = FindObjectsOfType<GameSession>().Length;
        if (countGameStatus > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    //update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
    }

    public void DestroyItself()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
