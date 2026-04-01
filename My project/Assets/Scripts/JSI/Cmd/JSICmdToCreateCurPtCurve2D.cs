using System.Text;
using X;
using UnityEngine;
using System.Collections.Generic;
using JSI.AppObject;

namespace JSI.Cmd {
    public class JSICmdToCreateCurPtCurve2D : XLoggableCmd {
        // fields
        private Vector2 mPt = JSIUtil.VECTOR2_NAN;

        // private constructor
        private JSICmdToCreateCurPtCurve2D(XApp app) : base(app) {
            JSIApp jsi = (JSIApp)this.mApp;
            this.mPt = jsi.getPenMarkMgr().getLastPenMark().getLastPt();
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToCreateCurPtCurve2D cmd = 
                new JSICmdToCreateCurPtCurve2D(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            List<Vector2> pts = new List<Vector2>();
            pts.Add(this.mPt);
            JSIAppPolyline2D ptCurve2D = new JSIAppPolyline2D("PtCurve2D",
                pts, JSIPtCurve2DMgr.PT_CURVE_WIDTH,
                JSIPtCurve2DMgr.PT_CURVE_COLOR);
            jsi.getPtCurve2DMgr().setCurPtCurve2D(ptCurve2D);
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("pt", this.mPt);
            return data;
        }
    }
}