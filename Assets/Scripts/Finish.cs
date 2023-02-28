using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("BonusWeapon") || 
			other.CompareTag("BonusHeart") || 
			other.CompareTag("BonusEnergy"))
		{
			EventManager.OnFinish?.Invoke(other.gameObject);
		}
		if (other.CompareTag("Enemy"))
		{
			EventManager.OnEnemyDieTrigger?.Invoke(other.gameObject);
		}
	}
}
