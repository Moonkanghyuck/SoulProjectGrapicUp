using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillUI : MonoBehaviour
{
	[SerializeField]
	private Image _SkillMLB;
	[SerializeField]
	private Image _SkillMRB;
	[SerializeField]
	private Image _SKillE;
	[SerializeField]
	private Image _SkillR;

	[SerializeField]
	private TextMeshProUGUI _skillMLBText;
	[SerializeField]
	private TextMeshProUGUI _skillMRBText;
	[SerializeField]
	private TextMeshProUGUI _skillEText;
	[SerializeField]
	private TextMeshProUGUI _skillRText;

	public void Setting(IMonster monster)
	{
		_skillMLBText.text = monster.CheckCanMLB() ? "MLB" : "LV.1";
		_skillMRBText.text = monster.CheckCanMRB() ? "MRB" : "LV.10";
		_skillEText.text = monster.CheckCanE() ? "E" : "LV.30";
		_skillRText.text = monster.CheckCanR() ? "R" : "LV.50";

		_SkillMLB.fillAmount = monster.CoolTimeMLB;
		_SkillMRB.fillAmount = monster.CoolTimeMRB;
		_SKillE.fillAmount = monster.CoolTimeE;
		_SkillR.fillAmount = monster.CoolTimeR;

		gameObject.SetActive(true);
	}

	public void NoneSetting()
	{
		gameObject.SetActive(false);
	}
}
