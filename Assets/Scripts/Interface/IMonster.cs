using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMonster
{
	public bool IsSelect 
	{
		get;
		set;
	}

	public Transform Transform
	{
		get;
		set;
	}

	public Vector3 Position
	{
		get;
		set;
	}

	public int HP
	{
		get;
		set;
	}

	public int Level
	{
		get;
		set;
	}
	public bool IsCapture
	{
		get;
		set;
	}

	/// <summary>
	/// ���ǰ� �������� üũ
	/// </summary>
	/// <param name="level">������ ���� ���� �������� üũ</param>
	bool CheckCapture(int level);

	/// <summary>
	/// ����
	/// </summary>
	void Capture();

	/// <summary>
	/// ���� ����
	/// </summary>
	void UnCapture();

	/// <summary>
	/// ���Ͱ� ���õ� ��
	/// </summary>
	void SelectMonster();

	/// <summary>
	/// ���Ͱ� ������ ��ҵ� ��
	/// </summary>
	void UnSelectMonster();
	
	/// <summary>
	/// ���Ͱ� �̵��� ��
	/// </summary>
	/// <returns></returns>
	bool MonsterMove(Vector3 targetVector);

	/// <summary>
	/// ���� ���콺 ��ư ��ų
	/// </summary>
	bool MouseLButtonSkill();

	/// <summary>
	/// ������ ���콺 ��ư ��ų
	/// </summary>
	bool MouseRButtonSkill();

	/// <summary>
	/// EŰ ��ų
	/// </summary>
	bool KeyESkill();

	/// <summary>
	/// RŰ ��ų
	/// </summary>
	bool KeyRSkill();

	/// <summary>
	/// ���� AI
	/// </summary>
	void MonsterAI();
}
