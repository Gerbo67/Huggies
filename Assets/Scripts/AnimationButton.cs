using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector3 normalScale = new Vector3(1, 1, 1);
    public Vector3 pressedScale = new Vector3(0.95f, 0.95f, 1);
    public float scaleDownSpeed = 30f;
    public float scaleUpSpeed = 40f;

    private bool isPressed = false;

    void Update()
    {
        float targetScaleSpeed = isPressed ? scaleDownSpeed : scaleUpSpeed;
        transform.localScale = Vector3.Lerp(transform.localScale, isPressed ? pressedScale : normalScale, targetScaleSpeed * Time.deltaTime);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
