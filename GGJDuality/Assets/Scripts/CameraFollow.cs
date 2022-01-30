using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Transform playerTransform;

    private Camera main;
    private Vector3 smoothPosition;
    [SerializeField] private float smoothSpeed;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    private float camX;
    private float camY;
    private float camOrthographicSize;
    private float camAspectRatio;

    // Start is called before the first frame update
    void Start()
    {
        main = Camera.main;
        minX = tilemap.cellBounds.min.x;
        maxX = tilemap.cellBounds.max.x - 1;
        minY = tilemap.cellBounds.min.y;
        maxY = tilemap.cellBounds.max.y;
        camOrthographicSize = main.orthographicSize;
        camAspectRatio = main.aspect * camOrthographicSize;
    }

    // FixedUpdate is called once per fixed frame
    void FixedUpdate()
    {
        camX = Mathf.Clamp(playerTransform.position.x, minX + camAspectRatio, maxX - camAspectRatio);
        camY = Mathf.Clamp(playerTransform.position.y, minY + camOrthographicSize, maxY - camOrthographicSize);
        smoothPosition = Vector3.Lerp(transform.position, new Vector3(camX, camY, transform.position.z), smoothSpeed);
        transform.position = smoothPosition;
    }
}
