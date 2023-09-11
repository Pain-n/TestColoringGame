using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalContext : MonoBehaviour
{
    public static GlobalContext Instance;

    [HideInInspector]
    public SoundManager SoundManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        SoundManager = new GameObject().AddComponent<SoundManager>();
        SoundManager.gameObject.name = "Soundmanager";
    }
}
