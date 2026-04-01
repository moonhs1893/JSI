using JSI.Cmd;
using UnityEngine;
using X;
using JSI.AppObject;
using System.Collections.Generic;
using JSI.Geom;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario : XScenario {
        public class MoveLineInStandingCardScene : JSIScene {
            // singleton pattern 
            private static MoveLineInStandingCardScene mSingleton = null;
            public static MoveLineInStandingCardScene getSingleton() {
                Debug.Assert(MoveLineInStandingCardScene.mSingleton != null);
                return MoveLineInStandingCardScene.mSingleton;
            }
            public static MoveLineInStandingCardScene createSingleton(
                XScenario scenario) {
                Debug.Assert(MoveLineInStandingCardScene.mSingleton == null);
                MoveLineInStandingCardScene.mSingleton = new 
                    MoveLineInStandingCardScene(scenario);
                return MoveLineInStandingCardScene.mSingleton;
            }
            private MoveLineInStandingCardScene(XScenario scenario) : 
                base(scenario) {
            }

            // fields
            private JSIAppPolyline3D mSelectedLine = null;
            private Vector3 mPrevLinePos = Vector3.zero;
            private float mTrailDistanceAccum = 0f;
            private const float TRAIL_INTERVAL = 0.01f; // distance between trail points
            private List<Vector3> mTrailPoints = new List<Vector3>();

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(jsi, this.mReturnScene, null);
                        break;
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(jsi,
                            JSIEditStandingCardScenario.RotateStandingCardScene.
                            getSingleton(), this.mReturnScene);
                        break;
                }
            }

            public override void handlePenDown(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSIStandingCard selectedSC =
                    JSIEditStandingCardScenario.getSingleton().
                    getSelectedStandingCard();
                if (selectedSC != null) {
                    // set prev pos for standing card trail
                    this.mPrevLinePos = selectedSC.getGameObject().transform.position;
                    this.mTrailPoints.Clear();
                    this.mTrailPoints.Add(this.mPrevLinePos);
                    this.mTrailDistanceAccum = 0f;
                    this.mSelectedLine = null; // reset selection

                    // check for line selection
                    foreach (JSIAppPolyline3D line in selectedSC.getPtCurve3Ds()) {
                        if (jsi.getCursor().hits(line)) {
                            this.mSelectedLine = line;
                            // calculate initial position (centroid) in world space
                            JSIPolyline3D polyline = (JSIPolyline3D)line.getGeom();
                            Transform cardTransform = selectedSC.getCard().getGameObject().transform;
                            Vector3 worldCentroid = cardTransform.TransformPoint(polyline.calcCentroid());
                            this.mPrevLinePos = worldCentroid;
                            this.mTrailPoints.Clear();
                            this.mTrailPoints.Add(worldCentroid);
                            this.mTrailDistanceAccum = 0f;
                            break;
                        }
                    }
                }
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                if (this.mSelectedLine != null) {
                    // move the selected line
                    JSIPerspCameraPerson cp = jsi.getPerspCameraPerson();

                    // create the ground plane.
                    Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

                    // get previous and current points
                    JSIPenMark penMark = jsi.getPenMarkMgr().getLastPenMark();
                    Vector2 prevPt = penMark.getRecentPt(1);
                    Vector2 curPt = penMark.getRecentPt(0);

                    // project the previous screen point to the plane. 
                    Ray prevPtRay = cp.getCamera().ScreenPointToRay(prevPt);
                    float prevPtDist = float.NaN;
                    groundPlane.Raycast(prevPtRay, out prevPtDist);
                    Vector3 prevPtOnPlane = prevPtRay.GetPoint(prevPtDist);

                    // project the current screen point to the plane. 
                    Ray curPtRay = cp.getCamera().ScreenPointToRay(curPt);
                    float curPtDist = float.NaN;
                    groundPlane.Raycast(curPtRay, out curPtDist);
                    Vector3 curPtOnPlane = curPtRay.GetPoint(curPtDist);

                    // calculate the position difference between the two points. 
                    Vector3 diff = curPtOnPlane - prevPtOnPlane;

                    // move the selected line
                    JSIStandingCard selectedSC =
                        JSIEditStandingCardScenario.getSingleton().
                        getSelectedStandingCard();
                    Transform cardTransform = selectedSC.getCard().getGameObject().transform;
                    Vector3 localDiff = cardTransform.InverseTransformVector(diff);
                    JSIPolyline3D polyline = (JSIPolyline3D)this.mSelectedLine.getGeom();
                    List<Vector3> pts = polyline.getPts();
                    for (int i = 0; i < pts.Count; i++) {
                        pts[i] += localDiff;
                    }
                    this.mSelectedLine.setPts(pts);

                    // update trail
                    Vector3 curLinePos = polyline.calcCentroid();
                    Vector3 worldCurLinePos = cardTransform.TransformPoint(curLinePos);
                    Vector3 moveVec = worldCurLinePos - this.mPrevLinePos;
                    float moveDist = moveVec.magnitude;
                    this.mTrailDistanceAccum += moveDist;

                    while (this.mTrailDistanceAccum >= TRAIL_INTERVAL) {
                        Vector3 trailPoint = this.mPrevLinePos + moveVec.normalized * (moveDist - (this.mTrailDistanceAccum - TRAIL_INTERVAL));
                        this.mTrailPoints.Add(trailPoint);
                        this.mTrailDistanceAccum -= TRAIL_INTERVAL;
                    }

                    this.mPrevLinePos = worldCurLinePos;
                } else {
                    // move the standing card and add trail
                    JSICmdToMoveStandingCard.execute(jsi);
                    JSIStandingCard selectedSC =
                        JSIEditStandingCardScenario.getSingleton().
                        getSelectedStandingCard();
                    Vector3 currentPos = selectedSC.getGameObject().transform.position;
                    Vector3 moveVec = currentPos - this.mPrevLinePos;
                    float moveDist = moveVec.magnitude;
                    this.mTrailDistanceAccum += moveDist;

                    while (this.mTrailDistanceAccum >= TRAIL_INTERVAL) {
                        Vector3 trailPoint = this.mPrevLinePos + moveVec.normalized * (moveDist - (this.mTrailDistanceAccum - TRAIL_INTERVAL));
                        this.mTrailPoints.Add(trailPoint);
                        this.mTrailDistanceAccum -= TRAIL_INTERVAL;
                    }

                    this.mPrevLinePos = currentPos;
                }
            }

            public override void handlePenUp(Vector2 pt) {
                if (this.mSelectedLine != null && this.mTrailPoints.Count > 1) {
                    JSIStandingCard selectedSC =
                        JSIEditStandingCardScenario.getSingleton().
                        getSelectedStandingCard();
                    // convert trail points to local space of the card
                    List<Vector3> localTrailPoints = new List<Vector3>();
                    Transform cardTransform = selectedSC.getCard().getGameObject().transform;
                    foreach (Vector3 worldPt in this.mTrailPoints) {
                        localTrailPoints.Add(cardTransform.InverseTransformPoint(worldPt));
                    }
                    // create trail polyline
                    JSIAppPolyline3D trail = new JSIAppPolyline3D("Trail", localTrailPoints, 0.05f, new Color(1f, 1f, 1f, 0.5f));
                    // set material to support transparency
                    LineRenderer lr = trail.getGameObject().GetComponent<LineRenderer>();
                    lr.material = new Material(Shader.Find("Unlit/Transparent"));
                    lr.material.color = trail.getColor();
                    selectedSC.getTrails().Add(trail);
                    selectedSC.getCard().addChild(trail);
                }
                this.mSelectedLine = null;
                this.mTrailPoints.Clear();
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(jsi,
                    JSINavigateScenario.TranslateReadyScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void getReady() {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();

                // deactivate all stands. 
                // deactivate all scale handles. 
                foreach (JSIStandingCard sc in
                    jsi.getStandingCardMgr().getStandingCards()) {
                    sc.getStand().getGameObject().SetActive(false);
                    sc.getScaleHandle().getGameObject().SetActive(false);
                }

                // activate and highlight only the selected stand. 
                JSIStandingCard selectedSC =
                    JSIEditStandingCardScenario.getSingleton().
                    getSelectedStandingCard();
                selectedSC.getStand().getGameObject().SetActive(true);
                selectedSC.highlightStand(true);
            }

            public override void wrapUp() {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSICmdToTakeSnapshot.execute(jsi);
            }
        }
    }
}