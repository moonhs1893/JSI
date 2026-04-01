using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;
using JSI.AppObject;
using JSI.Geom;

namespace JSI.Cmd {
    public class JSICmdToAddCurPtCurve2DToPtCurve2Ds : XLoggableCmd {
        // private constructor
        private JSICmdToAddCurPtCurve2DToPtCurve2Ds(XApp app) : base(app) {
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToAddCurPtCurve2DToPtCurve2Ds cmd = 
                new JSICmdToAddCurPtCurve2DToPtCurve2Ds(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIAppPolyline2D curPtCurve2D = 
                jsi.getPtCurve2DMgr().getCurPtCurve2D();
            JSIPolyline2D polyline = (JSIPolyline2D)curPtCurve2D.getGeom();
            if (polyline.getPts().Count >= 2) {
                jsi.getPtCurve2DMgr().getPtCurve2Ds().Add(curPtCurve2D);
                jsi.getPtCurve2DMgr().setCurPtCurve2D(null);
                return true;
            } else {
                curPtCurve2D.destroyGameObject();
                jsi.getPtCurve2DMgr().setCurPtCurve2D(null);
                return false;
            }
        }

        protected override XJson createLogData() {
            JSIApp jsi = (JSIApp)this.mApp;
            XJson data = new XJson();
            data.addMember("ptCurve2DCount", jsi.getPtCurve2DMgr().
                getPtCurve2Ds().Count);
            return data;
        }
    }
}