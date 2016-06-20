using UnityEngine;
using System.Collections;
using DotFuzzy;
public class Orc_Bos : MonoBehaviour {

    public float skala_model;
    private Vector3 posisiAwal;
    private bool maju_kiri;
    private int areaPatroli;
    private Animator anim;
    private bool patrol;
    private GameObject player;
    float CoolDown = 0;
    public AudioClip hit_sound;
    private float darah = 100;
    private float darahAwal;
    Transform hpbar;
    GameObject hpbarobj;
    public Transform anak;
    public Transform kapak;
    private Animator player_anim;
    //DotFuzzy Deklarasi
    LinguisticVariable HealthPoint;
    LinguisticVariable Jarak;
    LinguisticVariable Aksi;
    FuzzyEngine fuzzyEngine; 
    void Start()
    {
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

        //Fuzzy
        fuzzyEngine = new FuzzyEngine(); 
        //Fuzzy Tambah Fungsi Keanggotaan HP
        HealthPoint = new LinguisticVariable("HealthPoint"); 
        HealthPoint.MembershipFunctionCollection.Add(new MembershipFunction("SangatRendah", 0, 0, 0, 20));
        HealthPoint.MembershipFunctionCollection.Add(new MembershipFunction("Rendah", 15, 30, 30, 45));
        HealthPoint.MembershipFunctionCollection.Add(new MembershipFunction("Setengah", 35, 50, 50, 65));
        HealthPoint.MembershipFunctionCollection.Add(new MembershipFunction("Tinggi", 55, 70, 70, 85));
        HealthPoint.MembershipFunctionCollection.Add(new MembershipFunction("SangatTinggi", 80, 100, 100, 100));
        //Fuzzy Tambah Fungsi Keanggotaan Jarak
        Jarak = new LinguisticVariable("Jarak"); 
        Jarak.MembershipFunctionCollection.Add(new MembershipFunction("SangatDekat", 0, 0, 0, 20));
        Jarak.MembershipFunctionCollection.Add(new MembershipFunction("Dekat", 15, 30, 30, 50));
        Jarak.MembershipFunctionCollection.Add(new MembershipFunction("Jauh", 40, 60, 60, 80));
        Jarak.MembershipFunctionCollection.Add(new MembershipFunction("SangatJauh", 70, 100, 100, 100));
        //Fuzzy Tambah Fungsi Keanggotaan Aksi
        Aksi = new LinguisticVariable("Aksi"); 
        Aksi.MembershipFunctionCollection.Add(new MembershipFunction("PanggilAnak", 0, 0, 0, 25));
        Aksi.MembershipFunctionCollection.Add(new MembershipFunction("Lempar", 20, 50, 50, 70));
        Aksi.MembershipFunctionCollection.Add(new MembershipFunction("Attack", 60, 80, 80, 100));

        fuzzyEngine.LinguisticVariableCollection.Add(HealthPoint);
        fuzzyEngine.LinguisticVariableCollection.Add(Jarak);
        fuzzyEngine.LinguisticVariableCollection.Add(Aksi);
        fuzzyEngine.Consequent = "Aksi";
        //Fuzzy Rule
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatRendah) AND (Jarak IS SangatDekat) THEN Aksi IS PanggilAnak"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatRendah) AND (Jarak IS Dekat) THEN Aksi IS PanggilAnak"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatRendah) AND (Jarak IS Jauh) THEN Aksi IS PanggilAnak"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatRendah) AND (Jarak IS SangatJauh) THEN Aksi IS PanggilAnak"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Rendah) AND (Jarak IS SangatDekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Rendah) AND (Jarak IS Dekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Rendah) AND (Jarak IS Jauh) THEN Aksi IS Lempar"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Rendah) AND (Jarak IS SangatJauh) THEN Aksi IS Lempar"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Setengah) AND (Jarak IS SangatDekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Setengah) AND (Jarak IS Dekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Setengah) AND (Jarak IS Jauh) THEN Aksi IS Lempar"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Setengah) AND (Jarak IS SangatJauh) THEN Aksi IS Lempar"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Tinggi) AND (Jarak IS SangatDekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Tinggi) AND (Jarak IS Dekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Tinggi) AND (Jarak IS Jauh) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS Tinggi) AND (Jarak IS SangatJauh) THEN Aksi IS Lempar"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatTinggi) AND (Jarak IS SangatDekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatTinggi) AND (Jarak IS Dekat) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatTinggi) AND (Jarak IS Jauh) THEN Aksi IS Attack"));
        fuzzyEngine.FuzzyRuleCollection.Add(new FuzzyRule("IF (HealthPoint IS SangatTinggi) AND (Jarak IS SangatJauh) THEN Aksi IS Lempar"));
    }
    void Update()
    {
        if (transform.position.x - player.transform.position.x - 3.4 <= 7)
        {
            patrol = false;
            if (transform.localScale.x != -skala_model)
            {
                transform.localScale = new Vector3(-skala_model, skala_model, skala_model);
                maju_kiri = true;
            }
            //if (transform.position.x - player.transform.position.x - 3.4 > 6)
            //{
            //    GetComponent<Rigidbody2D>().velocity = new Vector2(-2, 0);
            //}
            //else
            //{
                CoolDown -= Time.deltaTime;
                if (CoolDown < 0)
                {
                    HealthPoint.InputValue = player.GetComponent<HpHandler>().darah;
                    if (((transform.position.x - player.transform.position.x - 3.4) * 10) > 100)
                    {
                        Jarak.InputValue = 100;
                    }
                    else
                    {
                        Jarak.InputValue = (transform.position.x - player.transform.position.x) * 10;
                    }
                    if ((fuzzyEngine.Defuzzify() >= 0) && (fuzzyEngine.Defuzzify() <= 10))
                    {
                        Instantiate(anak, kapak.position, Quaternion.identity);
                    }
                    else if ((fuzzyEngine.Defuzzify() >= 11) && (fuzzyEngine.Defuzzify() <= 70))
                    {
                        anim.SetTrigger("lempar");
                        StartCoroutine(play_hit_sound());
                    }
                    else if ((fuzzyEngine.Defuzzify() >= 71) && (fuzzyEngine.Defuzzify() <= 100))
                    {
                        anim.SetTrigger("attack");
                        StartCoroutine(play_hit_sound());
                    }
                    else
                    {
                        anim.SetTrigger("attack");
                        StartCoroutine(play_hit_sound());
                    }
                    CoolDown = 3;
                }
                else
                {
                    //GameObject.Find("orc_weapon").GetComponent<PolygonCollider2D>().enabled = true;
                    anim.SetFloat("speed", 0);
                }
            //}
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
    IEnumerator play_hit_sound()
    {
        yield return new WaitForSeconds(0.7f);
        AudioSource.PlayClipAtPoint(hit_sound, transform.position);
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (player_anim.GetCurrentAnimatorStateInfo(0).IsName("utama_attack"))
        {
            darah -= 10;
        }
        if (player_anim.GetCurrentAnimatorStateInfo(0).IsName("utama_attack2"))
        {
            darah -= 10;
        }
    }
}
