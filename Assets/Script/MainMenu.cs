using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void CreateGame(string CreateName)
    {
        Debug.Log("CREATE");
        SceneManager.LoadScene(CreateName);

    }

    public void JoinGame(string JoinName)
    {
        Debug.Log("JOIN");
        SceneManager.LoadScene(JoinName);

    }
    public void LeaderboardGame(string LeaderboardName)
    {
        SceneManager.LoadScene(LeaderboardName);

    }
    public void AchievementGame(string AchievementName)
    {
        SceneManager.LoadScene(AchievementName);

    }
    public void LoginGame(string LoginPage)
    {
        SceneManager.LoadScene(LoginPage);
    }
    public void SettingGame(string SettingName)
    {
        SceneManager.LoadScene(SettingName);

    }
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

    }
}


