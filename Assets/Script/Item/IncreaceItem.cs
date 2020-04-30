using UnityEngine;

public class IncreaceItem : MonoBehaviour, IPickupable {

    public GameObject originPlayer;
    public void PickedUp(UnityChan2DController player)
    {
        GetComponent<Collider2D>().enabled = false;
        Invoke("Increace", 0.5f);
    }

    private void Increace()
    {
        Instantiate(originPlayer, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
