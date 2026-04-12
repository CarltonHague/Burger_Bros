using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    public InputAction quitAction;


    public void exitGame(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            quitGame();
        }
    }

    private void quitGame()
    {
        Application.Quit();
        //if (Application.isEditor)
        //{
        //    UnityEditor.EditorApplication.isPlaying = false;
        //}
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
