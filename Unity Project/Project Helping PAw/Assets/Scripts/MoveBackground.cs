using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float movingSpeed;
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float x = Time.time * movingSpeed;
        float y = Time.time * movingSpeed;
        Vector2 offset = new Vector2(x,y);
        meshRenderer.material.mainTextureOffset = offset;
    }
}
