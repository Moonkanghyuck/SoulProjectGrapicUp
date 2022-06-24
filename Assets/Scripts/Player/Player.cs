using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;

public class Player : MonoBehaviour
{
	//������Ƽ
	public OverWorldUIManager OverWorldUIManager
	{
		get
		{
			_overWorldUIManager ??= FindObjectOfType<OverWorldUIManager>();
			return _overWorldUIManager;
		}
	}
	public CharacterController CharacterController
	{
		get
		{
			_characterController ??= GetComponent<CharacterController>();
			return _characterController;
		}
		set
		{
			_characterController = value;
		}
	}
	public GameOverUIManager GameOverUIManager
	{
		get
		{
			_gameOverUIManager ??= FindObjectOfType<GameOverUIManager>();
			return _gameOverUIManager;
		}
		set
		{
			_gameOverUIManager = value;
		}
	}
	public BattleUICanvas BattleUICanvas
	{
		get
		{
			_battleUICanvas ??= FindObjectOfType<BattleUICanvas>();
			return _battleUICanvas;
		}
		set
		{

		}
	}
	public ItemInventory ItemInventory
	{
		get
		{
			_itemInventory ??= FindObjectOfType<ItemInventory>();
			return _itemInventory;
		}
		set
		{
			_itemInventory = value;
		}
	}
	public NoticeManager NoticeManager
	{
		get
		{
			_noticeManager ??= FindObjectOfType<NoticeManager>();
			return _noticeManager;
		}
		set
		{
			_noticeManager = value;
		}
	}
	public IInteraction SelectObject
	{
		get
		{
			return _selectObj;
		}
		set
		{
			_selectObj = value;
		}
	}
	public IMonster SelectMonster
	{
		get
		{
			return _selectMonster;
		}
		set
		{
			_selectMonster = value;
		}
	}
	public IMonster CaptureMonster
	{
		get
		{
			return _captureMonster;
		}
		set
		{
			_captureMonster = value;
		}
	}
	public IMonster TargettingMonster
	{
		get
		{
			return _targettingMonster;
		}
		set
		{
			_targettingMonster = value;
		}
	}
	public Camera MainCamera
	{
		get
		{
			_mainCamera ??= Camera.main;
			return _mainCamera;
		}
		set
		{
			_mainCamera = value;
		}
	}
	public CameraMove MainCameraMove
	{
		get
		{
			_mainCameraMove = FindObjectOfType<CameraMove>();
			return _mainCameraMove;
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
	public bool IsCantAnything
	{
		get
		{
			return _isCantAnything;
		}
		set
		{
			_isCantAnything = value;
		}
	}

	public bool IsTarggeting
	{
		get
		{
			return _isTarggeting;
		}
		set
		{
			_isTarggeting = value;
		}
	}
	
	private bool _isCantAnything = false; //�ƹ� �͵� �� �� ���� ����
	private bool _isCapture = false; //�������� ����
	private bool _isTarggeting = false; //��� �ָ����� ����
	private ItemInventory _itemInventory; //������ �κ��丮
	private NoticeManager _noticeManager; //�߰� �Ŵ���
	private BattleUICanvas _battleUICanvas; //��ƲUIĵ����
	private OverWorldUIManager _overWorldUIManager; //�� UI
	private GameOverUIManager _gameOverUIManager; //���ӿ���UI
	private EffectManager _effectManager = null; //����Ʈ �Ŵ���
	private IMonster _selectMonster = null; //���콺�� ������ ����
	private IMonster _captureMonster = null; //������ ����
	private IMonster _targettingMonster; //Ÿ���õ� ����
	private IInteraction _selectObj; //���õ� ������Ʈ
	private Camera _mainCamera; //���� ī�޶�
	private CameraMove _mainCameraMove; //���� ī�޶��� ���꽺ũ��Ʈ
	private CharacterController _characterController; //ĳ���� ��Ʈ�ѷ�

	public void AddItem(IItem item)
	{
		ItemInventory?.AddItem(item);
		NoticeManager?.Notice(item);
	}

}
