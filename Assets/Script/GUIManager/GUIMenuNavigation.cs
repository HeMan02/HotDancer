using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIMenuNavigation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buttonReturnMainMenu;
    void Start()
    {
        if (buttonReturnMainMenu)
            buttonReturnMainMenu.SetActive(false);
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (buttonReturnMainMenu)
                buttonReturnMainMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void StartScene()
    {
        SceneManager.LoadScene("CombatScene");
    }

    public void ReturnInGame()
    {
        buttonReturnMainMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
