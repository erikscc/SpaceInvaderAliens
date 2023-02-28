using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
	public GameObject pause;
	public List<Sprite> thumbnails;
	public Image playerIcon;
	public TMP_Text scoreText;
	public GameObject energyPanel;
	public GameObject weaponPanel;
	public Button fireButton1;
	public Button fireButton2;
	public Slider energySlider;
    public Slider xpSlider;
	public Slider playerHP;
	public Slider weaponBonusTimer;
    public TMP_Text stageCounter;
	public Image MuteSoundBG;
	public Image OnSoundBG;
	private List<IEnumerator> timer=new();
	
	// Start is called before the first frame update
	private void OnEnable()
	{
		EventManager.OnXPIncrease += UpdateXP;
		EventManager.OnStageIncrease += StageIncrease;
		EventManager.OnWeaponBonus += WeaponBonusActivated;
		EventManager.OnPlayerDamage += PlayerDamageTaken;
		EventManager.OnHeartBonus += PlayerHPIncrease;
		EventManager.OnFireRateOne += FireRateOne;
		EventManager.OnFireRateTwo += FireRateTwo;
		EventManager.OnEnergyBonusPicked += EnergyTimerStart;
		EventManager.OnScoreUpdate += ScoreUpdate;
		EventManager.OnFullHPBonus += PlayerHPIncrease;
		EventManager.OnMuteSound += MuteSound;
	}
	private void OnDisable()
	{
		EventManager.OnXPIncrease -= UpdateXP;
		EventManager.OnStageIncrease -= StageIncrease;
		EventManager.OnWeaponBonus -= WeaponBonusActivated;
		EventManager.OnPlayerDamage -= PlayerDamageTaken;
		EventManager.OnHeartBonus -= PlayerHPIncrease;
		EventManager.OnFireRateOne -= FireRateOne;
		EventManager.OnFireRateTwo -= FireRateTwo;
		EventManager.OnEnergyBonusPicked -= EnergyTimerStart;
		EventManager.OnScoreUpdate -= ScoreUpdate;
		EventManager.OnFullHPBonus -= PlayerHPIncrease;
		EventManager.OnMuteSound -= MuteSound;
	}

	

	private void Awake()
	{
		playerIcon.sprite=thumbnails[GameSettings.skinIndex];
		ScoreUpdate(0);
		xpSlider.minValue = 0f;
		playerHP.maxValue = 100f;
		playerHP.value = 100f;
		playerHP.minValue = 0f;
	}
	private void UpdateXP(float playerXP,float stageXP)
	{
		xpSlider.value = playerXP;
		xpSlider.maxValue = stageXP;
	}
	private void StageIncrease(float stage)
	{
		stageCounter.text = string.Format("{0}",stage);
	}
	private void WeaponBonusActivated()
	{
		weaponPanel.SetActive(true);
		weaponPanel.transform.localScale = Vector3.zero;
		LeanTween.scale(weaponPanel,new Vector3(1.5f,1.5f,1.5f),0.5f)
			.setEaseInOutBounce()
			.setOnComplete(()=> 
			{
				weaponPanel.transform.localScale = Vector3.one;
				weaponBonusTimer.enabled = true;
				weaponBonusTimer.maxValue = 1f;
				weaponBonusTimer.value = 1f;
				timer.Add(WeaponActivated());
				StartCoroutine(timer[timer.Count-1]);
			});
	}
	public IEnumerator WeaponActivated()
	{
		while (weaponBonusTimer.value>0)
		{
			weaponBonusTimer.value -= 0.01f;
			yield return new WaitForSeconds(0.1f);
		}
		EventManager.OnWeaponBonusDeactivate?.Invoke();
		RemoveWeaponBonus();
		LeanTween.scale(weaponPanel, Vector3.zero, 0.5f)
			.setEaseOutBounce()
			.setOnComplete(() =>
			{
				weaponPanel.SetActive(false);
			});
	}
	private void RemoveWeaponBonus()
	{
		StopCoroutine(timer[0]);
		timer.RemoveAt(0);
		weaponBonusTimer.enabled = false;
	}
	private void PlayerDamageTaken(float damage)
	{
		playerHP.value = damage;
		Debug.LogFormat("Damage taken is {0} / {1} HP",damage,playerHP.value);
	}
	private void PlayerHPIncrease(float hpIncrease)
	{
		playerHP.value = hpIncrease;
	}
	public void FireRateOne(float time)
	{
		StartCoroutine(FireRateButton1(time));
	}
	public void FireRateTwo(float time)
	{
		StartCoroutine(FireRateButton2(time));
	}
	public IEnumerator FireRateButton1(float time)
	{
		fireButton1.interactable = false;
		yield return new WaitForSeconds(time);
		fireButton1.interactable = true;
	}
	public IEnumerator FireRateButton2(float time)
	{
		fireButton2.interactable = false;
		yield return new WaitForSeconds(time);
		fireButton2.interactable = true;
	}
	private void EnergyTimerStart()
	{
		energyPanel.SetActive(true);
		energyPanel.transform.localScale = Vector3.zero;
		LeanTween.scale(energyPanel,new Vector3(1.5f,1.5f,1.5f),0.5f)
			.setEase(LeanTweenType.easeInOutBounce)
			.setOnComplete(()=> 
			{
				StartCoroutine(EnergyTimer());
				energyPanel.transform.localScale = Vector3.one;
			});
	}
	public IEnumerator EnergyTimer()
	{
		energySlider.maxValue = 1f;
		EventManager.OnPlayerSpeedIncrease?.Invoke();
		energySlider.minValue = 0f;
		energySlider.value = 1f;
		while (energySlider.value > 0)
		{
			yield return new WaitForSeconds(0.1f);
			energySlider.value -= 0.02f;
		}
		EventManager.OnPlayerSpeedReset?.Invoke();
		LeanTween.scale(energyPanel,Vector3.zero, 0.5f)
			.setEaseOutBounce()
			.setOnComplete(()=> 
			{ 
				energyPanel.SetActive(false);
			});
	}
	public void ScoreUpdate(int score)
	{
		Debug.Log("Score Updated");
		scoreText.text = string.Format("{0:0,0}",score);
	}
	private void MuteSound(int mode)
	{
		Debug.Log("Soundon off");
		if (mode!=0)
		{
			OnSoundBG.enabled = false;
			MuteSoundBG.enabled = true;
		} else
		{
			OnSoundBG.enabled = true;
			MuteSoundBG.enabled = false;
		}
	}
}
