using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        SceneManager.LoadScene("welcome to hell");
    }

    public void Quit()
    {
        Application.Quit();
    }

    void Start()
    {
        var obj = FindAnyObjectByType<StatsSaver>();

        // ten obiekt może być nullem ale nie powinien
        // ma dane o wszystkich statystykach z chwili kiedy gracz umarł
    }
}
