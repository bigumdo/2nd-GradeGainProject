using UnityEngine;
using ButtonAttribute;

public class Test : MonoBehaviour
{
    [InspectorButton(true, "PrintA", "Print A")]
    private void PrintA()
    {
        Debug.Log("A");
    }
}