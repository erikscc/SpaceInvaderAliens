using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public Animator anim;
    public bool fly;
	private void OnEnable()
	{
	
        if (fly == true)
        {
            anim.SetBool("Fly", true);

        }
        else anim.SetBool("Walk",true);
    }
	private void OnDisable()
	{
        if (fly == true)
        {
            anim.SetBool("Fly", false);

        }
        else anim.SetBool("Walk", false);
    }

	
}
