using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : MonsterBase
{
	public override void Start()
	{
		base.Start();
		_name = "����";
		_description = "�¾�鼭���� ����ִ� ��ü��, �׵��� ���հ��� ������̴�. ����� �� �� ���� �������� �������� ������ �ؿ´�";
	}

	public override bool KeyESkill()
	{
		if (CheckCoolTimeE())
		{
			//ChangeState(MonsterState.Attack);
			//_attackState = AttackState.E;
			if (_targetCharacter == null)
			{
				NoticeManager.Notice("��ǥ ����� �����ϴ�");
			}
			else
			{
				_targetCharacter.transform.position = transform.position;
			}
			_coolTimeE = 0;
			return true;
		}
		return false;
	}

	public override bool KeyRSkill()
	{
		if (CheckCoolTimeR())
		{
			//ChangeState(MonsterState.Attack);
			//_attackState = AttackState.R;
			_coolTimeR = 0;
			return true;
		}
		return false;
	}

	public override bool MouseLButtonSkill()
	{
		if (CheckCoolTimeMLB())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.MLB;
			_coolTimeMLB = 0;
			return true;
		}
		return false;
	}

	public override bool MouseRButtonSkill()
	{
		if (CheckCoolTimeMRB())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.MRB;
			HP = MaxHP;
			_coolTimeMRB = 0;
			return true;
		}
		return false;
	}
	public override void Delete()
	{
		ItemPoolFind.RegisterObject(this);
		base.Delete();
	}
}
