using System;
using System.Collections.Generic;
using JSI.AppObject;
using JSI.Geom;
using UnityEngine;

/*
see JSON official documentation
https://json.org/json-en.html

{
    "pts": [{ "x": #, "y": #, "z": # }, ...]
    "width": #,
    "color": { "r": #, "g": #, "b": #, "a": # }
}
*/
namespace JSI.File {
    [Serializable]
    public class JSISerializableAppPolyline3D {
        // fields
        public List<JSISerializableVector3> pts = null;
        public JSISerializableColor color = null;
        public float width = float.NaN;

        // constructor
        public JSISerializableAppPolyline3D(JSIAppPolyline3D ptCurve3D) {
            this.pts = new List<JSISerializableVector3>();
            JSIPolyline3D polyline = (JSIPolyline3D) ptCurve3D.getGeom();
            foreach (Vector3 pt in polyline.getPts()) {
                JSISerializableVector3 sPt = new JSISerializableVector3(pt);
                this.pts.Add(sPt);
            }
            this.color = new JSISerializableColor(ptCurve3D.getColor());
            this.width = ptCurve3D.getWidth();
        }

        // methods
        public JSIAppPolyline3D toAppPolyline3D() {
            List<Vector3> pts = new List<Vector3>();
            foreach (JSISerializableVector3 sPt in this.pts) {
                Vector3 pt = sPt.toVector3();
                pts.Add(pt);
            }
            Color color = this.color.toColor();
            float width = this.width;

            return new JSIAppPolyline3D("PtCurve3D", pts, width, color);
        }
    }
}