using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    //configuration parameters
    [SerializeField] private AudioClip breakingClip;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] spriteBlocks;

    //cached parameters
    private Level level;
    private GameSession gameStatus;
    private float cameraPosZ;
    private int countHits=0;

    private void Start()
    {
        CountBreakableBlocks();
        gameStatus = FindObjectOfType<GameSession>();
        cameraPosZ = Camera.main.transform.position.z;
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag == "Breakable")
        {
            countHits++;
            int defenceLevel = spriteBlocks.Length + 1;

            if (countHits>=defenceLevel)
            {
                DestroyGameObject();
            }
            else
            {
                ShowNextSprite();
            }
        }
    }

    private void ShowNextSprite()
    {
        int spriteIndex = countHits - 1;

        if (spriteBlocks[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = spriteBlocks[spriteIndex];
        }
        else
        {
            Debug.LogError("Sprite is missing from the " + gameObject.name);
        }
    }

    private void DestroyGameObject()
    {
        DestroyBlock();
        gameStatus.AddToScore();
        TriggerSparklesVFX();
    }

    private void DestroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakingClip, new Vector3(transform.position.x, transform.position.y, cameraPosZ));
        level.CountDestroyedBlocks();
        Destroy(gameObject);        
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX,transform.position,transform.rotation);
        Destroy(sparkles,2f);
    }
}
