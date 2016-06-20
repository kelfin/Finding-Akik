using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HpHandler : MonoBehaviour {

	// Use this for initialization
    public float darah;
    private Slider HpLevel;
	void Start () {
        HpLevel = GameObject.Find("HPbar").GetComponent<Slider>();
	}
    void Update()
    {
        HpLevel.value = darah;
        if (HpLevel.value <= 0)
        {
            Application.LoadLevel("GameOver");
        }
    }
}
