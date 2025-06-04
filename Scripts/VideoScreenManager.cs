using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Microsoft.MixedReality.Toolkit.Input;

public class VideoScreenManager : MonoBehaviour, IMixedRealityPointerHandler
{
    public GameObject videoPanel;         // The fullscreen panel
    public RawImage videoScreen;          // The UI display element
    public VideoPlayer screenPlayer;      // The VideoPlayer component

    // Called from memory bubble
    public void PlayClip(string url)
    {
        videoPanel.SetActive(true);
        screenPlayer.url = url;
        screenPlayer.Play();
        Debug.Log("▶ Playing video on fullscreen panel.");
    }

    // Called by close button
    public void ClosePanel()
    {
        screenPlayer.Stop();
        videoPanel.SetActive(false);
        Debug.Log("❌ Closed fullscreen panel.");
    }

    // Play/pause toggle on click
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        if (!screenPlayer.isPrepared) return;

        if (screenPlayer.isPlaying)
        {
            screenPlayer.Pause();
            Debug.Log("⏸ Paused video.");
        }
        else
        {
            screenPlayer.Play();
            Debug.Log("▶ Resumed video.");
        }
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}
