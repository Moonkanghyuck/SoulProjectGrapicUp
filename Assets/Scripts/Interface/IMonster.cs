using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMonster
{
	public bool IsSelect 
	{
		get;
		set;
	}
	public Sprite Sprite
	{
		get;
		set;
	}
	public Transform Transform
	{
		get;
		set;
	}
	public GameObject GameObject
	{
		get;
		set;
	}
	public Vector3 Position
	{
		get;
		set;
	}
	public string Name
	{
		get;
		set;
	}
	public string Description
	{
		get;
		set;
	}
	public int HP
	{
		get;
		set;
	}
	public int MaxHP
	{
		get;
		set;
	}
	public int Level
	{
		get;
		set;
	}

	public int ATK
	{
		get;
		set;
	}
	public int SPD
	{
		get;
		set;
	}
	public int DEF
	{
		get;
		set;
	}
	public int EXP
	{
		get;
		set;
	}
	public bool IsCapture
	{
		get;
		set;
	}
	public float CoolTimeMLB
	{
		get;
		set;
	}
	public float CoolTimeMRB
	{
		get;
		set;
	}
	public float CoolTimeE
	{
		get;
		set;
	}
	public float CoolTimeR
	{
		get;
		set;
	}

	//���� �ʱ�ȭ
	void Init();

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
	void MonsterMove(Vector3 targetVector, bool isTarggeting);

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
	/// MLB ��ų�� ����� �� �ִ���
	/// </summary>
	/// <returns></returns>
	bool CheckCanMLB();
	/// <summary>
	/// MRB ��ų�� ����� �� �ִ���
	/// </summary>
	/// <returns></returns>
	bool CheckCanMRB();
	/// <summary>
	/// E ��ų�� ����� �� �ִ���
	/// </summary>
	/// <returns></returns>
	bool CheckCanE();
	/// <summary>
	/// R ��ų�� ����� �� �ִ���
	/// </summary>
	/// <returns></returns>
	bool CheckCanR();
	/// <summary>
	/// MLB ��ų�� ��Ÿ���� ��������
	/// </summary>
	/// <returns></returns>
	bool CheckCoolTimeMLB();
	/// <summary>
	/// MRB ��ų�� ��Ÿ���� ��������
	/// </summary>
	/// <returns></returns>
	bool CheckCoolTimeMRB();
	/// <summary>
	/// E ��ų�� ��Ÿ���� ��������
	/// </summary>
	/// <returns></returns>
	bool CheckCoolTimeE();
	/// <summary>
	/// R ��ų�� ��Ÿ���� ��������
	/// </summary>
	/// <returns></returns>
	bool CheckCoolTimeR();
	/// <summary>
	/// ���� �ִ� �������� üũ�Ѵ�
	/// </summary>
	/// <returns></returns>
	bool CheckMaxLevel();
	/// <summary>
	/// ���� ������
	/// </summary>
	void LevelUP();

	/// <summary>
	/// ����ġ ����
	/// </summary>
	/// <param name="exp"></param>
	void SetEXP(int exp);

	/// <summary>
	/// ����ġ �߰�
	/// </summary>
	/// <param name="exp"></param>
	void AddEXP(int exp);

	/// <summary>
	/// ���� ���� �Լ�
	/// </summary>
	void Delete();
}