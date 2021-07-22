using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static LevelManager levelManager;
    public static AudioManager audioManager;
    [HideInInspector]
    public bool switchSceneAnimIsDone;
    [SerializeField]
    Animator switchSceneAnimator;
    [SerializeField]
    string switchSceneAnimationName;

    void Awake()
    {
        audioManager = new AudioManager(this, () => {});
        levelManager = new LevelManager(this, switchSceneAnimator, switchSceneAnimationName, "AnimDone");
    }

    void AnimDone() { switchSceneAnimIsDone = true; }
    // Update is called once per frame
    void Update()
    {
        
    }
}
