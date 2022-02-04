using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitManager : MonoBehaviour
{
    // pressing the quit button ends the game
    public void QuitGame()
    {
        Application.Quit();
    }
}
