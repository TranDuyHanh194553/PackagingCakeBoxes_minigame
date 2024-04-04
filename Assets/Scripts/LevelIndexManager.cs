using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelIndexManager : MonoBehaviour
{
    public int levelIndex;
    [SerializeField] TextMeshProUGUI level;

    private void Start()
    {
        level.text = "LEVEL " + levelIndex;
    }
}
