using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public void FadeTo (string scene)
	{
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(scene);
        Time.timeScale = 1.0f;
    }

}
