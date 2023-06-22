using UnityEngine;
using UnityEngine.UI;

public class CustomButtonShapeFromImage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
