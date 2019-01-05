using UnityEngine;

public class CoinCounterController : MonoBehaviour
{
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            score += 1;
        }
    }

    public int getScore()
    {
        return score;
    }
}
