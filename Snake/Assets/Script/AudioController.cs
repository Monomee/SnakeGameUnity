using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource musicTheme;
    public GameObject audioOn;
    public GameObject audioOff;
    private bool isOn = true; // Trạng thái ban đầu: bật

    private void OnMouseDown()
    {
        Debug.Log("Mouse Clicked on: " + gameObject.name);
        isOn = !isOn;

        if (isOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    private void TurnOn()
    {
        Debug.Log("Audio Turned ON!");
        musicTheme.Play();
        audioOff.SetActive(false);
        audioOn.SetActive(true);
    }

    private void TurnOff()
    {
        Debug.Log("Audio Turned OFF!");
        musicTheme.Pause();
        audioOff.SetActive(true);
        audioOn.SetActive(false);
    }
}
