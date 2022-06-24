using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
	public Transform Transform
	{
		get;
	}

	/// <summary>
	/// ��ȣ�ۿ� �̸�
	/// </summary>
	public string InteractionName
	{
		get;
	}

	/// <summary>
	/// ��ȣ�ۿ� ������ �̸�
	/// </summary>
	public string InteractionActionName
	{
		get;
	}

	/// <summary>
	/// ��ȣ�ۿ�
	/// </summary>
	/// <param name="player"></param>
	void Interaction(Player player);
}
