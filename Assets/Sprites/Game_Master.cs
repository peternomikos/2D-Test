using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Master : MonoBehaviour
{
    public Pause_Menu pause;
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            pause.onPauseMenu = !pause.onPauseMenu;
        }
    }
}
