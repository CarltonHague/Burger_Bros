using UnityEngine;
using UnityEngine.UI;
public class Player_Coins : MonoBehaviour
{
    public Text coinText;
    private int coins = 0;

    private const float delay = 0.001f;
    private bool currentlyCollecting = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SeasameSeed")
        {
            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance < 2f)
            {
                if (!currentlyCollecting)
                {
                    collectCoin();
                }
            }
        }
    }

    private void collectCoin()
    {
        currentlyCollecting = true;
        coins++;
        coinText.text = ": " + coins.ToString();

        Invoke(nameof(endCollection), delay);
    }

    private void endCollection()
    {
        currentlyCollecting = false;
    }
}
