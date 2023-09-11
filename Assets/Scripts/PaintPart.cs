using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PaintPart : MonoBehaviour
{
    public Color correctColor { get; private set; }
    public Material Material;
    public bool IsCorrectColor = true;

    public static Action<Color> SendBrushData;
    public static Action<Color> PartColoredCorrect;
    void Start()
    {
        Material = GetComponent<Renderer>().material;
        correctColor = Material.color;
        tag = "PaintPart";
        SendBrushData?.Invoke(correctColor);
    }

    public void ChangeColor(Color color)
    {
        if (IsCorrectColor) return;
        Material.color = color;

        if(color == correctColor)
        {
            IsCorrectColor = true;
            PartColoredCorrect?.Invoke(color);
            GlobalContext.Instance.SoundManager.Play("CorrectPaintSound");
        }
        else
        {
            GlobalContext.Instance.SoundManager.Play("WrongPaintSound");
        }
    }
}
