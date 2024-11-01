using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCurveTube : Tube
{
    public bool endpoint1 = false;
    public bool endpoint2 = false;

    public GameObject instance;

    public Vector3 Start;
    public Vector3 End;

    public TubeRing startRing;
    public TubeRing endRing;

    public SmallCurveTube(GameObject model, Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, bool endpoint1, bool endpoint2)
        : base(model, mesh, position, rotation, scale)
    {
        this.endpoint1 = endpoint1;
        this.endpoint2 = endpoint2;
    }

    public override void Instantiate()
    {
        position.x = Mathf.Round(position.x * 10f) / 10f;
        GameObject modelInstance = Object.Instantiate(model, position, rotation);
        modelInstance.transform.localScale = scale;
        modelInstance.transform.position = new Vector3(modelInstance.transform.position.x, modelInstance.transform.position.y, modelInstance.transform.position.z - 0.7f);
        modelInstance.transform.rotation = Quaternion.Euler(270, 0, 0);

        this.instance = modelInstance;

        MeshCollider meshCollider = modelInstance.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        Renderer renderer = modelInstance.GetComponent<Renderer>();

        Bounds bounds = renderer.bounds;

        Vector3 objectSize = bounds.size;

        Vector3 endPosition = bounds.max;
        Vector3 startPosition = bounds.min;

        endPosition.z = Mathf.Round(endPosition.z * 10f) / 10f;

        this.Start = position;
        this.End = position;

        this.Start.z = startPosition.z;
        this.End.z = endPosition.z;

        GameObject ringModel = Resources.Load<GameObject>("Tubes/ring");
        Mesh ringMesh = Resources.Load<Mesh>("Tubes/ring mesh");
        int tubeID = this.getID();
        Vector3 ringScale = new Vector3(70, 35, 70);

        Vector3 offset = new Vector3(0, 0, 0.7f);

        Renderer ringRenderer = ringModel.GetComponent<Renderer>();

        Bounds ringBounds = ringRenderer.bounds;

        Vector3 ringSize = ringBounds.size;

        if (!endpoint1)
        {
            Vector3 ringPosition1 = position;
            ringPosition1.z = startPosition.z;
            TubeRing ring1 = new TubeRing(ringModel, ringMesh, ringPosition1, Quaternion.identity, ringScale, 0, tubeID);
            ring1.Instantiate();
            ring1.setRightID(this.getID());
            startRing = ring1;
            endpoint1 = true;
        }

        if (!endpoint2)
        {
            Vector3 ringPosition2 = position;
            ringPosition2.z = endPosition.z;
            ringPosition2 = ringPosition2 + offset;
            TubeRing ring2 = new TubeRing(ringModel, ringMesh, ringPosition2, Quaternion.identity, ringScale, tubeID, 0);
            ring2.Instantiate();
            ring2.setLeftID(this.getID());
            Vector3 center = bounds.center;
            ring2.RotateY(center, 270);
            endRing = ring2;
            endpoint2 = true;
        }
    }

    public Vector3 getStartPosition()
    {
        Vector3 startPosition = Start;
        startPosition.z -= 0.7f;
        return startPosition;
    }

    public Vector3 getEndPosition()
    {
        Vector3 endPosition = endRing.instance.transform.position;
        return endPosition;
    }

    public void Rotate()
    {
        instance.transform.rotation = Quaternion.Euler(0, 0, instance.transform.rotation.z + 90);
        endRing.RotateY(Start, 270);
        End = endRing.instance.transform.position;
    }
}
