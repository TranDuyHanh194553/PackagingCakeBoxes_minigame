using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pause;

    public string menuSceneName = "LevelSelect";

    [SerializeField] private SceneFader sceneFader;

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle();
		}
	}

	public void Toggle()
	{
		pause.SetActive(!pause.activeSelf);

		if (pause.activeSelf)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	public void Retry()
	{
		gameObject.SetActive(false);
		Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);

	}

	public void Menu()
	{
		gameObject.SetActive(false);
		Toggle();
		sceneFader.FadeTo(menuSceneName);
	}

}
