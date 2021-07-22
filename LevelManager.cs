using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// FIXME SCENE LOGIC
public class LevelManager
{
    public static AnimatorWatcher switchAnimationWatcher;
    GameManager gameManager;
    Animator switchSceneAnimator;
    string switchSceneAnimationName;
    Coroutine load;
    public struct SceneProperties
    {
        public int id;
        public string sceneName;

        public SceneProperties(int id_, string sceneName_)
        {
            id = id_;
            sceneName = sceneName_;
        }
    }

    public LevelManager(GameManager gameManager_, Animator switchSceneAnimator_, string switchSceneAnimationName_, string endAnimFuncName)
    {
        gameManager = gameManager_;
        switchSceneAnimationName = switchSceneAnimationName_;
        switchSceneAnimator = switchSceneAnimator_;
        switchAnimationWatcher = new AnimatorWatcher(switchSceneAnimator, switchSceneAnimationName, endAnimFuncName);
    }

    SceneProperties GetInstance(int id, string sceneName) { return new SceneProperties(id, sceneName); }
    public void LoadNextScene() { RequestLoad(GetInstance(SceneManager.GetActiveScene().buildIndex + 1, null)); }
    public void ReloadScene() { RequestLoad(GetInstance(SceneManager.GetActiveScene().buildIndex, null)); }
    public void LoadSceneWithName(string sceneName) { RequestLoad(GetInstance(0, sceneName)); }
    void RequestLoad(SceneProperties scene)
    {
        if (load != null) gameManager.StopCoroutine(load);
        load = gameManager.StartCoroutine(Load(scene));
    }

    IEnumerator Load(SceneProperties scene)
    {
        switchSceneAnimator.SetTrigger("Switch");
        while (!gameManager.switchSceneAnimIsDone) yield return null;
        gameManager.switchSceneAnimIsDone = false;

        if (scene.sceneName != null) SceneManager.LoadScene(scene.sceneName);
        else SceneManager.LoadScene(scene.id);
    }
}
