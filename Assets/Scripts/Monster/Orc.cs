using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonsterBase
{
	public override void Start()
	{
		base.Start();
		_name = "��ũ";
		_description = "�ΰ����� Ŀ�ٶ� ü�ݰ� �ٷ��� ���� ���ͷ� ��ġ�� ���� ��ø�ϸ� �ʷϻ� �Ǻθ� �̿��Ͽ� Ǯ �ӿ��� ��ɰ��� �ٰ����� ���� ��ٸ���";
		_canSkillMLB = true;
		_canSkillMRB = true;
		_canSkillE = true;
		_canSkillR = true;
	}

	public override bool KeyESkill()
	{
		if (CheckCoolTimeE())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.E;
			_coolTimeE = 0;
			return true;
		}
		return false;
	}

	public override bool KeyRSkill()
	{
		if (CheckCoolTimeR())
		{
			ChangeState(MonsterState.Attack);
			_attackState = AttackState.R;
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
