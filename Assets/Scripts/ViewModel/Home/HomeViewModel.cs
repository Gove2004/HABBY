using GoveKits.Runtime.Core;
using GoveKits.Runtime.UI;


public class HomeViewModel : ViewModel
{
    public void StartGame()
    {
        SceneCore.Load("Select", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        SceneCore.UnloadAsync("Home");
    }
}
