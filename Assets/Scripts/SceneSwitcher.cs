using UnityEngine;

public class SceneSwitcher : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager.Instance.SwitchScene(Screens.MainMenu);
    }

    public void OfflinePlay1P()
    {
        GameManager.Instance.SwitchScene(Screens.OfflinePlay1P);
    }

    public void OfflinePlay2P()
    {
        GameManager.Instance.SwitchScene(Screens.OfflinePlay2P);
    }

    public void OnlinePlay()
    {
        GameManager.Instance.SwitchScene(Screens.OnlinePlay);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }
}
