using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlHandler : MonoBehaviour {
    private Vector2 movement;
    public Animator anim;
    public bool lookRight = true;
    float loncat = 0;
    public AudioClip suara_hit;
	void Update ()
    {
        float inputX = Input.GetAxis("Horizontal");
        movement = new Vector2(7 * inputX, -5);
        if (Input.GetButtonDown("Jump") && (loncat <= 0))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 7000));
            anim.SetTrigger("loncat");
            loncat = 1;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("utama_attack"))
            {
                anim.SetTrigger("attack");
                MakeSound(suara_hit);
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("utama_attack2"))
            {
                anim.SetTrigger("attack2");
                MakeSound(suara_hit);
            }
        }
        if (Input.GetButtonDown("Fire3"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("utama_guard"))
            {
                anim.SetTrigger("guard");
            }
        }
        if (lookRight)
        {
            anim.SetFloat("speed", inputX);
        }
        else
        {
            anim.SetFloat("speed", inputX*-1);
        }
        if ((inputX > 0.1) && (lookRight == false))
        {
            Flip();
        }
        if ((inputX < -0.1) && (lookRight == true))
        {
            Flip();
        }
        if (loncat > 0)
        {
            loncat -= Time.deltaTime;
        }
	}
    public void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        lookRight = !lookRight;
    }
    void FixedUpdate() 
    {
        GetComponent<Rigidbody2D>().velocity = movement;
    }
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
