using System;
using UnityEngine;

/*
see JSON official documentation
https://json.org/json-en.html

{ "x": #, "y": #, "z": #, "w": # }
*/
namespace JSI.File {
    [Serializable]
    public class JSISerializableQuaternion {
        // fields
        public float x;
        public float y;
        public float z;
        public float w;

        // constructor
        public JSISerializableQuaternion(Quaternion q) {
            this.x = q.x;
            this.y = q.y;
            this.z = q.z;
            this.w = q.w;
        }

        // methods
        public Quaternion toQuaternion() {
            return new Quaternion(this.x, this.y, this.z, this.w);
        }
    }
}