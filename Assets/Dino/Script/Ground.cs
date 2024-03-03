using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        if (meshRenderer != null)
        {
            float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
            meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
        }
    }

}
