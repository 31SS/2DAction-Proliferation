using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoilBlock : MonoBehaviour, IEventBlock
{
    public AudioClip breakBlock;

    public void HappenEvent(UnityChan2DController player)
    {
        PointController.instance.AddSoil();
        AudioSourceController.instance.PlayOneShot(breakBlock);
        Destroy(gameObject);
    }
}
