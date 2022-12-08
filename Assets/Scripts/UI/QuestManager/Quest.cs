using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Quest
{
    public int id;
    public string title;
    public int stages;
    public string description;
    public List<GameObject> barriers;
}
