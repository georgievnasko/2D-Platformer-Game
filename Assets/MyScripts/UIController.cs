using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {

    public Image[] healthImages = new Image[3];
    public Text gameOverText;
    public Button retryBtn;

    private const string FINISHED = "FINISHED!";
    private const string GAME_OVER = "GAME OVER";

    void Update () 
    {
        if (PlayerScript.health < healthImages.Length && PlayerScript.health >= 0)
        {
            healthImages[PlayerScript.health].gameObject.SetActive(false);
        }

        gameOverText.text = PlayerScript.finished ? FINISHED : GAME_OVER;

        gameOverText.gameObject.SetActive(!PlayerScript.playing);
        retryBtn.gameObject.SetActive(!PlayerScript.playing);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
}
