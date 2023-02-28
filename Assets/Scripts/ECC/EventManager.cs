using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	public static Action OnActivateLane1Enemy;
	public static Action<GameObject> OnTargetSelectLane1;
	public static Action<float,float> OnXPIncrease;
	public static Action<float> OnStageIncrease;
	public static Action<GameObject,GameObject> OnBulletHit;
	public static Action<GameObject> OnFinish;
	public static Action OnShoot;
	public static Action OnWeaponBonus;
	public static Action<float> OnHeartBonus;
	public static Action OnEnergyBonusPicked;
	public static Action OnPlayerSpeedIncrease;
	public static Action OnPlayerSpeedReset;
	public static Action OnWeaponBonusDeactivate;
	public static Action<GameObject> OnPSEffectDeactivated;
	public static Action<Vector3> OnPSSpawn;
	public static Action<float> OnPlayerDamage;
	public static Action<GameObject> OnEnemyDieTrigger;
	public static Action OnSpecialAbilityFire;
	public static Action<float> OnFireRateOne;
	public static Action<float> OnFireRateTwo;
	public static Action<int> OnScoreUpdate;
	public static Action<float> OnFullHPBonus;
	public static Action<int> OnCharacterSelect;

	//Sounds
	public static Action OnCharacterBuySound;
	public static Action<int> OnMuteSound;
	public static Action OnEnemyHitSOund;
	public static Action OnBonusHitSound;
	public static Action OnSpecialSound;
}
