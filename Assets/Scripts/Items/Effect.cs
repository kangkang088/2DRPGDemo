using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {
    public float destroyTime;
    // Start is called before the first frame update
    void Start() {
        if(destroyTime != -1)
            Destroy(gameObject,destroyTime);
    }
}
