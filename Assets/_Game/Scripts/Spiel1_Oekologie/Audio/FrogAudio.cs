using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Modules.SoundManager;
using UnityEngine;

/// <summary>
/// Description:    This class handles the audio witch is played on the radio.\n
/// Author:         Konietzka\n
/// </summary>
public class FrogAudio : MonoBehaviour
{
    /// <summary>
    /// Description:    Play the audio on enter select.\n
    /// Author:         Konietzka\n
    /// Args:           None\n
    /// Returns:        None\n
    /// </summary>
    public void OnSelect()
    {
        Debug.Log("Grabbed");
        SoundManager.GetInstance.StartSfx("frog");
    }

    /// <summary>
    /// Description:    Stop the audio on exit select.\n
    /// Author:         Konietzka\n
    /// Args:           None\n
    /// Returns:        None\n
    /// </summary>
    public void OnDrop()
    {
        Debug.Log("Fallen lassen");
        SoundManager.GetInstance.StopSfx("frog");
    }

}
