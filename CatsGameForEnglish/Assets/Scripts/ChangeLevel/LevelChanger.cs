using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    private Animator anim;
    public int levelToLoad;

    public Vector3 position;
    public VectorValue playerStorage;

    public GameObject loadingScreen;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void FadeToLevel()
    {
        anim.SetTrigger("fade");
    }

    public void OnFadeComplete()
    {
        playerStorage.initialValue = position;
        SceneManager.LoadScene(levelToLoad);
        StartCoroutine(LoadingScreenOnFade());
    }

    IEnumerator LoadingScreenOnFade()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelToLoad);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }
}
