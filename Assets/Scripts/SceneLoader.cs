using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("OptionScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("NoticeScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("OverWorldUIScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("ShopScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("BattleUIScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
    }
}
