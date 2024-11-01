using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeRing : Tube
{
    private int Left = 0;
    private int Right = 0;

  public TubeRing(GameObject model, Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, int Left, int Right)
        : base(model, mesh, position, rotation, scale)
    {
        this.Left = Left;
        this.Right = Right;
    }

    public override void Instantiate()
    {
        GameObject modelInstance = Object.Instantiate(model, position, rotation);
        modelInstance.transform.localScale = scale;

        modelInstance.transform.rotation = Quaternion.Euler(90, 0, 0);

        MeshCollider meshCollider = modelInstance.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        instance = modelInstance;
    }

    public void setLeftID(int ID)
    {
        this.Left = ID;
    }

    public void setRightID(int ID)
    {
        this.Right = ID;
    }

    public int getLeftID()
    {
        return Left;
    }

    public int getRightID()
    {
        return Right;
    }

    public void Rotate(Vector3 rotationPoint)
    {
        instance.transform.RotateAround(rotationPoint, Vector3.forward, 90);
    }

    public void RotateY(Vector3 rotationPoint, float angle)
    {
        instance.transform.RotateAround(rotationPoint, Vector3.up, angle);
    }
}
