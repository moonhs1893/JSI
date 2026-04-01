using System.Text;
using X;
using UnityEngine;

namespace JSI.Cmd {
    public class JSICmdToDollyCamera : XLoggableCmd {
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // private constructor
        private JSICmdToDollyCamera(XApp app) : base(app) {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPenMark penMark = jsi.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = penMark.getRecentPt(1);
            this.mCurPt = penMark.getRecentPt(0);
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToDollyCamera cmd = new JSICmdToDollyCamera(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPerspCameraPerson cp = jsi.getPerspCameraPerson();

            // create a plane on the pivot, directly facing the camera.
            Plane pivotPlane = new Plane(-cp.getView(), cp.getPivot());

            // project the previous screen point to the plane. 
            Ray prevPtRay = cp.getCamera().ScreenPointToRay(this.mPrevPt);
            float prevPtDist = float.NaN;
            pivotPlane.Raycast(prevPtRay, out prevPtDist);
            Vector3 prevPtOnPlane = prevPtRay.GetPoint(prevPtDist);

            // project the current screen point to the plane. 
            Ray curPtRay = cp.getCamera().ScreenPointToRay(this.mCurPt);
            float curPtDist = float.NaN;
            pivotPlane.Raycast(curPtRay, out curPtDist);
            Vector3 curPtOnPlane = curPtRay.GetPoint(curPtDist);

            // calculate the position difference between the two points.
            Vector3 offset = curPtOnPlane - prevPtOnPlane;

            // update the postion of the camera.
            cp.setEye(cp.getEye() - offset);

            return true;
        }

        protected override XJson createLogData() {
            JSIApp jsi = (JSIApp)this.mApp;
            XJson data = new XJson();
            JSIPerspCameraPerson cp = jsi.getPerspCameraPerson();
            data.addMember("prevPt", this.mPrevPt);
            data.addMember("curPt", this.mCurPt);
            data.addMember("eye", cp.getEye());
            data.addMember("view", cp.getView());
            data.addMember("up", cp.getUp());
            return data;
        }
    }
}