using UnityEngine;
using System.Collections;

public class Kapak_Hit : MonoBehaviour {
    public Animator anim;
    private Animator player;
    void Start()
    {
        player = GameObject.Find("utama").GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            if (other.gameObject.name == "utama")
            {
                if (!player.GetCurrentAnimatorStateInfo(0).IsName("utama_guard"))
                {
                    other.gameObject.GetComponent<HpHandler>().darah -= 8;
                }
            }
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("orc_lempar"))
        {
            if (other.gameObject.name == "utama")
            {
                if (!player.GetCurrentAnimatorStateInfo(0).IsName("utama_guard"))
                {
                    other.gameObject.GetComponent<HpHandler>().darah -= 12;
                }
            }
        }
    }
}
