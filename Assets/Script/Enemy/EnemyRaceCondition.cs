using System;
using UnityEngine;

public class EnemyRaceCondition : MonoBehaviour
{
    [SerializeField] public Camera myCamera;
    [SerializeField] public GameObject objectToCheck;

    private GameObject A, B, C, D, E;

    private Vector3 _normalVectorPlaneABC, _normalVectorPlaneACD, _normalVectorPlaneADE, _normalVectorPlaneABE, _normalVectorPlaneBCD;
    private Vector3 _directVectorSidePlane,_directVectorBasePlane;

    public static EnemyRaceCondition Instance;
    public  Action<GameObject> CheckEnemyOnCamStart;
    public  Action<GameObject> CheckEnemyOnCamCallback;

    void Awake()
    {
        SingletonManager.Instance.RegisterObj<EnemyRaceCondition>(this);
        Instance = SingletonManager.Instance.GetObjInstance<EnemyRaceCondition>();
    }
    // Start is called before the first frame update
    void Start()
    {
        SetPyramidPoint();
    }


    public void SetNormalVectorFereachPlane()
    {
        _normalVectorPlaneABC = Vector3.Cross((B.transform.position - A.transform.position),(C.transform.position - A.transform.position));
        _normalVectorPlaneACD = Vector3.Cross((C.transform.position - A.transform.position), (D.transform.position - A.transform.position));
        _normalVectorPlaneADE = Vector3.Cross((D.transform.position - A.transform.position), (E.transform.position - A.transform.position));
        _normalVectorPlaneABE = Vector3.Cross((E.transform.position - A.transform.position), (B.transform.position - A.transform.position));
        _normalVectorPlaneBCD = Vector3.Cross((D.transform.position - B.transform.position), (C.transform.position - B.transform.position));
    }

    public void SetDirectionVector(Vector3 positionObjWanted)
    {
        _directVectorSidePlane = positionObjWanted - A.transform.position;
        _directVectorBasePlane = positionObjWanted - B.transform.position;
    }

    public void SetPyramidPoint()
    {
        float visibleHeight = Mathf.Tan(0.5f * myCamera.fieldOfView * Mathf.Deg2Rad) * myCamera.farClipPlane * 2;
        float visibleWidth = Mathf.Tan(0.5f * myCamera.fieldOfView * Mathf.Deg2Rad) * myCamera.farClipPlane * 2 * myCamera.aspect;

        A = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        A.name = "A";
        A.transform.position = myCamera.transform.position;
        A.transform.parent = myCamera.transform;
        A.transform.localScale = Vector3.one * 2;

        B = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        B.transform.parent = myCamera.transform;
        B.name = "B";
        B.transform.position = myCamera.transform.position + new Vector3(visibleWidth , visibleHeight, myCamera.farClipPlane);
        B.transform.localScale = Vector3.one * 3;

        C = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        C.transform.parent = myCamera.transform;
        C.name = "C";
        C.transform.position = myCamera.transform.position + new Vector3(-visibleWidth , visibleHeight ,  myCamera.farClipPlane);
        C.transform.localScale = Vector3.one * 2;

        D = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        D.transform.parent = myCamera.transform;
        D.name = "D";
        D.transform.position = myCamera.transform.position + new Vector3(-visibleWidth , -visibleHeight ,  myCamera.farClipPlane);
        D.transform.localScale = Vector3.one * 2;

        E = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        E.transform.parent = myCamera.transform;
        E.name = "E";
        E.transform.position = myCamera.transform.position + new Vector3(visibleWidth , -visibleHeight ,  myCamera.farClipPlane);
        E.transform.localScale = Vector3.one * 2;
    }

    private bool IsObjectInsideCamera(Vector3 positionObj)
    {
        SetNormalVectorFereachPlane();
        SetDirectionVector(positionObj);

        //Debug.Log("ABC: " + (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneABC) > 0) + " ACD: " + (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneACD) > 0)
        //    + " ADE: " + (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneADE) > 0) + " ABE: " + (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneABE) > 0)
        //    + " BCD " + (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneBCD) > 0));

        // NEGATIVO IN UN CASO, non mi è chiaro ma funziona, versione corretta:
        return (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneABC) > 0) &&
            (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneACD) > 0) &&
            (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneADE) > 0) &&
            (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneABE) > 0) &&
            (Vector3.Dot(_directVectorBasePlane, _normalVectorPlaneBCD) > 0);

        //return (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneABC) > 0) &&
        //    (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneACD) > 0) &&
        //    (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneADE) > 0) &&
        //    (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneABE) < 0) &&
        //    (Vector3.Dot(_directVectorSidePlane, _normalVectorPlaneBCD) > 0);
    }

    public void SetObjectActive(GameObject obj)
    {
        if(obj != null)
        {
            if (IsObjectInsideCamera(obj.transform.position))
            {
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                //CheckEnemyOnCamCallback.Invoke(obj);
                return;
            }
            else
            {
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                //CheckEnemyOnCamCallback.Invoke(obj);
                return;
            }
        }
        //CheckEnemyOnCamCallback.Invoke(obj);
    }


}
