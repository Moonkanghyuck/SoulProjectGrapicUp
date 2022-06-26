using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : MonsterBase
{
	public override void Start()
	{
		base.Start();
		_name = "구울";
		_description = "태어나면서부터 살아있는 시체로, 그들의 먹잇감은 사람뿐이다. 사람이 낼 수 없는 동작으로 위협적인 공격을 해온다";
	}

	public override bool KeyESkill()
	{
		if (CheckCoolTimeE())
		{
			//ChangeState(MonsterState.Attack);
			//_attackState = AttackState.E;
			if (_targetCharacter == null)
			{
				NoticeManager.Notice("목표 대상이 없습니다");
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
