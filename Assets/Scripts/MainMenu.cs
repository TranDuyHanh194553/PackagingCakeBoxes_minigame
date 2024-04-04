using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public string levelToLoad = "LevelSelect";

	[SerializeField] SceneFader sceneFader;
	[SerializeField] GameObject instruction;
    [SerializeField] private AudioManager audioManager;

    public void Play()
	{
        audioManager.PlaySFX(audioManager.click);
        sceneFader.FadeTo(levelToLoad);
	}

	public void InsOn()
	{
        audioManager.PlaySFX(audioManager.click);
        instruction.SetActive(true);
	}

    public void InsOff()
    {
        audioManager.PlaySFX(audioManager.click);
        instruction.SetActive(false);
    }

}
