using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeLoader : MonoBehaviour
{

    public Tube[] tubes;
    // Start is called before the first frame update
    void Start()
    {
        GameObject model = Resources.Load<GameObject>("Tubes/Large straight tube 2");
        Mesh mesh = Resources.Load<Mesh>("Tubes/Large straight tube.002");
        GameObject largeCurveTube = Resources.Load<GameObject>("Tubes/Large curve tube");
        Mesh largeCurveMesh = Resources.Load<Mesh>("Tubes/Large curve tube.001");
        GameObject smallCurveTube = Resources.Load<GameObject>("Tubes/Small curve tube");
        Mesh smallCurveMesh = Resources.Load<Mesh>("Tubes/Small curve tube.001");
        GameObject mediumTube = Resources.Load<GameObject>("Tubes/Medium straight tube");
        Mesh mediumMesh = Resources.Load<Mesh>("Tubes/Medium straight tube.001");
        GameObject smallTube = Resources.Load<GameObject>("Tubes/Small straight tube");
        Mesh smallMesh = Resources.Load<Mesh>("Tubes/Small straight tube");
        GameObject mediumCurveTube = Resources.Load<GameObject>("Tubes/Medium curve tube");
        Mesh mediumCurveMesh = Resources.Load<Mesh>("Tubes/Medium curve tube.002");
        GameObject spiralTube = Resources.Load<GameObject>("Tubes/Large spiral tube");
        Mesh spiralMesh = Resources.Load<Mesh>("Tubes/Large spiral tube.001");
        GameObject largeSlideTube = Resources.Load<GameObject>("Tubes/Large slide tube");
        Mesh largeSlideMesh = Resources.Load<Mesh>("Tubes/Large slide tube.001");
        GameObject mediumSlideTube = Resources.Load<GameObject>("Tubes/Medium slide tube");
        Mesh mediumSlideMesh = Resources.Load<Mesh>("Tubes/Medium slide tube.001");
        GameObject smallSlideTube = Resources.Load<GameObject>("Tubes/Small slide tube");
        Mesh smallSlideMesh = Resources.Load<Mesh>("Tubes/Small slide tube.001");
        GameObject TJunction = Resources.Load<GameObject>("Tubes/T junction");
        Mesh TMesh = Resources.Load<Mesh>("Tubes/T-intersect tube.002");
        GameObject CrossJunction = Resources.Load<GameObject>("Tubes/Cross junction");
        Mesh CrossMesh = Resources.Load<Mesh>("Tubes/Cross-intersect tube.002");

        Vector3 position = new Vector3(3, 15, 0);
        Quaternion rotation = Quaternion.identity;
        Vector3 scale = new Vector3(70, 70, 70);

        LongStraightTube tube = new LongStraightTube(model, mesh, position, rotation, scale, false, false);
        tube.Instantiate();

        LongStraightTube tube2 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube2.Instantiate();

        snapTogether(tube2, tube.endRing, tube2.snapPointA1, tube2.snapPointA2, tube.snapPointB1, tube.snapPointB2);

        CurveTube tube3 = new CurveTube(largeCurveTube, largeCurveMesh, position, rotation, scale, true, false);
        tube3.Instantiate();

        snapTogether(tube3, tube2.endRing, tube3.snapPointA1, tube3.snapPointA2, tube2.snapPointB1, tube2.snapPointB2);

        LongStraightTube tube4 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube4.Instantiate();

        snapTogether(tube4, tube3.endRing, tube4.snapPointA1, tube4.snapPointA2, tube3.snapPointB1, tube3.snapPointB2);

        LongStraightTube tube5 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube5.Instantiate();

        snapTogether(tube5, tube4.endRing, tube5.snapPointA1, tube5.snapPointA2, tube4.snapPointB1, tube4.snapPointB2);

        CurveTube tube6 = new CurveTube(smallCurveTube, smallCurveMesh, position, rotation, scale, true, false);
        tube6.Instantiate();

        snapTogether(tube6, tube5.endRing, tube6.snapPointA1, tube6.snapPointA2, tube5.snapPointB1, tube5.snapPointB2);
        tube6.Rotate();
        tube6.Rotate();

        LongStraightTube tube7 = new LongStraightTube(mediumTube, mediumMesh, position, rotation, scale, true, false);
        tube7.Instantiate();

        snapTogether(tube7, tube6.endRing, tube7.snapPointA1, tube7.snapPointA2, tube6.snapPointB1, tube6.snapPointB2);

        LongStraightTube tube8 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube8.Instantiate();

        snapTogether(tube8, tube7.endRing, tube8.snapPointA1, tube8.snapPointA2, tube7.snapPointB1, tube7.snapPointB2);

        CurveTube tube9 = new CurveTube(mediumCurveTube, mediumCurveMesh, position, rotation, scale, true, false);
        tube9.Instantiate();

        snapTogether(tube9, tube8.endRing, tube9.snapPointA1, tube9.snapPointA2, tube8.snapPointB1, tube8.snapPointB2);

        LargeSpiralTube tube10 = new LargeSpiralTube(spiralTube, spiralMesh, position, rotation, scale, true, false);
        tube10.Instantiate();

        snapTogether(tube10, tube9.endRing, tube10.snapPointA1, tube10.snapPointA2, tube9.snapPointB1, tube9.snapPointB2);

        LongStraightTube tube11 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube11.Instantiate();

        snapTogether(tube11, tube10.endRing, tube11.snapPointA1, tube11.snapPointA2, tube10.snapPointB1, tube10.snapPointB2);

        LongStraightTube tube12 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube12.Instantiate();

        snapTogether(tube12, tube11.endRing, tube12.snapPointA1, tube12.snapPointA2, tube11.snapPointB1, tube11.snapPointB2);

        SlideTube tube13 = new SlideTube(largeSlideTube, largeSlideMesh, position, rotation, scale, true, false);
        tube13.Instantiate();

        snapTogether(tube13, tube12.endRing, tube13.snapPointA1, tube13.snapPointA2, tube12.snapPointB1, tube12.snapPointB2);
        tube13.Rotate(tube12.endRing.instance.transform.position);
        tube13.Rotate(tube12.endRing.instance.transform.position);

        CurveTube tube14 = new CurveTube(largeCurveTube, largeCurveMesh, position, rotation, scale, true, false);
        tube14.Instantiate();

        snapTogether(tube14, tube13.endRing, tube14.snapPointA1, tube14.snapPointA2, tube13.snapPointB1, tube13.snapPointB2);

        LongStraightTube tube15 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube15.Instantiate();

        snapTogether(tube15, tube14.endRing, tube15.snapPointA1, tube15.snapPointA2, tube14.snapPointB1, tube14.snapPointB2);

        LongStraightTube tube16 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube16.Instantiate();

        snapTogether(tube16, tube15.endRing, tube16.snapPointA1, tube16.snapPointA2, tube15.snapPointB1, tube15.snapPointB2);


        CurveTube tube17 = new CurveTube(largeCurveTube, largeCurveMesh, position, rotation, scale, true, false);
        tube17.Instantiate();

        snapTogether(tube17, tube16.endRing, tube17.snapPointA1, tube17.snapPointA2, tube16.snapPointB1, tube16.snapPointB2);


        CurveTube tube18 = new CurveTube(largeCurveTube, largeCurveMesh, position, rotation, scale, true, false);
        tube18.Instantiate();

        snapTogether(tube18, tube17.endRing, tube18.snapPointA1, tube18.snapPointA2, tube17.snapPointB1, tube17.snapPointB2);

        LongStraightTube tube19 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube19.Instantiate();

        snapTogether(tube19, tube18.endRing, tube19.snapPointA1, tube19.snapPointA2, tube18.snapPointB1, tube18.snapPointB2);

        LongStraightTube tube20 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube20.Instantiate();

        snapTogether(tube20, tube19.endRing, tube20.snapPointA1, tube20.snapPointA2, tube19.snapPointB1, tube19.snapPointB2);

        CurveTube tube21 = new CurveTube(mediumCurveTube, mediumCurveMesh, position, rotation, scale, true, false);
        tube21.Instantiate();

        snapTogether(tube21, tube20.endRing, tube21.snapPointA1, tube21.snapPointA2, tube20.snapPointB1, tube20.snapPointB2);
        tube21.Rotate();
        tube21.Rotate();

        LongStraightTube tube22 = new LongStraightTube(smallTube, smallMesh, position, rotation, scale, true, false);
        tube22.Instantiate();

        snapTogether(tube22, tube21.endRing, tube22.snapPointA1, tube22.snapPointA2, tube21.snapPointB1, tube21.snapPointB2);

        CurveTube tube23 = new CurveTube(mediumCurveTube, mediumCurveMesh, position, rotation, scale, true, false);
        tube23.Instantiate();

        snapTogether(tube23, tube22.endRing, tube23.snapPointA1, tube23.snapPointA2, tube22.snapPointB1, tube22.snapPointB2);

        TIntersectTube tube24 = new TIntersectTube(TJunction, TMesh, position, rotation, scale, false, true, false);
        tube24.Instantiate();

        snapTogether(tube24, tube23.endRing, tube24.snapPointB1, tube24.snapPointB2, tube23.snapPointB1, tube23.snapPointB2);

        LongStraightTube tube25 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube25.Instantiate();

        snapTogether(tube25, tube24.endRing, tube25.snapPointA1, tube25.snapPointA2, tube24.snapPointC1, tube24.snapPointC2);

        LongStraightTube tube26 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube26.Instantiate();

        snapTogether(tube26, tube25.endRing, tube26.snapPointA1, tube26.snapPointA2, tube25.snapPointB1, tube25.snapPointB2);

        LongStraightTube tube27 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube27.Instantiate();

        snapTogether(tube27, tube26.endRing, tube27.snapPointA1, tube27.snapPointA2, tube26.snapPointB1, tube26.snapPointB2);

        LongStraightTube tube28 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube28.Instantiate();

        snapTogether(tube28, tube27.endRing, tube28.snapPointA1, tube28.snapPointA2, tube27.snapPointB1, tube27.snapPointB2);

        CrossIntersectTube tube29 = new CrossIntersectTube(CrossJunction, CrossMesh, position, rotation, scale, true, false, false, false);
        tube29.Instantiate();

        snapTogether(tube29, tube28.endRing, tube29.snapPointA1, tube29.snapPointA2, tube28.snapPointB1, tube28.snapPointB2);

        CurveTube tube30 = new CurveTube(mediumCurveTube, mediumCurveMesh, position, rotation, scale, true, false);
        tube30.Instantiate();

        snapTogether(tube30, tube29.endRing, tube30.snapPointA1, tube30.snapPointA2, tube29.snapPointB1, tube29.snapPointB2);

        CurveTube tube31 = new CurveTube(mediumCurveTube, mediumCurveMesh, position, rotation, scale, true, false);
        tube31.Instantiate();

        snapTogether(tube31, tube30.endRing, tube31.snapPointA1, tube31.snapPointA2, tube30.snapPointB1, tube30.snapPointB2);

        CurveTube tube32 = new CurveTube(mediumCurveTube, mediumCurveMesh, position, rotation, scale, true, true);
        tube32.Instantiate();

        snapTogether(tube32, tube31.endRing, tube32.snapPointA1, tube32.snapPointA2, tube31.snapPointB1, tube31.snapPointB2);

        LargeSpiralTube tube33 = new LargeSpiralTube(spiralTube, spiralMesh, position, rotation, scale, true, false);
        tube33.Instantiate();

        snapTogether(tube33, tube29.endRing, tube33.snapPointA1, tube33.snapPointA2, tube29.snapPointD1, tube29.snapPointD2);

        CurveTube tube34 = new CurveTube(largeCurveTube, largeCurveMesh, position, rotation, scale, true, false);
        tube34.Instantiate();

        snapTogether(tube34, tube33.endRing, tube34.snapPointA1, tube34.snapPointA2, tube33.snapPointB1, tube33.snapPointB2);
        tube34.Rotate();

        LongStraightTube tube35 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube35.Instantiate();

        snapTogether(tube35, tube24.endRing, tube35.snapPointA1, tube35.snapPointA2, tube24.snapPointA1, tube24.snapPointA2);

        LongStraightTube tube36 = new LongStraightTube(smallTube, smallMesh, position, rotation, scale, true, false);
        tube36.Instantiate();

        snapTogether(tube36, tube35.endRing, tube36.snapPointA1, tube36.snapPointA2, tube35.snapPointB1, tube35.snapPointB2);

        LongStraightTube tube37 = new LongStraightTube(model, mesh, position, rotation, scale, true, false);
        tube37.Instantiate();

        snapTogether(tube37, tube34.endRing, tube37.snapPointA1, tube37.snapPointA2, tube34.snapPointB1, tube34.snapPointB2);

        LargeSpiralTube tube38 = new LargeSpiralTube(spiralTube, spiralMesh, position, rotation, scale, true, false);
        tube38.Instantiate();

        snapTogether(tube38, tube37.endRing, tube38.snapPointA1, tube38.snapPointA2, tube37.snapPointB1, tube37.snapPointB2);
        

    }

    private void snapTogether(Tube tube1, TubeRing tubeRing2, Transform snapA1, Transform snapA2, Transform snapB1, Transform snapB2)
    {
        float deltaX = Mathf.Abs(snapB1.transform.position.x - snapB2.transform.position.x);
        float deltaY = Mathf.Abs(snapB1.transform.position.y - snapB2.transform.position.y);
        float deltaZ = Mathf.Abs(snapB1.transform.position.z - snapB2.transform.position.z);

        if (deltaY > deltaX && deltaY > deltaZ)
        {
            tubeRing2.instance.transform.Rotate(tubeRing2.instance.transform.up, 90f, Space.World);
        }

        if (Vector3.Dot(tubeRing2.instance.transform.up, Vector3.down) > 0.9f)
        {
            Debug.Log("object is facing down");
            switch (tube1.category)
            {
                case "straightTube":
                    tube1.instance.transform.Rotate(tube1.instance.transform.right, -90f, Space.World);
                    break;
                case "slideTube":
                    tube1.instance.transform.Rotate(tube1.instance.transform.right, 90f, Space.World);
                    break;
                case "curveTube":
                    tube1.instance.transform.Rotate(tube1.instance.transform.up, -90f, Space.World);
                    break;
                case "largeSpiralTube":
                    tube1.instance.transform.Rotate(tube1.instance.transform.right, -90f, Space.World);
                    break;
                default:
                    Debug.Log("Invalid value");
                    break;
            }
        }

        Vector3 direction1 = snapB2.position - snapB1.position;
        Vector3 direction2 = snapA2.position - snapA1.position;

        Quaternion rotationOffset = Quaternion.FromToRotation(direction2, direction1);

        tube1.instance.transform.rotation = rotationOffset * tube1.instance.transform.rotation;

        Vector3 offset = snapA1.position - snapB1.position;

        tube1.instance.transform.position -= offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
