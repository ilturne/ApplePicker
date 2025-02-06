using UnityEngine;
using UnityEngine.UI;

public class RollingClouds : MonoBehaviour
{
    [SerializeField] private RawImage _img; // Background RawImage
    [SerializeField] private float _xSpeed = 0.1f; // Speed along X
    [SerializeField] private float _ySpeed = 0f;   // Speed along Y

    // Update is called once per frame
    void Update()
    {
        // Move the UV coordinates of the RawImage texture
        _img.uvRect = new Rect(
            _img.uvRect.x + _xSpeed * Time.deltaTime,
            _img.uvRect.y + _ySpeed * Time.deltaTime,
            _img.uvRect.width,
            _img.uvRect.height
        );
    }
}
