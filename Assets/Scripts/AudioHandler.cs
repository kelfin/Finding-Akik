using UnityEngine;
using System.Collections;

public class AudioHandler : MonoBehaviour {
    public AudioClip bgsound;
	// Use this for initialization
	void Start () {
        MakeSound(bgsound);
	}
    private void MakeSound(AudioClip originalClip)
    {
        // As it is not 3D audio clip, position doesn't matter.
        AudioSource.PlayClipAtPoint(originalClip, transform.position);
    }
}
