using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject completeLevelUI;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject star1, star2, star3;
    [SerializeField] private AudioManager audioManager;

    private void Start()
    {
        GameIsOver = false;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        audioManager.PlaySFX(audioManager.timeUp);
        gameObject.SetActive(false);

        gameOverUI.SetActive(true);
    }

    public void Victory()
    {
        completeLevelUI.SetActive(true);
        audioManager.PlaySFX(audioManager.win);

        if (timer.remainTime < 15)
            { star1.SetActive(true); }
        else if (15 < timer.remainTime && timer.remainTime < 30)
            { star2.SetActive(true); }
        else
            { star3.SetActive(true); }

        StartCoroutine(waitingfor());
    }

    IEnumerator waitingfor()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0.0f;
    }    
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

}
    

