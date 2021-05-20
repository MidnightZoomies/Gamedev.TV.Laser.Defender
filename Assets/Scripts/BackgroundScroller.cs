using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeedY = 0.03f;
    //[SerializeField] float backgroundScrollSpeedX = 0.01f; Need a vertical and horizontal looping background
    Material myMaterial;
    Vector2 offset;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0f, backgroundScrollSpeedY);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
