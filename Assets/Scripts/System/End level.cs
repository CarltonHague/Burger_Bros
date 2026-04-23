using UnityEngine;
using UnityEngine.UI;

public class Endlevel : MonoBehaviour
{
    [SerializeField] string SceneYouWantToLoad;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeTime;
    private bool fading = false;

    private void Update()
    {
        if(fading)
        {
            Color currentColor = fadeImage.color;
            currentColor.a += Time.deltaTime / fadeTime;
            fadeImage.color = currentColor;

            if (currentColor.a >= 1f)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(SceneYouWantToLoad);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        fading = true;
    }
}
    

