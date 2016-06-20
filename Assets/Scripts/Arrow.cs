using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public Vector3 v = new Vector3(20, 20, 0);
	public Vector3 a = new Vector3(0, -10, 0);
    public bool right = false;
    public AudioClip suara_panah;
    private Animator player;
	void Start () {
        player = GameObject.Find("utama").GetComponent<Animator>();
		Destroy(this.gameObject, 10);
        if (!right)
            v.x = -v.x;
        MakeSound(suara_panah);
	}

	void Update () {
	
		transform.position += v*Time.deltaTime;
		v += a * Time.deltaTime;
        
       transform.rotation = Quaternion.LookRotation(v, new Vector3(0,0,-1));
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "utama")
        {
            Destroy(this.gameObject, 0);

            if (!player.GetCurrentAnimatorStateInfo(0).IsName("utama_guard"))
            {
                other.gameObject.GetComponent<HpHandler>().darah -= 5;
            }
        }
    }
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
