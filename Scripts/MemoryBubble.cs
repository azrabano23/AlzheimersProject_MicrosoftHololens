using UnityEngine;
using UnityEngine.Video;
using Microsoft.MixedReality.Toolkit.Input;

public class MemoryBubble : MonoBehaviour, IMixedRealityPointerHandler
{
    public string clipUrl;
    private VideoPlayer videoPlayer;
    private bool isPlaying = false;


    void Start()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        videoPlayer.url = clipUrl;
        videoPlayer.playOnAwake = false;
        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";


        Debug.Log("Video URL assigned: " + clipUrl);
    }

    public void PlayVideo()
    {   

        if (!string.IsNullOrEmpty(clipUrl))
        {
            if (isPlaying) {
                videoPlayer.Pause();
                isPlaying = false;
                Debug.Log("Video paused");
            } else
            {
                videoPlayer.Play();
                isPlaying = true;
                Debug.Log("Video started!");
            }

        }
        else
        {
            Debug.LogWarning("Clip URL is empty!");
        }
    }

    // Called when the bubble is clicked or pinched
    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        Debug.Log("Bubble clicked!");
        PlayVideo();
    }

    // Required interface methods
    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}
