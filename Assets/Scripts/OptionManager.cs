using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
	[SerializeField]
	private Canvas _optionCanvas = null;
	[SerializeField]
	private MoneyInventory _moneyInventory = null;

	[SerializeField]
	private GameObject _inventoryPanel;
	[SerializeField]
	private GameObject _soulPanel;
	[SerializeField]
	private GameObject _monsterPanel;
	[SerializeField]
	private GameObject _quitPanel;

	[SerializeField]
	private Button _inventoryButton;
	[SerializeField]
	private Button _soulButton;
	[SerializeField]
	private Button _monsterButton;
	[SerializeField]
	private Button _quitButton;
	[SerializeField]
	private Button _quitNoButton;
	[SerializeField]
	private Button _quitYesButton;


	private void Start()
	{
		_inventoryButton.onClick.AddListener(() => InventoryPanelActive());
		_soulButton.onClick.AddListener(() => SoulPanelActive());
		_monsterButton.onClick.AddListener(() => MonsterPanelActive());

		_quitButton.onClick.AddListener(() => OpenQuitPanel());
		_quitNoButton.onClick.AddListener(() => CloseQuitPanel());
		_quitYesButton.onClick.AddListener(() => MoveMainScene());


	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			if(!_optionCanvas.gameObject.activeSelf)
			{
				CanvasActive();
			}
			else if(_inventoryPanel.activeSelf)
			{
				CanvasActive();
			}
			InventoryPanelActive();
		}
		else if (Input.GetKeyDown(KeyCode.O))
		{
			if (!_optionCanvas.gameObject.activeSelf)
			{
				CanvasActive();
			}
			else if (_soulPanel.activeSelf)
			{
				CanvasActive();
			}
			SoulPanelActive();
		}
		else if (Input.GetKeyDown(KeyCode.P))
		{
			if (!_optionCanvas.gameObject.activeSelf)
			{
				CanvasActive();
			}
			else if (_monsterPanel.activeSelf)
			{
				CanvasActive();
			}
			MonsterPanelActive();
		}
	}

	private void CanvasActive()
	{
		_optionCanvas.gameObject.SetActive(!_optionCanvas.gameObject.activeSelf);
		_moneyInventory.SetActiveMoenyCanvas(_optionCanvas.gameObject.activeSelf);
		Time.timeScale = _optionCanvas.gameObject.activeSelf ? 0 : 1;
		_moneyInventory.SetActiveMoenyCanvas(_optionCanvas.gameObject.activeSelf);
		_quitPanel.gameObject.SetActive(false);

		if (_optionCanvas.gameObject.activeSelf)
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		else
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	private void InventoryPanelActive()
	{
		_inventoryPanel.SetActive(true);
		_soulPanel.SetActive(false);
		_monsterPanel.SetActive(false);
	}
	private void SoulPanelActive()
	{
		_soulPanel.SetActive(true);
		_inventoryPanel.SetActive(false);
		_monsterPanel.SetActive(false);
	}
	private void MonsterPanelActive()
	{
		_monsterPanel.SetActive(true);
		_inventoryPanel.SetActive(false);
		_soulPanel.SetActive(false);
	}
	private void OpenQuitPanel()
	{
		_quitPanel.SetActive(true);
	}
	private void CloseQuitPanel()
	{
		_quitPanel.SetActive(false);
	}
	private void MoveMainScene()
	{
		LoadingManager.LoadScene("Title");
	}
}
