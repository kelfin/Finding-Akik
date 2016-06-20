using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HealthItem : MonoBehaviour
{
    public AudioClip sound;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "utama")
        {
            if (other.gameObject.GetComponent<HpHandler>().darah >= 100)
            {
                other.gameObject.GetComponent<HpHandler>().darah = 100;
            }
            else
            {
                other.gameObject.GetComponent<HpHandler>().darah += 20;
            }
            MakeSound(sound);
            Destroy(this.gameObject, 0);
        }
    }
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
