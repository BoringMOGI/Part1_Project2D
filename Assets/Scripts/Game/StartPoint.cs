using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : Singleton<StartPoint>
{
    [SerializeField] Transform startPivot;      // 시작 지점.
    
    public void SetStartPoint(Transform target)
    {
        target.position = startPivot.position;  // 위치 변경.
    }
}
