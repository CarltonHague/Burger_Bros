using UnityEngine;
using UnityEngine.UI;
public class Player_Coins : MonoBehaviour
{
    public Text coinText;
    public static int coins = 0;
    public int coinsOnLevelStart = 0;


    private const float delay = 0.001f;
    private bool currentlyCollecting = false;

    void Start()
    {
        coins = StaticCoinData.coinAmount;    //keep old coins
        coinText.text = ": " + coins.ToString();  //update ui
    }


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
    public void resetCoins()
    {
        coins = coinsOnLevelStart;
        coinText.text = ": " + coins.ToString();
    }
    public void saveCoinsFromPreviousLevel()
    {
        coinsOnLevelStart = coins;
    }

    private void endCollection()
    {
        currentlyCollecting = false;
    }
}
