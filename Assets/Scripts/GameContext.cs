using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameContext : MonoBehaviour
{
    public GameObject PaintPartsContainer;
    public GamePanel GamePanel;
    public PlayerController PlayerController;
    public BrushDetector BrushDetector;
    public Dictionary<Color, BrushData> Brushes = new Dictionary<Color, BrushData>();
    private List<PaintPart> PaintParts = new List<PaintPart>();
    private void Awake()
    {
        PaintPart.SendBrushData += AddBrush;
        PaintPart.PartColoredCorrect += RemoveBrush;
    }
    void Start()
    {
        for (int i = 0; i < PaintPartsContainer.transform.childCount; i++)
        {
            PaintParts.Add(PaintPartsContainer.transform.GetChild(i).AddComponent<PaintPart>());
        }
        StartCoroutine(ColorFadeRoutine());
    }

    private void AddBrush(Color color)
    {
        if (Brushes.ContainsKey(color))
        {
            Brushes[color].Count++;
            GamePanel.ProgressBar.maxValue++;
        }
        else
        {
            Brush brush = Instantiate(Resources.Load<Brush>("Prefabs/Brush"), GamePanel.BrushesContainer);
            Brushes.Add(color, new BrushData(brush.gameObject));           
            brush.PaintImage.color = color;
            GamePanel.ProgressBar.maxValue++;
        }
    }

    private void RemoveBrush(Color color)
    {
        Brushes[color].Count--;
        GamePanel.ProgressBar.value++;
        if (Brushes[color].Count <= 0)
        {
            Destroy(Brushes[color].GameObject);
            Brushes.Remove(color);
        }
    }

    public void HighlightParts(Button button)
    {
        StartCoroutine(HighLightPartsRoutine(button));
    }

    IEnumerator HighLightPartsRoutine(Button button)
    {
        button.interactable = false;
        PlayerController.enabled = false;
        foreach (PaintPart part in PaintParts)
        {
            part.Material.color = part.correctColor;
        }
        yield return new WaitForSeconds(3);
        foreach (PaintPart part in PaintParts)
        {
            if(!part.IsCorrectColor) part.Material.color = Color.white;
        }
        button.interactable = true;
        PlayerController.enabled = true;
    }

    IEnumerator ColorFadeRoutine()
    {
        yield return new WaitForSeconds(3);
        foreach (PaintPart part in PaintParts) 
        {
            part.Material.color = Color.white;
            part.IsCorrectColor = false;
        }
        GamePanel.ProgressBar.maxValue--;
        BrushDetector.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        PaintPart.SendBrushData -= AddBrush;
        PaintPart.PartColoredCorrect -= RemoveBrush;
    }
}
