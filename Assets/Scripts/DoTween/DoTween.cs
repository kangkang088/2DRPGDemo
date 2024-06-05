using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoTween : MonoBehaviour
{
    public Transform tweenTarget;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(tweenTarget.position,2);
        transform.DORotate(new Vector3(0,90,0),1);
        transform.DOShakePosition(1,1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
