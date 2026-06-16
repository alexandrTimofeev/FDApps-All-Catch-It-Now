using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button Play;
    [SerializeField] private Button Exit;

    private void Awake()
    {
        Play.onClick.AddListener(StartGame);
        Exit.onClick.AddListener(Quit);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Demo 2");
    }

    private void Quit()
    {
        Application.Quit();
    }
}
