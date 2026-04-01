using System;
using UnityEngine;

/*
see JSON official documentation
https://json.org/json-en.html

{ "x": #, "y": #, "z": # }
*/
namespace JSI.File {
    [Serializable]
    public class JSISerializableVector3 {
        // fields
        public float x;
        public float y;
        public float z;

        // constructor
        public JSISerializableVector3(Vector3 v) {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        // methods
        public Vector3 toVector3() {
            return new Vector3(this.x, this.y, this.z);
        }
    }
}