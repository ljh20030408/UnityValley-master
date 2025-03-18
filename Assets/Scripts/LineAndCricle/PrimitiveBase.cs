using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
[ExecuteInEditMode]
public class PrimitiveBase : MonoBehaviour {

    public Vector3 offset;
    protected MeshFilter meshFilter;
    public Transform cacheTransform;

    protected Vector3[] vertices;
    protected int[] triangles;
    protected Vector2[] uvs;

    void Awake()
    {
        Init();
        InitMesh();
    }

    public void Init()
    {
        cacheTransform = this.transform;
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = new Mesh();
    }

    protected virtual void InitMesh() { }
    protected virtual void UpdateShape() { }
    protected void UpdateMesh()
    {
        if (meshFilter.sharedMesh == null)
        {
            meshFilter.sharedMesh = new Mesh();
        }
        meshFilter.sharedMesh.vertices = vertices;
        meshFilter.sharedMesh.triangles = triangles;
        meshFilter.sharedMesh.uv = uvs;
    }

    void OnValidate()
    {
        InitMesh();
    }
}
