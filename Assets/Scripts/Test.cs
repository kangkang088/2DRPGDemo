using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void IsFront_1(Entity a,Entity b) {
        if(Vector3.Dot(a.direction,b.direction) < 0) {
            Console.WriteLine("a��b�ĺ�");
        }
        else if(Vector3.Dot(a.direction,a.direction) > 0) {
            Console.WriteLine("a��b��ǰ��");
        }
        else {
            Console.WriteLine("һ������");
        }
    }
    void IsFront_2(Entity a,Entity b) {
        if(Vector3.Dot(a.position.normalized,b.position.normalized) < 0) {
            Console.WriteLine("a��b�ĺ�");
        }
        else if(Vector3.Dot(a.position.normalized,a.position.normalized) > 0) {
            Console.WriteLine("a��b��ǰ��");
        }
        else {
            Console.WriteLine("һ������");
        }
    }
}
class Entity {
    public Vector3 position {
        get;
    }
    public Vector3 direction {
        get;
    }
}
