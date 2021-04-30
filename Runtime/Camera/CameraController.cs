using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurtleGames.Framework.Runtime.Camera
{


public class CameraController : MonoBehaviour
{

    private Image foregroundImage;
    private int ongoingFade;

    #region "Unity Functions"

    void Awake()
    {
        GameObject cameraControllerObject = new GameObject("CameraControllerObject");

        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvasObj.transform.SetParent(cameraControllerObject.transform);


        GameObject imageObj = new GameObject("ForegroundImage");
        foregroundImage = imageObj.AddComponent<Image>();
        imageObj.transform.SetParent(canvasObj.transform);

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 500;

        foregroundImage.enabled = false;
        foregroundImage.color = new Color(0, 0, 0, 0);
        foregroundImage.rectTransform.anchorMin = new Vector2(0, 0);
        foregroundImage.rectTransform.anchorMax = new Vector2(1, 1);
    }

    #endregion

    #region "Public Functions"

    public void FadeInFromColor(float duration)
    {
        FadeInFromColor(duration, Color.black);
    }

    public void FadeInFromColor(float duration, Color color)
    {
        Fade(duration, color, 1, 0);
    }

    public void FadeOutToColor(float duration)
    {
        FadeOutToColor(duration, Color.black);
    }

    public void FadeOutToColor(float duration, Color color)
    {
        Fade(duration, color, 0, 1);
    }

    #endregion

    #region "Private Functions"

    private void Fade(float duration, Color color, float from, float to)
    {
        foregroundImage.enabled = true;
        foregroundImage.color = new Color(color.r, color.g, color.b, from);

        LeanTween.cancel(ongoingFade);

        var tween = LeanTween.value(foregroundImage.gameObject, from, to, duration).setOnUpdate((float val) =>
        {
            color.a = val;
            foregroundImage.color = color;
        });
        ongoingFade = tween.uniqueId;
    }

    #endregion

}

}