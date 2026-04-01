using System.Text;
using X;
using UnityEngine;
using JSI.Scenario;
using JSI.AppObject;
using System.Collections.Generic;
using JSI.Geom;

namespace JSI.Cmd {
    public class JSICmdToScaleStandingCard : XLoggableCmd {
        // fields
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // private constructor
        private JSICmdToScaleStandingCard(XApp app) : base(app) {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPenMark penMark = jsi.getPenMarkMgr().getLastPenMark();
            this.mPrevPt = penMark.getRecentPt(1);
            this.mCurPt = penMark.getRecentPt(0);
        }

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToScaleStandingCard cmd = 
                new JSICmdToScaleStandingCard(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            JSIPerspCameraPerson cp = jsi.getPerspCameraPerson();
            JSIEditStandingCardScenario scenario =
                JSIEditStandingCardScenario.getSingleton();
            JSIStandingCard standingCard =
                scenario.getSelectedStandingCard();
            JSIAppRect3D card = standingCard.getCard();
            JSIAppCircle3D stand = standingCard.getStand();
            JSIAppCircle3D scaleHandle = standingCard.getScaleHandle();
            List<JSIAppPolyline3D> ptCurve3Ds =
                standingCard.getPtCurve3Ds();
            JSIRect3D rect = (JSIRect3D)card.getGeom();

            // create the ground plane.
            Plane cardPlane = new Plane(
                standingCard.getGameObject().transform.forward,
                standingCard.getGameObject().transform.position);

            // project the previous screen point to the plane. 
            Ray prevPtRay = cp.getCamera().ScreenPointToRay(this.mPrevPt);
            float prevPtDist = float.NaN;
            cardPlane.Raycast(prevPtRay, out prevPtDist);
            Vector3 prevPtOnPlane = prevPtRay.GetPoint(prevPtDist);

            // project the current screen point to the plane. 
            Ray curPtRay = cp.getCamera().ScreenPointToRay(this.mCurPt);
            float curPtDist = float.NaN;
            cardPlane.Raycast(curPtRay, out curPtDist);
            Vector3 curPtOnPlane = curPtRay.GetPoint(curPtDist);

            // calculate the scale factor.
            float scaleFactor = curPtOnPlane.y / prevPtOnPlane.y;

            // resized the standing card. 
            float newCardWidth = scaleFactor * rect.getWidth();
            float newCardHeight = scaleFactor * rect.getHeight();
            card.setSize(newCardWidth, newCardHeight);

            // change the postion of the standing card (and its card).
            Vector3 standingCardPosition =
                standingCard.getGameObject().transform.position;
            Vector3 newStandingCardPosition = new Vector3(
                standingCardPosition.x, scaleFactor * standingCardPosition.y,
                standingCardPosition.z);
            standingCard.getGameObject().transform.position =
                newStandingCardPosition;

            // change the postion of the stand. 
            Vector3 standLocalPos =
                stand.getGameObject().transform.localPosition;
            Vector3 newStandLocalPos = new Vector3(
                standLocalPos.x, scaleFactor * standLocalPos.y, 
                standLocalPos.z);
            stand.getGameObject().transform.localPosition =
                newStandLocalPos;
            stand.setRadius(newCardWidth / 2f);

            // change the postion of the scale handle. 
            Vector3 scaleHandleLocalPos =
                scaleHandle.getGameObject().transform.localPosition;
            Vector3 newScaleHandleLocalPos = new Vector3(
                scaleHandleLocalPos.x, scaleFactor * scaleHandleLocalPos.y,
                scaleHandleLocalPos.z);
            scaleHandle.getGameObject().transform.localPosition =
                newScaleHandleLocalPos;

            // scale the 3D point curves. 
            if (ptCurve3Ds != null) {
                foreach (JSIAppPolyline3D ptCurve3D in ptCurve3Ds) {
                    JSIPolyline3D polyline =
                        (JSIPolyline3D)ptCurve3D.getGeom();
                    List<Vector3> scaledPt3Ds = new List<Vector3>();
                    foreach (Vector3 pt3D in polyline.getPts()) {
                        Vector3 scaledPt3D = new Vector3(
                            scaleFactor * pt3D.x, scaleFactor * pt3D.y, 
                            pt3D.z);
                        scaledPt3Ds.Add(scaledPt3D);
                    }
                    ptCurve3D.setPts(scaledPt3Ds);
                }
            }
            
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("prevPt", this.mPrevPt);
            data.addMember("curPt", this.mCurPt);
            JSIStandingCard sc = JSIEditStandingCardScenario.getSingleton().
                getSelectedStandingCard();
            JSIRect3D rect = (JSIRect3D)sc.getCard().getGeom();
            float w = rect.getWidth();
            float h = rect.getHeight();
            data.addMember("cardWidth", w);
            data.addMember("cardHeight", h);
            return data;
        }
    }
}