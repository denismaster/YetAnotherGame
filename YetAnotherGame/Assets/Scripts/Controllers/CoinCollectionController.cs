using UnityEngine;

public class CoinCollectionController : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == TagConstants.Coin && collision.gameObject.tag == TagConstants.Player)
        {
            Destroy(this.gameObject);
        }
    }
}
