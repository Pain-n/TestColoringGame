using UnityEngine;

public class BrushData
{
    public int Count;
    public GameObject GameObject;

    public BrushData(GameObject gameObject)
    {
        GameObject = gameObject;
        Count = 1;
    }
}
