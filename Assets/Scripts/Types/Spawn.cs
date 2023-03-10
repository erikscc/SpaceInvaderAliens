using UnityEngine;
using System;

namespace ForestDefenseGame.Types
{
	/// <summary>
	/// This class defines an object that can be spawend, and its chance of apperance.
	/// </summary>
	[Serializable]
	public class Spawn
	{
		[Tooltip("The object to be spawned")]
		public Transform spawnObject;

		[Tooltip("How often the object appears. The minimum value is 1")]
		public int spawnChance = 1;
	}
}