using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFromTileController : MonoBehaviour, IsTree {

    System.DateTime age;

    public DateTime getAge() {
        return age;
    }

    private void Start() {
        age = System.DateTime.Now;
    }
}
