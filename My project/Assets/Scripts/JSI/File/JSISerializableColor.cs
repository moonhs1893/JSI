using System;
using UnityEngine;

/*
see JSON official documentation
https://json.org/json-en.html

{ "r": #, "g": #, "b": #, "a": # }
*/
namespace JSI.File {
    [Serializable]
    public class JSISerializableColor {
        // fields
        public float r;
        public float g;
        public float b;
        public float a;

        // constructor
        public JSISerializableColor(Color c) {
            this.r = c.r;
            this.g = c.g;
            this.b = c.b;
            this.a = c.a;
        }

        // methods
        public Color toColor() {
            return new Color(this.r, this.g, this.b, this.a);
        }
    }
}