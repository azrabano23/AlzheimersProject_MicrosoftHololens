using UnityEngine;//newest
using UnityEngine.Video;
using Microsoft.MixedReality.Toolkit.Input;

public class MemoryBubble : MonoBehaviour, IMixedRealityPointerHandler
{
    public string clipUrl;
    public VideoScreenManager screenManager;

    public void OnPointerClicked(MixedRealityPointerEventData eventData)
    {
        screenManager.PlayClip(clipUrl);  // This should open the fullscreen panel
        Debug.Log("🟣 Bubble clicked: opening fullscreen video.");
    }

    public void OnPointerDown(MixedRealityPointerEventData eventData) { }
    public void OnPointerUp(MixedRealityPointerEventData eventData) { }
    public void OnPointerDragged(MixedRealityPointerEventData eventData) { }
}
