using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour, IMonster
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
	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}
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
	public int MaxHP
	{
		get
		{
			return _maxhp;
		}
		set
		{
			_maxhp = value;
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
	public MoneyInventory MoneyInventory
	{
		get
		{
			if (_moneyInventory == null)
			{
				_moneyInventory = FindObjectOfType<MoneyInventory>();
			}
			return _moneyInventory;
		}
	}
	public NoticeManager NoticeManager
	{
		get
		{
			if (_noticeManager == null)
			{
				_noticeManager = FindObjectOfType<NoticeManager>();
			}
			return _noticeManager;
		}
	}
	public ItemPool ItemPoolFind
	{
		get
		{
			_itemPool ??= FindObjectOfType<ItemPool>();
			return _itemPool;
		}
	}

	public EffectManager EffectManagerObj
	{
		get
		{
			_effectManager ??= FindObjectOfType<EffectManager>();
			return _effectManager;
		}
		set
		{
			_effectManager = value;
		}
	}

	public GameObject GameObject
	{
		get
		{
			return gameObject;
		}
		set
		{

		}
	}

	public float CoolTimeMLB
	{
		get
		{
			return _coolTimeMLB;
		}
		set
		{
			_coolTimeMLB = value;
		}
	}
	public float CoolTimeMRB
	{
		get
		{
			return _coolTimeMRB;
		}
		set
		{
			_coolTimeMRB = value;
		}
	}
	public float CoolTimeE
	{
		get
		{
			return _coolTimeE;
		}
		set
		{
			_coolTimeE = value;
		}
	}
	public float CoolTimeR
	{
		get
		{
			return _coolTimeR;
		}
		set
		{
			_coolTimeR = value;
		}
	}

	public Sprite Sprite
	{
		get
		{
			return _monsterSprite;
		}
		set
		{
			_monsterSprite = value;
		}
	}

	public int SPD
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
		}
	}
	public int DEF
	{
		get
		{
			return _defense;
		}
		set
		{
			_defense = value;
		}
	}
	public int EXP
	{
		get
		{
			return _exp;
		}
		set
		{
			_exp = value;
		}
	}

	public string Description
	{
		get
		{
			return _description;
		}
		set
		{
			_description = value;
		}
	}

	public int ATK
	{
		get
		{
			return _atk;
		}
		set
		{
			_atk = value;
		}
	}

	public MonsterSpawner MonsterSpawner
	{
		get
		{
			_monsterSpawner ??= FindObjectOfType<MonsterSpawner>();
			return _monsterSpawner;
		}
		set
		{
			_monsterSpawner = value;
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
	private int _originLevel = 5; //�ʱ� ����
	[SerializeField]
	private int _level = 5; //����
	[SerializeField]
	private GameObject _selection = null; //���õ� �� ų ���̴� ������Ʈ
	[SerializeField]
	private Transform _centerPivot; //�߽��� Ʈ������
	[SerializeField]
	private float _originMoveSpeed = 0f; //�⺻ �̵��ӵ�
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
	private int _maxhp = 100; //�ִ�ü��
	[SerializeField]
	private int _hp = 100; //ü��
	[SerializeField]
	private float _atkRange = 1.5f; //���� ���� �Ÿ�
	[SerializeField]
	protected GameObject _targetCharacter = null; //������ Ÿ��
	[SerializeField]
	private float _viewAngle; //�þ߰�
	[SerializeField]
	private float _groundHeight; //���������� ������ ����
	[SerializeField]
	private Sprite _monsterSprite; //���� ��������Ʈ
	[SerializeField]
	protected float _coolTimeSpeedMLB = 10.0f; //���콺 ��Ŭ�� ��Ÿ�� �����ӵ�
	[SerializeField]
	protected float _coolTimeSpeedMRB = 10.0f; //���콺 ��Ŭ�� ��Ÿ�� �����ӵ�
	[SerializeField]
	protected float _coolTimeSpeedE = 10.0f; //E ��Ÿ�� �����ӵ�
	[SerializeField]
	protected float _coolTimeSpeedR = 10.0f; //R ��Ÿ�� �����ӵ�
	[SerializeField]
	protected int _atk = 10; //���� ����
	[SerializeField]
	protected int _speed = 10; //�ӵ� ����
	[SerializeField]
	protected int _defense = 10; //���� ����
	[SerializeField]
	protected int _exp = 0; //����ġ
	[SerializeField]
	protected ItemSetSO _dropItemSet = null; //����ϴ� ������ ����Ʈ

	//�����ϴ� �Ӽ�
	protected Animator _animator = null;
	protected CharacterController _characterController = null;
	private IAttack[] _iAttacks = null;
	private Player _player = null;
	private MoneyInventory _moneyInventory = null;
	private NoticeManager _noticeManager = null;
	private EffectManager _effectManager = null;
	private MonsterSpawner _monsterSpawner = null;
	protected ItemPool _itemPool = null;

	//�Ӽ�
	protected float _moveSpeed = 1.0f; //�̵��ӵ�
	protected bool _isSelect = false; //���õǾ�����
	protected bool _isCapture = false; //���� �Ǿ�����
	protected Vector3 _moveDirect = Vector3.zero; //�̵�����
	protected Vector3 _gravityDirect = Vector3.zero; //�߷� ����
	protected Vector3 currentVelocitySpeed = Vector3.zero; //ĳ���� ���� �̵� �ӵ�
	protected Vector3 _posTarget = Vector3.zero; //���Ͱ� �� Ÿ�� ��ġ
	protected string _name; //���� �̸�
	protected string _description; //���� ����;
	protected float _coolTimeMLB = 0.0f; //���콺 ��Ŭ�� ��Ÿ��
	protected float _coolTimeMRB = 0.0f; //���콺 ��Ŭ�� ��Ÿ��
	protected float _coolTimeE = 0.0f; //E ��Ÿ��
	protected float _coolTimeR = 0.0f; //R ��Ÿ��

	protected bool _canSkillMLB = false; //���콺 ��Ŭ�� ��ų�� ��� ��������
	protected bool _canSkillMRB = false; //���콺 ��Ŭ�� ��ų�� ��� ��������
	protected bool _canSkillE = false; //E ��ų�� ��� ��������
	protected bool _canSkillR = false; //R ��ų�� ��� ��������
	private bool _isAttack; //�������� ��
	private bool _isDie; //�׾��� ��

	//�ʱ갪
	private int _originMaxHP = 0;
	protected int _originAtk = 0; //���� ����
	protected int _originSpeed = 0; //�ӵ� ����
	protected int _originDefense = 0; //���� ����


	public virtual void Start()
	{
		_characterController = GetComponent<CharacterController>();
		_animator = GetComponentInChildren<Animator>();
		_iAttacks = GetComponentsInChildren<IAttack>(true);
		_player = FindObjectOfType<Player>();
		ChangeState(MonsterState.Idle);
		
		SetSpd();
		SetAtk();

		_originMaxHP = _maxhp;
		_originAtk = _atk;
		_originSpeed = _speed;
		_originDefense = _defense;
		Init();
	}
	public void Init()
	{
		_level = _originLevel + Random.Range(0, 2);
		_maxhp = _originMaxHP + Random.Range(50, 200); ;
		_hp = _maxhp;
		_atk = _originAtk + Random.Range(5, 10); ;
		_speed = _originSpeed + Random.Range(1, 10); ;
		_defense = _originDefense + Random.Range(1, 5);
		gameObject.SetActive(true);
		SetSkill();
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
		UpdateCoolTime();
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
			_posTarget = new Vector3(transform.position.x + Random.Range(-10f, 10f),
									transform.position.y + 1000f,
									transform.position.z + Random.Range(-10f, 10f));


			//����ĳ��Ʈ ������ ��ǥ����
			Ray ray = new Ray(_posTarget, Vector3.down);
			RaycastHit info = new RaycastHit();
			//�浹ü�� �ִٸ�
			if (Physics.Raycast(ray, out info, Mathf.Infinity))
			{
				//������ ��ǥ ���Ϳ� ���� �� �߰�
				_posTarget.y = info.point.y;
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
				if (_posTarget != Vector3.zero)
				{
					//��ǥ ��ġ�� �ذ� ��ġ ���� ���ϱ�
					distance = _posTarget - transform.position;

					if (distance.magnitude < _atkRange)
					{
						StartCoroutine(SetWait(0.5f));
						return;
					}

					//�ٶ󺸴� ����
					posLookAt = new Vector3(_posTarget.x, transform.position.y, _posTarget.z);
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
						if(CheckCoolTimeR())
						{
							KeyRSkill();
						}
						else if (CheckCoolTimeE())
						{
							KeyESkill();
						}
						else if(CheckCoolTimeMRB())
						{
							MouseRButtonSkill();
						}
						else if(CheckCoolTimeMLB())
						{
							MouseLButtonSkill();
						}
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
		if(_targetCharacter == null || !_targetCharacter.activeSelf)
		{
			ChangeState(MonsterState.Idle);
		}
		else if ((distance > _atkRange + 0.5f || angle >= _viewAngle / 2) && _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f )
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

	public abstract bool KeyESkill();
	public abstract bool KeyRSkill();
	public abstract bool MouseLButtonSkill();
	public abstract bool MouseRButtonSkill();

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

	/// <summary>
	/// ���� ���� �� ������
	/// </summary>
	/// <param name="targetVector"></param>
	public void MonsterMove(Vector3 targetVector, bool isTarggeting)
	{
		if (_monsterState == MonsterState.Wait || _monsterState == MonsterState.Damage || _monsterState == MonsterState.Attack)
		{
			return;
		}
		ChangeState(MonsterState.Move);

		if(isTarggeting)
		{
			Vector3 forwardVector = Camera.main.transform.forward;
			forwardVector.y = 0;
			transform.forward = forwardVector;
			_moveDirect = Vector3.RotateTowards(_moveDirect, forwardVector, _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
			_moveDirect = _moveDirect.normalized;

			Vector3 amount = (targetVector * _moveSpeed * Time.deltaTime) + (_gravityDirect * Time.deltaTime);

			_characterController.Move(amount);
		}
		else
		{
			_moveDirect = Vector3.RotateTowards(_moveDirect, targetVector, _rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000.0f);
			_moveDirect = _moveDirect.normalized;

			Vector3 amount = (targetVector * _moveSpeed * Time.deltaTime) + (_gravityDirect * Time.deltaTime);

			_characterController.Move(amount);
		}
	}

	/// <summary>
	/// ����
	/// </summary>
	public void Capture()
	{
		SelectMonster();
		ChangeState(MonsterState.Idle);
		_isCapture = true;
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
				if (GetVelocitySpd() > 0.0f)
				{
					_animator.SetBool("IsWalk", true);
				}
				else
				{
					_animator.SetBool("IsWalk", false);
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
			if(iAttack.Attacker != gameObject)
			{
				//���ݹ���
				Damaged(iAttack);
				_targetCharacter = iAttack.Attacker;
			}
		}
	}

	/// <summary>
	/// ���ݹ޾��� �� �Լ�
	/// </summary>
	/// <param name="iAttack"></param>
	private void Damaged(IAttack iAttack)
	{
		int damage = (iAttack.Damage - _defense) + Random.Range(-5, 5);
		if(damage <= 0)
		{
			damage = 1;
		}
		_hp -= damage;
		EffectManagerObj.SetTextEffect(damage, Position);
		EffectManagerObj.AttackEffect(Position);
		if (_hp > 0)
		{
			ChangeState(MonsterState.Damage);
		}
		else
		{
			Die();
		}
	}

	/// <summary>
	/// ����
	/// </summary>
	private void Die()
	{
		if (_monsterState != MonsterState.Die)
		{
			ChangeState(MonsterState.Die);
			NoticeManager.Notice(10);
			PlayerStat playerStat = _player.GetComponent<PlayerStat>();
			playerStat.AddExp(10);
			playerStat.AddMoney(10);
			ItemDrop();
			StartCoroutine(DeleteMonster());
		}
	}

	/// <summary>
	/// ������ ���
	/// </summary>
	private void ItemDrop()
	{
		if(_dropItemSet == null)
		{
			return;
		}
		ItemPool itemPool = ItemPoolFind;
		ItemObject itemObject = itemPool.GetObject<ItemObject>();
		if (itemObject == null)
		{
			itemObject = new GameObject().AddComponent<ItemObject>();
		}
		
		int random = Random.Range(0, _dropItemSet._itemsList.Count - 1);
		ItemData itemData = _dropItemSet._itemsList[random];

		itemObject.Setting(itemData.ChangeIItem(), itemData, Position);
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
	/// <summary>
	/// ��Ÿ�� ����
	/// </summary>
	private void UpdateCoolTime()
	{
		if(CoolTimeMLB < 1)
		{
			_coolTimeMLB += Time.deltaTime * _coolTimeSpeedMLB;
		}
		if (CoolTimeMRB < 1)
		{
			_coolTimeMRB += Time.deltaTime * _coolTimeSpeedMRB;
		}
		if (CoolTimeE < 1)
		{
			_coolTimeE += Time.deltaTime * _coolTimeSpeedE;
		}
		if (CoolTimeR < 1)
		{
			_coolTimeR += Time.deltaTime * _coolTimeSpeedR;
		}
	}
	public bool CheckCanMLB()
	{
		return _canSkillMLB;
	}
	public bool CheckCanMRB()
	{
		return _canSkillMRB;
	}
	public bool CheckCanE()
	{
		return _canSkillE;
	}
	public bool CheckCanR()
	{
		return _canSkillR;
	}
	public bool CheckCoolTimeMLB()
	{
		if(CheckCanMLB())
		{
			return _coolTimeMLB > 1;
		}
		return false;
	}
	public bool CheckCoolTimeMRB()
	{
		if (CheckCanMRB())
		{
			return _coolTimeMRB > 1;

		}
		return false;
	}
	public bool CheckCoolTimeE()
	{
		if (CheckCanE())
		{
			return _coolTimeE > 1;

		}
		return false;
	}
	public bool CheckCoolTimeR()
	{
		if (CheckCanR())
		{
			return _coolTimeR > 1;

		}
		return false;
	}

	public bool CheckMaxLevel()
	{
		return _level >= 100;
	}

	public void LevelUP()
	{
		++_level;
		AddMaxHP(5);
		AddAtk(1);
		AddDefense(1);
		AddSpeed(1);
		SetEXP(0);
		_hp = MaxHP;
		SetSkill();
	}


	public void AddHP(int hp)
	{
		_hp += hp;
		if(_hp > MaxHP)
		{
			_hp = MaxHP;
		}
	}
	public void AddMaxHP(int add)
	{
		_maxhp = _maxhp + add;
		_hp += add;
	}
	public void AddAtk(int add)
	{
		_atk = _atk + add;
		SetAtk();
	}
	public void SetAtk()
	{
		int count = _iAttacks.Length;
		for (int i = 0; i < count; i++)
		{
			_iAttacks[i].AddDamage = _atk;
		}
	}
	private void SetSpd()
	{
		_moveSpeed = _originMoveSpeed + (float)_speed / 20;
	}

	public void SetEXP(int exp)
	{
		_exp = exp;
	}

	public void AddEXP(int exp)
	{
		if(_monsterState == MonsterState.Die)
		{
			return;
		}

		_exp += exp;

		if(_exp > _level * 10)
		{
			LevelUP();
		}
	}

	public void OnGUI()
	{
		if(!IsCapture)
		{
			return;
		}
		GUI.Label(new Rect(5, 5, 1920, 20), $"ĳ���� ��Ʈ�ѷ� �ӵ� : {_characterController.velocity}");
		GUI.Label(new Rect(5, 25, 1920, 20), $"ĳ���� ��Ʈ�ѷ� �ӵ��ű״�: {_characterController.velocity.magnitude}");
		GUI.Label(new Rect(5, 45, 1920, 20), $"���� �ӵ�: {currentVelocitySpeed}");
		GUI.Label(new Rect(5, 65, 1920, 20), $"���� �ӵ��ű״�: {currentVelocitySpeed.magnitude}");
		
	}

	public virtual void Delete()
	{
		MonsterSpawner.RemoveCount();
		gameObject.SetActive(false);
		//ItemPoolFind.RegisterObject<MonsterBase>(this); �ڽĿ��� �ٽ� ����
	}

	private IEnumerator DeleteMonster()
	{
		yield return new WaitForSeconds(7f);
		Delete();
	}

	public void SetPos(Vector3 pos)
	{
		transform.position = pos;
	}

	private void SetSkill()
	{
		if(_level >= 1)
		{
			_canSkillMLB = true;
		}
		if (_level >= 10)
		{
			_canSkillMRB = true;
		}
		if (_level >= 30)
		{
			_canSkillE = true;
		}
		if (_level >= 50)
		{
			_canSkillR = true;
		}
	}

	public void AddSpeed(int spd)
	{
		_speed = _speed + spd;
		SetSpd();
	}

	public void AddDefense(int def)
	{
		_defense = _defense + def;
	}
}
