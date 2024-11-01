using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tube
{
    private static int nextID = 0;
    private int id;

    public GameObject model;
    public Mesh mesh;
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;

    public GameObject instance;

    public string category;

    public Tube(GameObject model, Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale)
    {
        nextID++;
        this.model = model;
        this.mesh = mesh;
        this.position = position;
        this.rotation = rotation;
        this.scale = scale;
        this.id = nextID;
    }

    public virtual void Instantiate()
    {
        if(model != null)
        {
            GameObject modelInstance = Object.Instantiate(model, position, rotation);
            modelInstance.transform.localScale = scale;

            MeshCollider meshCollider = modelInstance.AddComponent<MeshCollider>();
            meshCollider.sharedMesh = mesh;
        }
    }

    public int getID()
    {
        return id;
    }
}
