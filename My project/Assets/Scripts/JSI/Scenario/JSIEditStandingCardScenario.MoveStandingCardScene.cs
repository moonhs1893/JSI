using JSI.Cmd;
using JSI.AppObject;
using JSI.Geom;
using System.Collections.Generic;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario : XScenario {
        public class MoveStandingCardScene : JSIScene {
            // singleton pattern 
            private static MoveStandingCardScene mSingleton = null;
            public static MoveStandingCardScene getSingleton() {
                Debug.Assert(MoveStandingCardScene.mSingleton != null);
                return MoveStandingCardScene.mSingleton;
            }
            public static MoveStandingCardScene createSingleton(
                XScenario scenario) {
                Debug.Assert(MoveStandingCardScene.mSingleton == null);
                MoveStandingCardScene.mSingleton = new 
                    MoveStandingCardScene(scenario);
                return MoveStandingCardScene.mSingleton;
            }
            private MoveStandingCardScene(XScenario scenario) : 
                base(scenario) {
            }

            // trail mode fields
            private bool mIsNPressed = false;
            private Vector3 mPrevCardPos = Vector3.zero;
            private float mTrailStampAccum = 0f;
            private const float TRAIL_STAMP_INTERVAL = 0.05f;

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
                if (kc == KeyCode.N) {
                    this.mIsNPressed = true;
                    return;
                }

                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(jsi,
                            JSIEditStandingCardScenario.MoveLineInStandingCardScene.
                            getSingleton(), this);
                        break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                if (kc == KeyCode.N) {
                    this.mIsNPressed = false;
                    return;
                }

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
                JSIStandingCard selectedSC =
                    JSIEditStandingCardScenario.getSingleton().
                    getSelectedStandingCard();
                if (selectedSC != null) {
                    this.mPrevCardPos = selectedSC.getGameObject().transform.position;
                    this.mTrailStampAccum = 0f;
                }
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSICmdToMoveStandingCard.execute(jsi);

                if (!this.mIsNPressed) {
                    return;
                }

                JSIStandingCard selectedSC =
                    JSIEditStandingCardScenario.getSingleton().
                    getSelectedStandingCard();
                if (selectedSC == null) {
                    return;
                }

                Vector3 currentPos = selectedSC.getGameObject().transform.position;
                Vector3 moveVec = currentPos - this.mPrevCardPos;
                float moveDist = moveVec.magnitude;

                this.mTrailStampAccum += moveDist;

                if (moveDist > 0f) {
                    while (this.mTrailStampAccum >= TRAIL_STAMP_INTERVAL) {
                        float t = (this.mTrailStampAccum - TRAIL_STAMP_INTERVAL) / moveDist;
                        t = Mathf.Clamp01(1f - t);
                        Vector3 stampPos = Vector3.Lerp(currentPos, this.mPrevCardPos, t);

                        // create trail copies of current lines in world coordinates
                        foreach (JSIAppPolyline3D line in selectedSC.getPtCurve3Ds()) {
                            JSIPolyline3D sourcePolyline = (JSIPolyline3D)line.getGeom();
                            List<Vector3> worldPts = new List<Vector3>();
                            Transform cardTransform = selectedSC.getCard().getGameObject().transform;
                            foreach (Vector3 localPt in sourcePolyline.getPts()) {
                                worldPts.Add(cardTransform.TransformPoint(localPt));
                            }

                            JSIAppPolyline3D trail = new JSIAppPolyline3D("Trail", worldPts, line.getWidth(), new Color(line.getColor().r, line.getColor().g, line.getColor().b, 0.4f));
                            trail.getGameObject().transform.position = Vector3.zero;
                            trail.getGameObject().transform.rotation = Quaternion.identity;

                            selectedSC.getTrails().Add(trail);
                        }

                        this.mTrailStampAccum -= TRAIL_STAMP_INTERVAL;
                    }
                }

                this.mPrevCardPos = currentPos;
            }

            public override void handlePenUp(Vector2 pt) {
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