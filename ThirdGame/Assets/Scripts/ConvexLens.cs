using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConvexLens : MonoBehaviour,IDragHandler
{

    private Vector3 initPoint;
    private Vector3 maxPoint;//相对屏幕
    private Vector3 minPoint;//相对屏幕
    private LineRenderer line;  
    private Transform fireTarget;
    private ProgressController progressController;
    private bool isShotting = false;
    private int curFaculaId;

    private void OnDisable()
    {
        this.transform.localPosition = initPoint;
        line.positionCount = 0;
    }

    public void SetDragScope(Vector3 _maxPoint,Vector3 _minPoint)
    {
        minPoint = _minPoint;
        maxPoint = _maxPoint;
        // maxPoint = new Vector3(500,400,0);
        // minPoint = new Vector3(-500,-400,0);
        Debug.Log("minPoint: " + minPoint + "maxPoint: " + maxPoint );
    }
    
    public void SetLine(LineRenderer _line)
    {
        line = _line;
        line.positionCount = 0;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
    }
    public void SetFireTarget(Transform _fireTarget)
    {
        fireTarget = _fireTarget;
    }

    public void SetProgress(ProgressController _progressController)
    {
        progressController = _progressController;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 curPoint = eventData.position;
        // Debug.Log("mousepos: " + curPoint);
        curPoint.x = Mathf.Clamp(curPoint.x, minPoint.x, maxPoint.x);
        curPoint.y = Mathf.Clamp(curPoint.y, minPoint.y, maxPoint.y);
        // Debug.Log("minPoint: " + minPoint + "maxPoint: " + maxPoint );
        // Debug.Log("afterclamp: " + curPoint);
        curPoint.x = (curPoint.x - Screen.width *0.5f)/(Screen.width*0.5f - minPoint.x) * 500;
        curPoint.y = (curPoint.y - Screen.height * 0.5f) / (Screen.height * 0.5f - minPoint.y) * 400;
        transform.localPosition = curPoint;
        // Debug.Log("last: " + curPoint);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Facula"))
        {
            if (isShotting == false)
            {
                isShotting = true;
                curFaculaId = other.gameObject.GetInstanceID();
                line.positionCount = 2;
                line.SetPosition(0, gameObject.transform.position);
                progressController.StartShot();
            }
            else
            {
                if (other.gameObject.GetInstanceID() == curFaculaId)
                {
                    //照射成功
                    line.SetPosition(0, gameObject.transform.position);
                    line.SetPosition(1, fireTarget.position);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Facula") && curFaculaId == other.gameObject.GetInstanceID())
        {
            curFaculaId = 0;
            isShotting = false;
            line.positionCount = 0;
            progressController.EndShot();
        }
    }

    public void SetInitPos(Vector3 initPointPosition)
    {
        initPoint = initPointPosition;
        this.transform.localPosition = initPoint;
    }
}
