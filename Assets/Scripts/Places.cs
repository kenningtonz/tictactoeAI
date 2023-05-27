//Kennedy Adams 100632983 A2
using UnityEngine;

public class Places : MonoBehaviour
{
    public bool isClicked = false;

    private void OnMouseDown()
    {
        isClicked = true;
        // Debug.Log("clicked");
    }
}
