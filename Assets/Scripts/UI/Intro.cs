using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour
{
    [SerializeField] string SceneYouWantToLoad;
    public float duration = 6f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke(nameof(changeScene), duration);
    }
 

    private void changeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneYouWantToLoad);
    }

}
