using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    //configuration parameters
    [SerializeField] private float screenWidthUnits = 16f;
    [SerializeField] private float minX = 0.5f;
    [SerializeField] private float maxX = 15.5f;

    //Cached references
    GameSession gameSession;
    Ball ball;

    //for initialization
    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        ball = FindObjectOfType<Ball>();
    }

    //Update is called once per frame
    private void Update()
    {
        Vector2 trampolinePos = new Vector2(transform.position.x, transform.position.y);
        trampolinePos.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = trampolinePos;
    }

    private float GetXPos()
    {
        if (gameSession.IsAutoPlayEnabled())
        {
            return ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthUnits;
        }
    }
}
