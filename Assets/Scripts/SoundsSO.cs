using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundList", menuName = "SoundsSO")]
public class SoundsSO : ScriptableObject
{
    public List<Sound> SoundList;
}
