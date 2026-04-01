using System.Text;
using X;
using UnityEngine;

namespace JSI.Cmd {
    public class JSICmdToTumbleCamera : XLoggableCmd {
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // private constructor
        private JSICmdToTumbleCamera(XApp app) : base(app) {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPenMark penMark = jsi.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = penMark.getRecentPt(1);
            this.mCurPt = penMark.getRecentPt(0);
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToTumbleCamera cmd = new JSICmdToTumbleCamera(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPerspCameraPerson cp = jsi.getPerspCameraPerson();

            float dx = this.mCurPt.x - this.mPrevPt.x;
            float dy = this.mCurPt.y - this.mPrevPt.y;
            float dAzimuth = 180f * dx / Screen.width;
            float dZenith = 180f * dy / Screen.height;

            Quaternion qa = Quaternion.AngleAxis(dAzimuth, Vector3.up);
            Quaternion qz = Quaternion.AngleAxis(-dZenith, cp.getRight());

            Vector3 pivotToEye = cp.getEye() - cp.getPivot();
            Vector3 nextEye = cp.getPivot() + qa * qz * pivotToEye;
            Vector3 nextView = qa * qz * cp.getView();

            cp.setEye(nextEye);
            cp.setView(nextView);

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