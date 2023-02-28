using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    public List<GameObject> particleSystems;
    public List<GameObject> PSpool;
    public List<GameObject> activated;
    public List<GameObject> deactivated;
    public int psCount;
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < particleSystems.Count; i++)
        {
            for (int x = 0; x < psCount; x++)
            {
                GameObject enemyCopy = Instantiate(particleSystems[i]);
                PSpool.Add(enemyCopy);
                enemyCopy.SetActive(false);
            }
        }
    }
}
