using UnityEngine;
using UnityEngine.UI;

public class CoinsCouterView : MonoBehaviour
{
    CoinCounterController viewScore = new CoinCounterController();
    private Text scoreText;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Coins: " + viewScore.getScore();
    }
}
