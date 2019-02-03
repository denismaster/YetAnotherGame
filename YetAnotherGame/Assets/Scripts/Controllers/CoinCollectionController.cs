using UnityEngine;

public class CoinCollectionController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == TagConstants.Coin && collision.gameObject.tag == TagConstants.Player)
        {
            SoundManagerController.Play(SoundConstants.CoinCollect);
            Destroy(this.gameObject);
        }
    }
}
