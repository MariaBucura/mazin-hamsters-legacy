using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossIntersectTube : Tube
{
    public bool endpoint1 = false;
    public bool endpoint2 = false;
    public bool endpoint3 = false;
    public bool endpoint4 = false;

    public Vector3 Start;
    public Vector3 End;

    public TubeRing startRing;
    public TubeRing topRing;
    public TubeRing endRing;
    public TubeRing bottomRing;

    public Transform snapPointA1;
    public Transform snapPointA2;

    public Transform snapPointB1;
    public Transform snapPointB2;

    public Transform snapPointC1;
    public Transform snapPointC2;

    public Transform snapPointD1;
    public Transform snapPointD2;

    public CrossIntersectTube(GameObject model, Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale, bool endpoint1, bool endpoint2, bool endpoint3, bool endpoint4)
        : base(model, mesh, position, rotation, scale)
    {
        this.endpoint1 = endpoint1;
        this.endpoint2 = endpoint2;
        this.endpoint3 = endpoint3;
        this.endpoint4 = endpoint4;
    }

    public override void Instantiate()
    {
        GameObject modelInstance = Object.Instantiate(model, position, rotation);
        modelInstance.transform.localScale = scale;

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
        ringPosition1.x -= bounds.size.x / 2 + 0.7f;

        if (!endpoint1)
        {
            TubeRing ring1 = new TubeRing(ringModel, ringMesh, ringPosition1, Quaternion.identity, ringScale, 0, tubeID);
            ring1.Instantiate();
            ring1.setRightID(this.getID());
            ring1.instance.transform.rotation = Quaternion.Euler(0, 0, 90);
            startRing = ring1;
            generateSnapPoints2(startRing.instance, out snapPointA1, out snapPointA2, ringPosition1);
            startRing.instance.transform.parent = instance.transform;
            endpoint1 = true;
        }
        else
        {
            ringPosition1.x += 0.7f;
            generateSnapPoints3(instance, out snapPointA1, out snapPointA2, ringPosition1);
        }

        Vector3 ringPosition2 = position;
        ringPosition2.z = endPosition.z;
        ringPosition2.z -= bounds.size.z;

        if (!endpoint2)
        {
            TubeRing ring2 = new TubeRing(ringModel, ringMesh, ringPosition2, Quaternion.identity, ringScale, tubeID, 0);
            ring2.Instantiate();
            ring2.setLeftID(this.getID());
            topRing = ring2;
            Vector3 snapPosition2 = ringPosition2;
            snapPosition2.z -= 0.7f;
            generateSnapPoints4(topRing.instance, out snapPointB1, out snapPointB2, snapPosition2);
            topRing.instance.transform.parent = instance.transform;
            endpoint2 = true;
        }
        else
        {
            generateSnapPoints4(instance, out snapPointB1, out snapPointB2, ringPosition2);
        }

        Vector3 ringPosition3 = position;
        ringPosition3.x += bounds.size.x / 2;

        if (!endpoint3)
        {
            TubeRing ring3 = new TubeRing(ringModel, ringMesh, ringPosition3, Quaternion.identity, ringScale, tubeID, 0);
            ring3.Instantiate();
            ring3.setLeftID(this.getID());
            ring3.instance.transform.rotation = Quaternion.Euler(0, 0, 90);
            endRing = ring3;
            Vector3 snapPosition3 = ringPosition3;
            snapPosition3.x += 0.7f;
            generateSnapPoints3(endRing.instance, out snapPointC1, out snapPointC2, snapPosition3);
            endRing.instance.transform.parent = instance.transform;
            endpoint3 = true;
        }
        else
        {
            generateSnapPoints2(instance, out snapPointC1, out snapPointC2, ringPosition3);
        }

        Vector3 ringPosition4 = position;
        ringPosition4.z = endPosition.z + 0.7f;

        if (!endpoint4)
        {
            TubeRing ring4 = new TubeRing(ringModel, ringMesh, ringPosition4, Quaternion.identity, ringScale, tubeID, 0);
            ring4.Instantiate();
            ring4.setLeftID(this.getID());
            bottomRing = ring4;
            Vector3 snapPosition4 = ringPosition4;
            snapPosition4.z += 0.7f;
            generateSnapPoints1(bottomRing.instance, out snapPointD1, out snapPointD2, ringPosition4);
            bottomRing.instance.transform.parent = instance.transform;
            endpoint4 = true;
        }
        else
        {
            ringPosition4.z -= 0.7f;
            generateSnapPoints1(instance, out snapPointD1, out snapPointD2, ringPosition4);
        }
    }

    private void generateSnapPoints1(GameObject ring, out Transform snapPoint1, out Transform snapPoint2, Vector3 position)
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

    private void generateSnapPoints2(GameObject ring, out Transform snapPoint1, out Transform snapPoint2, Vector3 position)
    {
        GameObject snap1 = new GameObject("Snap point 1");
        GameObject snap2 = new GameObject("Snap point 2");

        snap1.transform.position = new Vector3(position.x, position.y, position.z + (0.7f * 3f));
        snap2.transform.position = new Vector3(position.x, position.y, position.z - (0.7f * 3f));

        snap1.transform.parent = ring.transform;
        snap2.transform.parent = ring.transform;

        snapPoint1 = snap1.transform;
        snapPoint2 = snap2.transform;
    }

    private void generateSnapPoints3(GameObject ring, out Transform snapPoint1, out Transform snapPoint2, Vector3 position)
    {
        GameObject snap1 = new GameObject("Snap point 1");
        GameObject snap2 = new GameObject("Snap point 2");

        snap1.transform.position = new Vector3(position.x, position.y, position.z - (0.7f * 3f));
        snap2.transform.position = new Vector3(position.x, position.y, position.z + (0.7f * 3f));

        snap1.transform.parent = ring.transform;
        snap2.transform.parent = ring.transform;

        snapPoint1 = snap1.transform;
        snapPoint2 = snap2.transform;
    }

    private void generateSnapPoints4(GameObject ring, out Transform snapPoint1, out Transform snapPoint2, Vector3 position)
    {
        GameObject snap1 = new GameObject("Snap point 1");
        GameObject snap2 = new GameObject("Snap point 2");

        snap1.transform.position = new Vector3(position.x - (0.7f * 3f), position.y, position.z);
        snap2.transform.position = new Vector3(position.x + (0.7f * 3f), position.y, position.z);

        snap1.transform.parent = ring.transform;
        snap2.transform.parent = ring.transform;

        snapPoint1 = snap1.transform;
        snapPoint2 = snap2.transform;
    }

    public Vector3 getStartPosition()
    {
        Vector3 startPosition = Start;
        startPosition.z -= 0.7f;
        return startPosition;
    }

    public Vector3 getEndPosition()
    {
        Vector3 endPosition = End;
        endPosition.z += 0.7f;
        return endPosition;
    }

    public void Rotate()
    {
        instance.transform.rotation = Quaternion.Euler(0, 0, instance.transform.rotation.z + 90);
        endRing.RotateY(Start, 270);
        End = endRing.instance.transform.position;
    }
}
