using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeactivate : MonoBehaviour
{
	private void OnParticleSystemStopped()
	{
		EventManager.OnPSEffectDeactivated?.Invoke(gameObject);
	}
}
