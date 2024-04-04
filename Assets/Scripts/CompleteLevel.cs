using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{

	public string menuSceneName = "LevelSelect";

	public string nextLevel = "Level2";
    [SerializeField] private int levelToUnlock = 2;

	[SerializeField] private SceneFader sceneFader;
	


	public void Continue()
	{
		PlayerPrefs.SetInt("levelReached", levelToUnlock);
		sceneFader.FadeTo(nextLevel);
	}

	public void Menu()
	{
		sceneFader.FadeTo(menuSceneName);
	}

}
