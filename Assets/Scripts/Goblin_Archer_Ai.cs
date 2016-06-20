using UnityEngine;
using System.Collections;

public class Goblin_Archer_Ai : MonoBehaviour {

	// Use this for initialization
    private Vector3 posisiAwal;
    private bool maju_kiri;
    private int areaPatroli;
    private Animator anim;
    private Animator player_anim;
    private bool patrol;
    private GameObject player;
    public Transform arrowPrefab;
    public Transform hand;
    float CoolDown = 0;
    public float skala_model;
    public float darah = 100;
    private float darahAwal;
    Transform hpbar;
    GameObject hpbarobj;
	void Start () {
        darahAwal = darah;
        hpbar = transform.Find("darahbar");
        hpbarobj = hpbar.gameObject;
        posisiAwal = transform.position;
        maju_kiri = true;
        areaPatroli = 7;
        anim = GetComponent<Animator>();
        patrol = true;
        player = GameObject.Find("utama");
        player_anim = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if(transform.position.x - player.transform.position.x <= 7)
        {
            patrol = false;
            if (transform.localScale.x != -skala_model)
            {
                transform.localScale = new Vector3(-skala_model, skala_model, skala_model);
                maju_kiri = true;
            }
            CoolDown -= Time.deltaTime;
            if (CoolDown < 0)
            {
                anim.SetTrigger("attack");
                StartCoroutine(serang());
                CoolDown = 3;
            }
            else
            {
                anim.SetFloat("speed", 0);
            }
        }
        else
        {
            patrol = true;
        }

        if (patrol == true)
        {
            if (maju_kiri == true && posisiAwal.x - areaPatroli < transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
                anim.SetFloat("speed", 2);
            }
            else if (maju_kiri == true && posisiAwal.x - areaPatroli > transform.position.x)
            {
                Flip();
            }
            else if (maju_kiri == false && posisiAwal.x + areaPatroli > transform.position.x)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
                anim.SetFloat("speed", 2);
            }
            else if (maju_kiri == false && posisiAwal.x + areaPatroli < transform.position.x)
            {
                Flip();
            }
        }
        if (darah > (darahAwal * 0.5) && darah <= (darahAwal * 0.75))
        {
            if (hpbarobj.transform.Find("darahbar_4"))
            {
                Destroy(hpbarobj.transform.Find("darahbar_4").gameObject);
            }
        }
        if (darah > (darahAwal * 0.25) && darah <= (darahAwal * 0.5))
        {
            if (hpbarobj.transform.Find("darahbar_3"))
            {
                Destroy(hpbarobj.transform.Find("darahbar_3").gameObject);
            }
        }
        if (darah > 0 && darah <= (darahAwal * 0.25))
        {
            if (hpbarobj.transform.Find("darahbar_2"))
            {
                Destroy(hpbarobj.transform.Find("darahbar_2").gameObject);
            }
        }
        if (darah <= 0)
        {
            if (hpbarobj.transform.Find("darahbar_1"))
            {
                Destroy(hpbarobj.transform.Find("darahbar_1").gameObject);
            }
            Destroy(gameObject);
        }
	}
    public void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
        maju_kiri = !maju_kiri;
    }
    IEnumerator serang()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(arrowPrefab, hand.position, Quaternion.Euler(0, 0, 90));
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (player_anim.GetCurrentAnimatorStateInfo(0).IsName("utama_attack"))
        {
            darah -= 30;
        }
        if (player_anim.GetCurrentAnimatorStateInfo(0).IsName("utama_attack2"))
        {
            darah -= 30;
        }
    }
}
