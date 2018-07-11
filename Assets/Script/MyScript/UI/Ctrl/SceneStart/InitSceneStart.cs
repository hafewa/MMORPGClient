using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneStart : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(LoadToLogOnScene());
    }

    IEnumerator LoadToLogOnScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneMgr.Instance.LoadLogOnScene();
    }
}
