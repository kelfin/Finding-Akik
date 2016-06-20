using UnityEngine;
using System.Collections;

public class PickScroll : MonoBehaviour {
    public AudioClip snd;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "utama")
        {
            MakeSound(snd);
            Destroy(gameObject, 0.75f);
            Application.LoadLevel("next_level2");
        }
    }
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
