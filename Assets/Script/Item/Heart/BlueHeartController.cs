using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueHeartController : MonoBehaviour, IGetableBlueHeart
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotBlueHeart()
    {
        if (GameManager.Instance.currentState == GameManager.GameState.Playing_Heart1)
        {
            GameManager.Instance.dispatch(GameManager.GameState.Clear);
            return;
        }
        GameManager.Instance.dispatch(GameManager.GameState.Playing_Heart1);
    }
}
