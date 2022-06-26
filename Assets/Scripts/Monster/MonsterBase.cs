using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour, IMonster
{

	//몬스터 상태
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

	//프로퍼티
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

	//인스펙터에서 확인할 수 있는 속성
	[SerializeField]
	protected MonsterState _monsterState = MonsterState.None; //몬스터 상태
	[SerializeField]
	protected MonsterState _beforeMonsterState = MonsterState.None; //몬스터 상태
	[SerializeField]
	protected AttackState _attackState = AttackState.None; //몬스터 상태
	[SerializeField]
	private int _originLevel = 5; //초기 레벨
	[SerializeField]
	private int _level = 5; //레벨
	[SerializeField]
	private GameObject _selection = null; //선택될 때 킬 쉐이더 오브젝트
	[SerializeField]
	private Transform _centerPivot; //중심점 트랜스폼
	[SerializeField]
	private float _originMoveSpeed = 0f; //기본 이동속도
	[SerializeField]
	private float _gravitySpeed = 0f; //중력속도
	[SerializeField]
	private float _rotateSpeed = 100.0f; //방향 회전속도
	[SerializeField]
	private float _bodyRotateSpeed = 50.0f; //몸통 회전속도
	[Range(0.01f, 1f)]
	[SerializeField]
	private float velocityChangeSpeed = 0.01f; //가변 증가값
	[SerializeField]
	private int _maxhp = 100; //최대체력
	[SerializeField]
	private int _hp = 100; //체력
	[SerializeField]
	private float _atkRange = 1.5f; //몬스터 공격 거리
	[SerializeField]
	protected GameObject _targetCharacter = null; //몬스터의 타겟
	[SerializeField]
	private float _viewAngle; //시야각
	[SerializeField]
	private float _groundHeight; //땅에서부터 떨어질 높이
	[SerializeField]
	private Sprite _monsterSprite; //몬스터 스프라이트
	[SerializeField]
	protected float _coolTimeSpeedMLB = 10.0f; //마우스 좌클릭 쿨타임 증가속도
	[SerializeField]
	protected float _coolTimeSpeedMRB = 10.0f; //마우스 우클릭 쿨타임 증가속도
	[SerializeField]
	protected float _coolTimeSpeedE = 10.0f; //E 쿨타임 증가속도
	[SerializeField]
	protected float _coolTimeSpeedR = 10.0f; //R 쿨타임 증가속도
	[SerializeField]
	protected int _atk = 10; //공격 스탯
	[SerializeField]
	protected int _speed = 10; //속도 스탯
	[SerializeField]
	protected int _defense = 10; //방어력 스탯
	[SerializeField]
	protected int _exp = 0; //경험치
	[SerializeField]
	protected ItemSetSO _dropItemSet = null; //드랍하는 아이템 리스트

	//참조하는 속성
	protected Animator _animator = null;
	protected CharacterController _characterController = null;
	private IAttack[] _iAttacks = null;
	private Player _player = null;
	private MoneyInventory _moneyInventory = null;
	private NoticeManager _noticeManager = null;
	private EffectManager _effectManager = null;
	private MonsterSpawner _monsterSpawner = null;
	protected ItemPool _itemPool = null;

	//속성
	protected float _moveSpeed = 1.0f; //이동속도
	protected bool _isSelect = false; //선택되었는지
	protected bool _isCapture = false; //빙의 되었는지
	protected Vector3 _moveDirect = Vector3.zero; //이동방향
	protected Vector3 _gravityDirect = Vector3.zero; //중력 벡터
	protected Vector3 currentVelocitySpeed = Vector3.zero; //캐릭터 현재 이동 속도
	protected Vector3 _posTarget = Vector3.zero; //몬스터가 본 타겟 위치
	protected string _name; //몬스터 이름
	protected string _description; //몬스터 설명;
	protected float _coolTimeMLB = 0.0f; //마우스 좌클릭 쿨타임
	protected float _coolTimeMRB = 0.0f; //마우스 우클릭 쿨타임
	protected float _coolTimeE = 0.0f; //E 쿨타임
	protected float _coolTimeR = 0.0f; //R 쿨타임

	protected bool _canSkillMLB = false; //마우스 좌클릭 스킬을 사용 가능한지
	protected bool _canSkillMRB = false; //마우스 우클릭 스킬을 사용 가능한지
	protected bool _canSkillE = false; //E 스킬을 사용 가능한지
	protected bool _canSkillR = false; //R 스킬을 사용 가능한지
	private bool _isAttack; //공격중일 때
	private bool _isDie; //죽었을 때

	//초깃값
	private int _originMaxHP = 0;
	protected int _originAtk = 0; //공격 스탯
	protected int _originSpeed = 0; //속도 스탯
	protected int _originDefense = 0; //방어력 스탯


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
	/// 해골 상태에 따라 동작 제어하는 함수
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
	/// 해골 상태가 대기일 때 동작
	/// </summary>
	void SetIdle()
	{
		//플레이어를 감지했느냐 안 했느냐
		if (_targetCharacter == null)
		{
			//임의의 이동 목표점
			_posTarget = new Vector3(transform.position.x + Random.Range(-10f, 10f),
									transform.position.y + 1000f,
									transform.position.z + Random.Range(-10f, 10f));


			//레이캐스트 시작점 목표방향
			Ray ray = new Ray(_posTarget, Vector3.down);
			RaycastHit info = new RaycastHit();
			//충돌체가 있다면
			if (Physics.Raycast(ray, out info, Mathf.Infinity))
			{
				//임의의 목표 벡터에 높이 값 추가
				_posTarget.y = info.point.y;
			}
			//해골 상태를 Move로
			ChangeState(MonsterState.Move);
		}
		else
		{
			//해골 상태를 Go Target으로
			ChangeState(MonsterState.GoTarget);
		}
	}

	/// <summary>
	/// 해골 상태가 Move혹은 GOtoTarget일 때 이동 함수
	/// </summary>
	void SetMove()
	{
		//출발점과 도착점 두 벡터의 차이
		Vector3 distance = Vector3.zero;
		//방향
		Vector3 posLookAt = Vector3.zero;

		switch (_monsterState)
		{
			case MonsterState.Move:
				//목표 위치가 있을 때
				if (_posTarget != Vector3.zero)
				{
					//목표 위치와 해골 위치 차이 구하기
					distance = _posTarget - transform.position;

					if (distance.magnitude < _atkRange)
					{
						StartCoroutine(SetWait(0.5f));
						return;
					}

					//바라보는 방향
					posLookAt = new Vector3(_posTarget.x, transform.position.y, _posTarget.z);
				}
				break;
			case MonsterState.GoTarget:
				if (_targetCharacter != null)
				{
					//타겟 위치와 해골 위치 차이 구하기
					distance = _targetCharacter.transform.position - transform.position;
					float angle = GetTargetToAngle();
					if (distance.magnitude < _atkRange && angle < _viewAngle / 2)
					{
						//if(CheckCoolTimeR())
						//{
						//	KeyRSkill();
						//}
						//else if (CheckCoolTimeE())
						//{
						//	KeyESkill();
						//}
						//else if(CheckCoolTimeMRB())
						//{
						//	MouseRButtonSkill();
						//}
						if(CheckCoolTimeMLB())
						{
							MouseLButtonSkill();
						}
						return;
					}

					//바라보는 방향
					posLookAt = new Vector3(_targetCharacter.transform.position.x, transform.position.y, _targetCharacter.transform.position.z);
				}
				break;
		}

		//해골이 이동할 방향은 크기가 없고 방향만 가빈 벡터
		Vector3 direction = distance.normalized;

		//방향은 x,z만 사용
		direction = new Vector3(direction.x, 0f, direction.z);
		_moveDirect = direction.normalized;

		//이동량 방향 구함
		Vector3 amount = direction * _moveSpeed * Time.deltaTime + _gravityDirect;

		//월드 좌표로 이동
		_characterController.Move(amount);

		//캐릭터 방향
		transform.LookAt(posLookAt);

	}

	/// <summary>
	/// 해골 공격모드
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
	/// 정지 상태 코루틴 함수
	/// </summary>
	/// <returns></returns>
	IEnumerator SetWait(float time)
	{
		//대기 상태로 변경
		ChangeState(MonsterState.Wait);
		//대기시간 작동
		yield return new WaitForSeconds(time);
		//Idle로 돌려준다
		ChangeState(MonsterState.Idle);
	}

	public bool CheckCapture(int level)
	{
		if (_level <= level && _hp > 0)
		{
			Debug.Log("빙의 가능");
			return true;
		}
		else
		{
			Debug.Log("빙의 불가능");
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
	/// 빙의 됐을 때 움직임
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
	/// 빙의
	/// </summary>
	public void Capture()
	{
		SelectMonster();
		ChangeState(MonsterState.Idle);
		_isCapture = true;
	}

	/// <summary>
	/// 빙의 해제
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
	/// 애니메이션 설정
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
	/// 중력 설정
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
	/// 몸통 돌리기
	/// </summary>
	private void BodyDirectChange()
	{
		if (GetVelocitySpd() > 0.0f)
		{
			transform.forward = Vector3.Lerp(transform.forward, _moveDirect, _bodyRotateSpeed * Time.deltaTime);
		}
	}

	/// <summary>
	/// 속도 반환
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
	/// 공격 충돌
	/// </summary>
	/// <param name="other"></param>
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("ATK"))
		{
			var iAttack = other.GetComponent<IAttack>();
			if(iAttack.Attacker == null)
			{
				//공격받음
				Damaged(iAttack);
				_targetCharacter = null;
			}
			else if(iAttack.Attacker != gameObject)
			{
				Damaged(iAttack);
				_targetCharacter = iAttack.Attacker;
			}
		}
	}

	/// <summary>
	/// 공격받았을 때 함수
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
	/// 죽음
	/// </summary>
	private void Die()
	{
		if (_monsterState != MonsterState.Die)
		{
			ChangeState(MonsterState.Die);
			NoticeManager.Notice(10 * Level);
			PlayerStat playerStat = _player.GetComponent<PlayerStat>();
			playerStat.AddExp(10 * Level);
			playerStat.AddMoney(10 * Level);
			ItemDrop();
			StartCoroutine(DeleteMonster());
		}
	}

	/// <summary>
	/// 아이템 드랍
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
	/// 타겟을 향한 각도
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
	/// 타겟 찾음
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
	/// 상태 변경
	/// </summary>
	/// <param name="monsterState"></param>
	public void ChangeState(MonsterState monsterState)
	{
		_monsterState = monsterState;
	}
	/// <summary>
	/// 쿨타임 증가
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

	public virtual void Delete()
	{
		MonsterSpawner.RemoveCount();
		gameObject.SetActive(false);
		//ItemPoolFind.RegisterObject<MonsterBase>(this); 자식에서 다시 구현
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
