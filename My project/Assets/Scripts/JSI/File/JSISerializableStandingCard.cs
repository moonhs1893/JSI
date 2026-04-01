using System;
using System.Collections.Generic;
using JSI.AppObject;
using JSI.Geom;
using UnityEngine;

/*
see JSON official documentation
https://json.org/json-en.html

{
    "width": #,
    "height": #,
    "pos": { "x": #, "y": #, "z": # },
    "rot": { "x": #, "y": #, "z": #, "w": # },
    "ptCurve3Ds": [
        {
            "pts": [{ "x": #, "y": #, "z": # }, ...],
            "width": #,
            "color": { "r": #, "g": #, "b": #, "a": # }
        },
        ...
    ]
}
*/
namespace JSI.File {
    [Serializable]
    public class JSISerializableStandingCard {
        // fields
        public string id = string.Empty;
        public float width = float.NaN;
        public float height = float.NaN;
        public JSISerializableVector3 pos = null;
        public JSISerializableQuaternion rot = null;
        public List<JSISerializableAppPolyline3D> ptCurve3Ds = null;
        public List<JSISerializableAppPolyline3D> trails = null;

        // constructor
        public JSISerializableStandingCard(JSIStandingCard sc) {
            this.id = sc.getId();

            JSIRect3D rect = (JSIRect3D)sc.getCard().getGeom();
            this.width = rect.getWidth();
            this.height = rect.getHeight();

            this.pos = new JSISerializableVector3(sc.getGameObject().transform.
                position);
            this.rot = new JSISerializableQuaternion(sc.getGameObject().transform.
                rotation);

            this.ptCurve3Ds = new List<JSISerializableAppPolyline3D>();
            foreach (JSIAppPolyline3D ptCurve in sc.getPtCurve3Ds()) {
                JSISerializableAppPolyline3D sPtCurve =
                    new JSISerializableAppPolyline3D(ptCurve);
                this.ptCurve3Ds.Add(sPtCurve);
            }

            this.trails = new List<JSISerializableAppPolyline3D>();
            foreach (JSIAppPolyline3D trail in sc.getTrails()) {
                JSISerializableAppPolyline3D sTrail =
                    new JSISerializableAppPolyline3D(trail);
                this.trails.Add(sTrail);
            }
        }

        // methods
        public JSIStandingCard toStandingCard() {
            string id = this.id;
            float width = this.width;
            float height = this.height;

            Vector3 pos = this.pos.toVector3();
            Quaternion rot = this.rot.toQuaternion();

            List<JSIAppPolyline3D> ptCurve3Ds = new List<JSIAppPolyline3D>();
            foreach (JSISerializableAppPolyline3D sPtCurve3D in this.ptCurve3Ds) {
                JSIAppPolyline3D ptCurve3D = sPtCurve3D.toAppPolyline3D();
                ptCurve3Ds.Add(ptCurve3D);
            }

            JSIStandingCard sc = new JSIStandingCard(id, width, height, pos, rot, ptCurve3Ds);

            if (this.trails != null) {
                foreach (JSISerializableAppPolyline3D sTrail in this.trails) {
                    JSIAppPolyline3D trail = sTrail.toAppPolyline3D();
                    sc.getTrails().Add(trail);
                    sc.getCard().addChild(trail);
                }
            }

            return sc;
        }
    }
}