using NaughtyAttributes;
using UnityEngine;

public class TestAttributes : MonoBehaviour
{
    [SerializeField] private bool useAttributes;
    [ShowIf("useAttributes")]
    [SerializeField] private float hiddenField;
    [ShowIf("useAttributes")]
    [SerializeField] private float hiddenField2;
    [ShowIf("useAttributes")]
    [SerializeField] private float hiddenField3;

    [Button]
    public void TestMethod()
    {
        Debug.Log("Hello from the TestMethod");
    }
}
