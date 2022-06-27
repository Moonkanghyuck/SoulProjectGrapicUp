using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TitleManager : MonoBehaviour
{
	/// <summary>
	/// ���� ����
	/// </summary>
	public void GameStart()
	{
		LoadingManager.LoadScene("InGame");
	}

	/// <summary>
	/// ���� ����
	/// </summary>
	public void GameEnd()
	{

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
	}
}
