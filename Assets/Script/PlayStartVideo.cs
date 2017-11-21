using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayStartVideo : MonoBehaviour
{
    public string NextScene="MainMenu";

    private void Start()
    {
        StartCoroutine(GoMainMenu());
    }

    IEnumerator GoMainMenu()
    {
        yield return new WaitForSeconds(4.5f);
        SceneManager.LoadScene(NextScene);
        Debug.Log("影片結束");
    }
}
