using UnityEngine;

public class Endlevel : MonoBehaviour
{
    [SerializeField] string SceneYouWantToLoad;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(SceneYouWantToLoad);
        }
    }
}
    

