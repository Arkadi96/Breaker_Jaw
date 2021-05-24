using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    //configuration parameters
    [SerializeField] private int breakableBlocks;

    //cached parameters
    SceneLoader scene;

    private void Start()
    {
        scene = FindObjectOfType<SceneLoader>();
    }

    public void CountBlocks()
    {
        breakableBlocks++;
    }

    public void CountDestroyedBlocks()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            scene.LoadNextScene();
        }
    }
}
