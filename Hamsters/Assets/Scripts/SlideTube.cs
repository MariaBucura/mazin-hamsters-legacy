using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideTube : Tube
{
    public bool endpoint1 = false;
    public bool endpoint2 = false;

    public Vector3 Start;
    public Vector3 End;

    public TubeRing startRing;
    public TubeRing endRing;

    public Transform snapPointA1;
    public Transform snapPointA2;

    public Transform snapPointB1;
    public Transform snapPointB2;

    public SlideTube(GameObject model, Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, bool endpoint1, bool endpoint2)
        : base(model, mesh, position, rotation, scale)
    {
        this.endpoint1 = endpoint1;
        this.endpoint2 = endpoint2;
        category = "slideTube";
    }

    public override void Instantiate()
    {
        GameObject modelInstance = Object.Instantiate(model, position, rotation);
        modelInstance.transform.localScale = scale;

        modelInstance.transform.rotation = Quaternion.Euler(90, 0, 0);
        this.instance = modelInstance;

        MeshCollider meshCollider = modelInstance.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        Renderer renderer = modelInstance.GetComponent<Renderer>();

        Bounds bounds = renderer.bounds;

        Vector3 objectSize = bounds.size;

        Vector3 endPosition = bounds.max;
        Vector3 startPosition = bounds.min;

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

        Vector3 ringPosition1 = position;
        ringPosition1.z = startPosition.z;
        ringPosition1.y -= bounds.size.y - 0.7f * 6f;

        if (!endpoint1)
        {
            TubeRing ring1 = new TubeRing(ringModel, ringMesh, ringPosition1, Quaternion.identity, ringScale, 0, tubeID);
            ring1.Instantiate();
            ring1.setRightID(this.getID());
            startRing = ring1;
            startRing.instance.transform.parent = instance.transform;
            ringPosition1.z -= 0.7f;
            generateSnapPoints(startRing.instance, out snapPointA1, out snapPointA2, ringPosition1);
            endpoint1 = true;
        }
        else
        {
            Start.y -= bounds.size.y - 0.7f * 6f;
            generateSnapPoints(instance, out snapPointA1, out snapPointA2, Start);
        }

        Vector3 ringPosition2 = position;
        ringPosition2.z = endPosition.z;
        ringPosition2 = ringPosition2 + offset;

        if (!endpoint2)
        {
            TubeRing ring2 = new TubeRing(ringModel, ringMesh, ringPosition2, Quaternion.identity, ringScale, tubeID, 0);
            ring2.Instantiate();
            ring2.setLeftID(this.getID());
            endRing = ring2;
            generateSnapPoints(endRing.instance, out snapPointB1, out snapPointB2, getEndPosition());
            endRing.instance.transform.parent = instance.transform;
            endpoint2 = true;
        }
        else
        {
            ringPosition2.z -= 0.7f; 
            generateSnapPoints(instance, out snapPointA1, out snapPointA2, ringPosition2);
        }
    }

    private void generateSnapPoints(GameObject ring, out Transform snapPoint1, out Transform snapPoint2, Vector3 position)
    {
        GameObject snap1 = new GameObject("Snap point 1");
        GameObject snap2 = new GameObject("Snap point 2");

        snap1.transform.position = new Vector3(position.x + (0.7f * 3f), position.y, position.z);
        snap2.transform.position = new Vector3(position.x - (0.7f * 3f), position.y, position.z);

        snap1.transform.parent = ring.transform;
        snap2.transform.parent = ring.transform;

        snapPoint1 = snap1.transform;
        snapPoint2 = snap2.transform;
    }

    public Vector3 getStartPosition()
    {
        Renderer renderer = instance.GetComponent<Renderer>();

        Bounds bounds = renderer.bounds;

        Vector3 startPosition = bounds.min;
        return startPosition;
    }

    public Vector3 getEndPosition()
    {
        Vector3 endPosition = End;
        endPosition.z += 0.7f;
        return endPosition;
    }

    public void Rotate(Vector3 position)
    {
        instance.transform.RotateAround(position, instance.transform.up, 90f);
        endRing.instance.transform.Rotate(endRing.instance.transform.up, -90f, Space.World);
    }
}
