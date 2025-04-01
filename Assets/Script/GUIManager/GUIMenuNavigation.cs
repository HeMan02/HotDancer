using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIMenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttonReturnMainMenu;
    public GameObject buttonQuit;
    void Start()
    {
        if (buttonReturnMainMenu)
            buttonReturnMainMenu.SetActive(false);
        if (buttonQuit)
            buttonQuit.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (buttonReturnMainMenu)
                buttonReturnMainMenu.SetActive(true);
            if (buttonQuit)
                buttonQuit.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void ReturnInGame()
    {
        if (buttonReturnMainMenu)
            buttonReturnMainMenu.SetActive(false);
        if (buttonQuit)
            buttonQuit.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
