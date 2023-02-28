using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHelperFX : MonoBehaviour
{
	public GameObject helperFX;
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Hello");
		helperFX.SetActive(true);
	}
	private void OnTriggerExit(Collider other)
	{
		helperFX.SetActive(false);
	}
}
