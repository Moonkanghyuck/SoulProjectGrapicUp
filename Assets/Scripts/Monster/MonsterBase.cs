using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBase : MonoBehaviour, IMonster
{
	//���� ����
	public enum MonsterState
	{
		None,
		Idle,
		Move,
		Wait,
		GoTarget,
		Attack,
		Damage,
		Die,
	}
	public enum AttackState
	{
		None,
		MLB,
		MRB,
		E,
		R
	}

	//������Ƽ
	public bool IsSelect
	{
		get
		{
			return _isSelect;
		}
		set
		{
			_isSelect = value;
		}
	}
	public Transform Transform
	{
		get
		{
			return transform;
		}
		set
		{

		}
	}

	public Vector3 Position
	{
		get
		{
			return _centerPivot.position;
		}
		set
		{

		}
	}

	public int Level
	{
		get
		{
			return _level;
		}
		set
		{

		}
	}
	public int HP
	{
		get
		{
			return _hp;
		}
		set
		{
			_hp = value;
		}
	}

	public bool IsCapture
	{
		get
		{
			return _isCapture;
		}
		set
		{
			_isCapture = value;
		}
	}

	//�ν����Ϳ��� Ȯ���� �� �ִ� �Ӽ�
	[SerializeField]
	protected MonsterState _monsterState = MonsterState.None; //���� ����
	[SerializeField]
	protected MonsterState _beforeMonsterState = MonsterState.None; //���� ����
	[SerializeField]
	protected AttackState _attackState = AttackState.None; //���� ����
	[SerializeField]
	private int _level = 5; //�ʱ� ����
	[SerializeField]
	private GameObject _selection = null; //���õ� �� ų ���̴� ������Ʈ
	[SerializeField]
	private Transform _centerPivot; //�߽��� Ʈ������
	[SerializeField]
	private float _moveSpeed = 0f; //�̵��ӵ�
	[SerializeField]
	private float _gravitySpeed = 0f; //�߷¼ӵ�
	[SerializeField]
	private float _rotateSpeed = 100.0f; //���� ȸ���ӵ�
	[SerializeField]
	private float _bodyRotateSpeed = 50.0f; //���� ȸ���ӵ�
	[Range(0.01f, 1f)]
	[SerializeField]
	private float velocityChangeSpeed = 0.01f; //���� ������
	[SerializeField]
	private int _hp = 100; //ü��
	[SerializeField]
	private float _atkRange = 1.5f; //���� ���� �Ÿ�
	[SerializeField]
	private GameObject _targetCharacter = null; //������ Ÿ��
	[SerializeField]
	private float _viewAngle; //�þ߰�
	[SerializeField]
	private float _groundHeight; //���������� ������ ����

	//�����ϴ� �Ӽ�
	protected Animator _animator = null;
	private CharacterController _characterController = null;
	private IAttack[] _iAttacks = null;
	private PlayerMove _playerMove = null;

	//�Ӽ�
	protected bool _isSelect = false; //���õǾ�����
	protected bool _isCapture = false; //���� �Ǿ�����
	protected Vector3 _moveDirect = Vector3.zero; //�̵�����
	protected Vector3 _gravityDirect = Vector3.zero; //�߷� ����
	protected Vector3 currentVelocitySpeed = Vector3.zero; //ĳ���� ���� �̵� �ӵ�
	protected Vector3 posTarget = Vector3.zero; //���Ͱ� �� Ÿ�� ��ġ
	private bool _isAttack; //�������� ��
	private bool _isDie; //�׾��� ��


	public virtual void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_animator = GetComponentInChildren<Animator>();
		_iAttacks = GetComponentsInChildren<IAttack>(true);
		_playerMove = FindObjectOfType<PlayerMove>();
		ChangeState(MonsterState.Idle);
	}

	public void Update()
	{
		if(_isDie)
		{
			return;
		}
		if(!IsCapture)
		{
			CheckState();
		}
		BodyDirectChange();
		SetGravity();
		SetAnimation();
	}

	/// <summary>
	/// �ذ� ���¿� ���� ���� �����ϴ� �Լ�
	/// </summary>
	void CheckState()
	{
		switch (_monsterState)
		{
			case MonsterState.Idle:
				SetIdle(); // Idle -> GoTarget or Move
				break;
			case MonsterState.GoTarget:
			case MonsterState.Move:
				SetMove();
				break;
			case MonsterState.Attack:
				SetAttack();
				break;
			case MonsterState.Damage:
				break;
			case MonsterState.Die:
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// �ذ� ���°� ����� �� ����
	/// </summary>
	void SetIdle()
	{
		//�÷��̾ �����ߴ��� �� �ߴ���
		if (_targetCharacter == null)
		{
			//������ �̵� ��ǥ��
			posTarget = new Vector3(transform.position.x + Random.Range(-10f, 10f),
									transform.position.y + 1000f,
									transform.position.z + Random.Range(-10f, 10f));


			//����ĳ��Ʈ ������ ��ǥ����
			Ray ray = new Ray(posTarget, Vector3.down);
			RaycastHit info = new RaycastHit();
			//�浹ü�� �ִٸ�
			if (Physics.Raycast(ray, out info, Mathf.Infinity))
			{
				//������ ��ǥ ���Ϳ� ���� �� �߰�
				posTarget.y = info.point.y;
			}
			//�ذ� ���¸� Move��
			ChangeState(MonsterState.Move);
		}
		else
		{
			//�ذ� ���¸� Go Target����
			ChangeState(MonsterState.GoTarget);
		}
	}

	/// <summary>
	/// �ذ� ���°� MoveȤ�� GOtoTarget�� �� �̵� �Լ�
	/// </summary>
	void SetMove()
	{
		//������� ������ �� ������ ����
		Vector3 distance = Vector3.zero;
		//����
		Vector3 posLookAt = Vector3.zero;

		switch (_monsterState)
		{
			case MonsterState.Move:
				//��ǥ ��ġ�� ���� ��
				if (posTarget != Vector3.zero)
				{
					//��ǥ ��ġ�� �ذ� ��ġ ���� ���ϱ�
					distance = posTarget - transform.position;

					if (distance.magnitude < _atkRange)
					{
						StartCoroutine(SetWait(0.5f));
						return;
					}

					//�ٶ󺸴� ����
					posLookAt = new Vector3(posTarget.x, transform.position.y, posTarget.z);
				}
				break;
			case MonsterState.GoTarget:
				if (_targetCharacter != null)
				{
					//Ÿ�� ��ġ�� �ذ� ��ġ ���� ���ϱ�
					distance = _targetCharacter.transform.position - transform.position;
					float angle = GetTargetToAngle();
					if (distance.magnitude < _atkRange && angle < _viewAngle / 2)
					{
						ChangeState(MonsterState.Attack);
						_attackState = AttackState.MLB;
						return;
					}

					//�ٶ󺸴� ����
					posLookAt = new Vector3(_targetCharacter.transform.position.x, transform.position.y, _targetCharacter.transform.position.z);
				}
				break;
		}

		//�ذ��� �̵��� ������ ũ�Ⱑ ���� ���⸸ ���� ����
		Vector3 direction = distance.normalized;

		//������ x,z�� ���
		direction = new Vector3(direction.x, 0f, direction.z);
		_moveDirect = direction.normalized;

		//�̵��� ���� ����
		Vector3 amount = direction * _moveSpeed * Time.deltaTime + _gravityDirect;

		//���� ��ǥ�� �̵�
		_characterController.Move(amount);

		//ĳ���� ����
		transform.LookAt(posLookAt);

	}

	/// <summary>
	/// �ذ� ���ݸ��
	/// </summary>
	void SetAttack()
	{
		float distance = Vector3.Distance(_targetCharacter.transform.position, transform.position);
		float angle = GetTargetToAngle();
		if ((distance > _atkRange + 0.5f || angle >= _viewAngle / 2) && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f )
		{
			ChangeState(MonsterState.GoTarget);
		}

	}

	/// <summary>
	/// ���� ���� �ڷ�ƾ �Լ�
	/// </summary>
	/// <returns></returns>
	IEnumerator SetWait(float time)
	{
		//��� ���·� ����
		ChangeState(MonsterState.Wait);
		//���ð� �۵�
		yield return new WaitForSeconds(time);
		//Idle�� �����ش�
		ChangeState(MonsterState.Idle);
	}

	public bool CheckCapture(int level)
	{
		if (_level <= level && _hp > 0)
		{
			Debug.Log("���� ����");
			return true;
		}
		else
		{
			Debug.Log("���� �Ұ���");
			return false;
		}
	}

	public virtual bool KeyESkill()
	{
		Debug.Log("��ųE");
		if (IsCapture)
		{
			return true;
		}
		return false;
	}

	public virtual bool KeyRSkill()
	{
		Debug.Log("��ųR");
		if (IsCapture)
		{
			return true;
		}
		return false;
	}

	public virtual bool MouseLButtonSkill()
	{
		Debug.Log("��ųMLB");
		if (IsSelect)
		{
			return false;
		}
		return true;
	}

	public virtual bool MouseRButtonSkill()
	{
		Debug.Log("��ųMRB");
		if (IsSelect)
		{
			return false;
		}
		return true;
	}

	public void MonsterAI()
	{

	}

	public void SelectMonster()
	{
		_selection.gameObject.SetActive(true);
		IsSelect = true;

	}

	public void UnSelectMonster()
	{
		_selection.gameObject.SetActive(false);
		IsSelect = false;
	}

	public bool MonsterMove(Vector3 targetVector)
	{
		if (IsCapture)
		{
			if(_monsterState == MonsterState.Wait || _monsterState == MonsterState.Damage || _monsterState == MonsterState.Attack)
			{
				return false;
			}
			ChangeState(MonsterState.Move);
			float inputX = Input.GetAxis("Horizontal");
			float inputY = Input.GetAxis("Vertical");

			if (inputY >= 0)
			{
				_moveDirect = Vector3.RotateTowards(_moveDirect, targetVector, _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
				_moveDirect = _moveDirect.normalized;
			}

			Vector3 amount = (targetVector * _moveSpeed * Time.deltaTime) + (_gravityDirect * Time.deltaTime);

			_characterController.Move(amount);

			return false;
		}
		return true;
	}

	/// <summary>
	/// ����
	/// </summary>
	public void Capture()
	{
		SelectMonster();
		ChangeState(MonsterState.Idle);
		_isCapture = true;
		int count = _iAttacks.Length;
		for (int i = 0; i < count; i++)
		{
			_iAttacks[i].IsPlayer = true;
		}
	}

	/// <summary>
	/// ���� ����
	/// </summary>
	public void UnCapture()
	{
		UnSelectMonster();
		if(_monsterState != MonsterState.Die)
		{
			StartCoroutine(SetWait(1f));
		}
		_isCapture = false;
		int count = _iAttacks.Length;
		for (int i = 0; i < count; i++)
		{
			_iAttacks[i].IsPlayer = false;
		}
	}

	/// <summary>
	/// �ִϸ��̼� ����
	/// </summary>
	private void SetAnimation()
	{
		switch (_monsterState)
		{
			case MonsterState.Idle:
				_isAttack = false;
				_animator.SetBool("IsWalk", false);
				break;
			case MonsterState.GoTarget:
			case MonsterState.Move:
				_isAttack = false;
				if (GetVelocitySpd() <= 0)
				{
					_animator.SetBool("IsWalk", false);
				}
				else
				{
					_animator.SetBool("IsWalk", true);
				}
				break;
			case MonsterState.Attack:
				if (_isDie)
				{
					break;
				}
				if (_isAttack && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
				{
					_isAttack = false;
					StartCoroutine(SetWait(0.3f));
					break;
				}
				if(_isAttack)
				{
					break;
				}
				_isAttack = true;
				switch (_attackState)
				{
					case AttackState.None:
						break;
					case AttackState.MLB:
						_animator.SetTrigger("IsAttackMLB");
						break;
					case AttackState.MRB:
						_animator.SetTrigger("IsAttackMRB");
						break;
					case AttackState.E:
						_animator.SetTrigger("IsAttackE");
						break;
					case AttackState.R:
						_animator.SetTrigger("IsAttackR");
						break;
				}
				_animator.SetBool("IsWalk", false);
				break;
			case MonsterState.Damage:
				if (_isDie)
				{
					break;
				}
				_isAttack = false;
				_animator.SetTrigger("IsDamage");
				StartCoroutine(SetWait(1f));
				break;
			case MonsterState.Die:
				if(_isDie)
				{
					break;
				}
				_isDie = true;
				_isAttack = false;
				_animator.SetTrigger("IsDie");
				break;
			default:
				break;
		}
	}

	/// <summary>
	/// �߷� ����
	/// </summary>
	private void SetGravity()
	{
		Ray ray = new Ray(transform.position, -transform.up);
		RaycastHit raycastHit;

		if (Physics.Raycast(ray, out raycastHit, _groundHeight))
		{
			_gravityDirect.y = 0;
		}
		else
		{
			_gravityDirect.y = -_gravitySpeed;
		}
	}

	/// <summary>
	/// ���� ������
	/// </summary>
	private void BodyDirectChange()
	{
		if (GetVelocitySpd() > 0.0f)
		{
			transform.forward = Vector3.Lerp(transform.forward, _moveDirect, _bodyRotateSpeed * Time.deltaTime);
		}
	}

	/// <summary>
	/// �ӵ� ��ȯ
	/// </summary>
	private float GetVelocitySpd()
	{
		if (_characterController.velocity == Vector3.zero)
		{
			currentVelocitySpeed = Vector3.zero;
		}
		else
		{
			Vector3 retVelocity = _characterController.velocity;
			retVelocity.y = 0;
			currentVelocitySpeed = Vector3.Lerp(currentVelocitySpeed, retVelocity, velocityChangeSpeed * Time.fixedDeltaTime);
		}

		return currentVelocitySpeed.magnitude;
	}

	/// <summary>
	/// ���� �浹
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("ATK"))
		{
			var iAttack = other.GetComponent<IAttack>();
			if(iAttack.IsPlayer != _isCapture)
			{
				//���ݹ���
				Damaged(iAttack);
			}
		}
	}

	/// <summary>
	/// ���ݹ޾��� �� �Լ�
	/// </summary>
	/// <param name="iAttack"></param>
	private void Damaged(IAttack iAttack)
	{
		_hp -= iAttack.Damage;
		Instantiate(iAttack.Effect, _centerPivot.position, Quaternion.identity);
		if (_hp > 0)
		{
			ChangeState(MonsterState.Damage);
		}
		else
		{
			if(_monsterState != MonsterState.Die)
			{
				ChangeState(MonsterState.Die);
				_playerMove.AddExp(10);
				if(IsCapture)
				{
					_playerMove.OutCaptureMonster();	
				}
			}
		}
	}

	/// <summary>
	/// Ÿ���� ���� ����
	/// </summary>
	/// <returns></returns>
	private float GetTargetToAngle()
	{
		Vector3 targetPos = _targetCharacter.transform.position;
		Vector3 currentPos = transform.position;
		targetPos.y = 0;
		currentPos.y = 0;
		Vector3 dirToTarget = (targetPos - currentPos).normalized;
		float targetAngle = Mathf.Acos(Vector3.Dot(transform.forward, dirToTarget)) * Mathf.Rad2Deg;
		return targetAngle;
	}

	/// <summary>
	/// Ÿ�� ã��
	/// </summary>
	/// <param name="target"></param>
	private void OnCheckTarget(GameObject target)
	{
		if(_targetCharacter == null)
		{
			_targetCharacter = target;
			_monsterState = MonsterState.GoTarget;
		}

	}

	/// <summary>
	/// ���� ����
	/// </summary>
	/// <param name="monsterState"></param>
	public void ChangeState(MonsterState monsterState)
	{
		_monsterState = monsterState;
	}
}
