using UnityEngine;
using System.Collections;

public class PickRing : MonoBehaviour {
    public AudioClip snd;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "utama")
        {
            MakeSound(snd);
            Destroy(gameObject, 0.70f);
            Application.LoadLevel("tamat");
        }
    }
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}