using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	// Use this for initialization
    public void mulai()
    {
        Application.LoadLevel("Level_1");
    }
    public void about()
    {
        Application.LoadLevel("About");
    }
    public void back()
    {
        Application.LoadLevel("MainMenu");
    }
    public void level2()
    {
        Application.LoadLevel("Level_2");
    }
    public void exit()
    {
        Application.Quit(); 
    }
}
