using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public AudioSource buyCharacter;
	public AudioSource shoot;
	public AudioSource shootSecond;
	public AudioSource shootThird;
	public AudioSource shootFour;
	public AudioSource explosion;
	public AudioSource explosionSecond;
	public AudioSource explosionThird;
	public AudioSource explosionFour;
	public AudioSource bonusHit;
	public AudioSource specialAbility;
	private void OnEnable()
	{
		EventManager.OnCharacterBuySound += CharacterBuySound;
		EventManager.OnShoot += Shooting;
		EventManager.OnEnemyHitSOund += EnemyHit;
		EventManager.OnBonusHitSound += BonusHitSound;
		EventManager.OnSpecialSound += SpecialSound;
	}
	private void OnDisable()
	{
		EventManager.OnCharacterBuySound -= CharacterBuySound;
		EventManager.OnShoot -= Shooting;
		EventManager.OnEnemyHitSOund -= EnemyHit;
		EventManager.OnBonusHitSound -= BonusHitSound;
		EventManager.OnSpecialSound -= SpecialSound;
	}

	private void SpecialSound()
	{
		specialAbility.Play();
	}

	private void EnemyHit()
	{
		if (!explosion.isPlaying)
		{
			Debug.Log("1");
			explosion.Play();
		}
		else
		{
			if (!explosionSecond.isPlaying && explosion.isPlaying)
			{
				Debug.Log("2");
				explosionSecond.Play();
			}
			else
			{
				if (explosionSecond.isPlaying && explosion.isPlaying && !explosionThird.isPlaying)
				{
					Debug.Log("3");
					explosionThird.Play();
				}
				else
				{
					if (explosionSecond.isPlaying && explosion.isPlaying &&
						explosionThird.isPlaying && !explosionFour.isPlaying)
					{
						Debug.Log("4");
						explosionFour.Play();
					}
				}
			}
		}
	}

	private void Shooting()
	{
		if (!shoot.isPlaying)
		{
			Debug.Log("1");
			shoot.Play();
		}
		else
		{
			if (!shootSecond.isPlaying && shoot.isPlaying)
			{
				Debug.Log("2");
				shootSecond.Play();
			} else
			{
				if (shootSecond.isPlaying && shoot.isPlaying && !shootThird.isPlaying)
				{
					Debug.Log("3");
					shootThird.Play();
				} else
				{
					if (shootSecond.isPlaying && shoot.isPlaying && 
						shootThird.isPlaying && !shootFour.isPlaying)
					{
						Debug.Log("4");
						shootFour.Play();
					}
				}
			}
		}
	}

	private void CharacterBuySound()
	{
		buyCharacter.Play();
	}
	public void BonusHitSound() 
	{
		bonusHit.Play();
	}
}
