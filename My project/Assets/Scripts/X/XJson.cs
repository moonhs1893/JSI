using System;
using System.Collections.Generic;
using UnityEngine;

// terminology and syntax follow the JSON official documentation
// https://json.org/json-en.html
namespace X {
    public class XJson {
        // constants
        private static readonly string QT = "\""; // quote
        private static readonly string CN = ":"; // colon
        private static readonly string CM = ","; // comma
        private static readonly string LSB = "["; // left square bracket
        private static readonly string RSB = "]"; // right square bracket
        private static readonly string LCB = "{"; // left curly bracket
        private static readonly string RCB = "}"; // right curly bracket
        private static readonly string TRUE = "true"; // 'true' keyword
        private static readonly string FALSE = "false"; // 'false' keyword
        private static readonly string NULL = "null"; // 'null' keyword

        // fields
        private List<string> mMembers = null;
        private string mJsonString = LCB + RCB;

        // constructor
        public XJson() {
            this.mMembers = new List<string>();
        }

        // methods
        // string
        public void addMember(string name, string value) {
            if (value == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                this.mMembers.Add(QT + name + QT + CN + QT + value + QT);
            }
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<string> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<string> formattedValues = new List<string>();
                foreach (string value in values) {
                    formattedValues.Add(QT + value + QT);
                }
                this.mMembers.Add(QT + name + QT + CN + LSB + String.Join(CM,
                    formattedValues) + RSB);
            }
            this.refreshJsonString();
        }
        // number
        public void addMember(string name, int value) {
            this.mMembers.Add(QT + name + QT + CN + value.ToString());
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<int> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<string> formattedValues = new List<string>();
                foreach (int value in values) {
                    formattedValues.Add(value.ToString());
                }
                this.mMembers.Add(QT + name + QT + CN + LSB + String.Join(CM,
                    formattedValues) + RSB);
            }
            this.refreshJsonString();
        }
        public void addMember(string name, float value) {
            this.mMembers.Add(QT + name + QT + CN + value.ToString());
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<float> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<string> formattedValues = new List<string>();
                foreach (float value in values) {
                    formattedValues.Add(value.ToString());
                }
                this.mMembers.Add(QT + name + QT + CN + LSB + String.Join(CM,
                    formattedValues) + RSB);
            }
            this.refreshJsonString();
        }
        public void addMember(string name, double value) {
            this.mMembers.Add(QT + name + QT + CN + value.ToString());
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<double> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<string> formattedValues = new List<string>();
                foreach (double value in values) {
                    formattedValues.Add(value.ToString());
                }
                this.mMembers.Add(QT + name + QT + CN + LSB + String.Join(CM,
                    formattedValues) + RSB);
            }
            this.refreshJsonString();
        }
        // boolean
        public void addMember(string name, bool value) {
            if (value) {
                this.mMembers.Add(QT + name + QT + CN + TRUE);
            } else {
                this.mMembers.Add(QT + name + QT + CN + FALSE);
            }
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<bool> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<string> formattedValues = new List<string>();
                foreach (bool value in values) {
                    if (value) {
                        formattedValues.Add(TRUE);
                    } else {
                        formattedValues.Add(FALSE);
                    }
                }
                this.mMembers.Add(QT + name + QT + CN + LSB + String.Join(CM,
                    formattedValues) + RSB);
            }
            this.refreshJsonString();
        }
        // object
        public override string ToString() {
            return this.mJsonString;
        }
        private void refreshJsonString() {
            this.mJsonString = LCB + String.Join(CM, this.mMembers) + RCB;
        }
        public void addMember(string name, XJson value) {
            this.mMembers.Add(QT + name + QT + CN + value.ToString());
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<XJson> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<string> formattedValues = new List<string>();
                foreach (XJson value in values) {
                    formattedValues.Add(value.ToString());
                }
                this.mMembers.Add(QT + name + QT + CN + LSB + String.Join(CM,
                    formattedValues) + RSB);
            }
            this.refreshJsonString();
        }
        // Vector2
        public void addMember(string name, Vector2 value) {
            XJson obj = new XJson();
            obj.addMember("x", value.x);
            obj.addMember("y", value.y);
            this.addMember(name, obj);
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<Vector2> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<XJson> objs = new List<XJson>();
                foreach (Vector2 value in values) {
                    XJson obj = new XJson();
                    obj.addMember("x", value.x);
                    obj.addMember("y", value.y);
                    objs.Add(obj);
                }
                this.addMember(name, objs);
            }
            this.refreshJsonString();
        }
        // Vector3
        public void addMember(string name, Vector3 value) {
            XJson obj = new XJson();
            obj.addMember("x", value.x);
            obj.addMember("y", value.y);
            obj.addMember("z", value.z);
            this.addMember(name, obj);
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<Vector3> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<XJson> objs = new List<XJson>();
                foreach (Vector3 value in values) {
                    XJson obj = new XJson();
                    obj.addMember("x", value.x);
                    obj.addMember("y", value.y);
                    obj.addMember("z", value.z);
                    objs.Add(obj);
                }
                this.addMember(name, objs);
            }
            this.refreshJsonString();
        }
        // Quaternion
        public void addMember(string name, Quaternion value) {
            XJson obj = new XJson();
            obj.addMember("x", value.x);
            obj.addMember("y", value.y);
            obj.addMember("z", value.z);
            obj.addMember("w", value.w);
            this.addMember(name, obj);
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<Quaternion> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<XJson> objs = new List<XJson>();
                foreach (Quaternion value in values) {
                    XJson obj = new XJson();
                    obj.addMember("x", value.x);
                    obj.addMember("y", value.y);
                    obj.addMember("z", value.z);
                    obj.addMember("w", value.w);
                    objs.Add(obj);
                }
                this.addMember(name, objs);
            }
            this.refreshJsonString();
        }
        // Color
        public void addMember(string name, Color value) {
            XJson obj = new XJson();
            obj.addMember("r", value.r);
            obj.addMember("g", value.g);
            obj.addMember("b", value.b);
            obj.addMember("a", value.a);
            this.addMember(name, obj);
            this.refreshJsonString();
        }
        public void addMember(string name, IEnumerable<Color> values) {
            if (values == null) {
                this.mMembers.Add(QT + name + QT + CN + NULL);
            } else {
                List<XJson> objs = new List<XJson>();
                foreach (Color value in values) {
                    XJson obj = new XJson();
                    obj.addMember("r", value.r);
                    obj.addMember("g", value.g);
                    obj.addMember("b", value.b);
                    obj.addMember("a", value.a);
                    objs.Add(obj);
                }
                this.addMember(name, objs);
            }
            this.refreshJsonString();
        }
    }
}