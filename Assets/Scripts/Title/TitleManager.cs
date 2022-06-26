using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	/// <summary>
	/// 게임 시작
	/// </summary>
	public void GameStart()
	{
		LoadingManager.LoadScene("InGame");
	}

	/// <summary>
	/// 게임 종료
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
