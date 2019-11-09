using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStatus
{
    idle,
}

public class GameStatusManager : MonoBehaviour
{
    public static GameStatusManager Instance;

    private void Awake()
    {
        Instance = this;
    }


}
