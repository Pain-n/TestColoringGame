using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    public GameContext GameContext;

    public Transform BrushesContainer;

    public Slider ProgressBar;
    public Button HighlightButton;

    private void Start()
    {
        HighlightButton.onClick.AddListener(() =>
        {
            GameContext.HighlightParts(HighlightButton);
        });
    }
}
