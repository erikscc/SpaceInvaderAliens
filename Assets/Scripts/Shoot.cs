using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
	public List<GameObject> ShootingPoint;
	public GameObject specialAbility;
	public ProjectilePool bullets;
	public int weaponCounter;
	// Start is called before the first frame update
	private void OnEnable()
	{
		EventManager.OnShoot += Shooting;
		EventManager.OnBulletHit += BulletHit;
		EventManager.OnWeaponBonus += WeaponBonus;
		EventManager.OnWeaponBonusDeactivate += WeaponBonusDeactivate;
		EventManager.OnSpecialAbilityFire += SpecialAbility;
	}
	private void OnDisable()
	{
		EventManager.OnSpecialAbilityFire -= SpecialAbility;
		EventManager.OnShoot -= Shooting;
		EventManager.OnBulletHit -= BulletHit;
		EventManager.OnWeaponBonus -= WeaponBonus;
		EventManager.OnWeaponBonusDeactivate -= WeaponBonusDeactivate;
	}
	private void Start()
	{
		weaponCounter = 1;
	}
	private void WeaponBonus()
	{
		weaponCounter++;
		if (weaponCounter > ShootingPoint.Count) weaponCounter = ShootingPoint.Count;
	
		Debug.Log("Bonus Weapon activated");
	}
	private void WeaponBonusDeactivate()
	{
		weaponCounter--;	
		if (weaponCounter < 1) weaponCounter = 1;
		
	}

	private void BulletHit(GameObject bullet,GameObject hitObject)
	{
		if (!bullet.CompareTag("SpecialAbility"))
		{
			bullets.Despawn(bullet);	
		}
	}

	private void Shooting()
	{
		Debug.Log("Shooting");
		
		for (int i = 0; i < weaponCounter; i++)
		{
			bullets.Spawn(ShootingPoint[i].transform.position);
		}

	}
	private void SpecialAbility()
	{
		specialAbility.transform.position= ShootingPoint[0].transform.position;
		specialAbility.SetActive(true);

	}
	


}
