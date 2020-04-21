using System.Collections;
using UnityEngine;

public class CoinController : MonoBehaviour , IPickupable
{
    public AudioClip getCoin;

    public void PickedUp(UnityChan2DController player)
    {
        PointController.instance.AddCoin();
        AudioSourceController.instance.PlayOneShot(getCoin);
        Destroy(gameObject);
    }
}
