using UnityEngine;
using System.Collections;

public class AiBos : MonoBehaviour {
    float timeLeft = 3;
    public Animator dragon;
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            dragon.SetBool("is_attack", true);
            //Debug.Log("hit");
        }
        else
        {
            dragon.SetBool("is_attack", false);
        }
	}
}
