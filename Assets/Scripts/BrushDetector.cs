using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrushDetector : MonoBehaviour
{
    public static Action<Color> SetBrushColor;

    private GameObject detectedBrush;
    private float basePositionX;
    private void Update()
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = transform.position }, results);

        if (results.Count > 0)
        {
            if (results[0].gameObject.tag == "Brush")
            {
                if(detectedBrush != results[0].gameObject)
                {
                    if(basePositionX == 0) basePositionX = results[0].gameObject.transform.position.x;
                    SetBrushColor?.Invoke(results[0].gameObject.GetComponent<Brush>().PaintImage.color);
                    if(detectedBrush != null) detectedBrush.transform.position = new Vector2(basePositionX, detectedBrush.transform.position.y);
                    detectedBrush = results[0].gameObject;
                    detectedBrush.transform.position = new Vector2(detectedBrush.transform.position.x - 25, detectedBrush.transform.position.y);
                }
            }
        }
    }
}
