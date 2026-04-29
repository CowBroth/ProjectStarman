using UnityEngine;

[System.Serializable]
public class BaseText
{
    public string name;
    [TextArea (3, 10)]
    public string[] sentences;
}
