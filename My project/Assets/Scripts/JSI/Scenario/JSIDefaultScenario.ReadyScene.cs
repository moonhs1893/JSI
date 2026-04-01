using JSI.Cmd;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIDefaultScenario : XScenario {
        public class ReadyScene : JSIScene {
            // singleton pattern 
            private static ReadyScene mSingleton = null;
            public static ReadyScene getSingleton() {
                Debug.Assert(ReadyScene.mSingleton != null);
                return ReadyScene.mSingleton;
            }
            public static ReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(ReadyScene.mSingleton == null);
                ReadyScene.mSingleton = new ReadyScene(scenario);
                return ReadyScene.mSingleton;
            }
            private ReadyScene(XScenario scenario) : base(scenario) {
            }

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(jsi,
                            JSINavigateScenario.RotateReadyScene.getSingleton(),
                            this);
                        break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.Return:
                        //JSICmdToCreateEmptyStandingCard.execute(jsi);
                        JSICmdToCreateStandingCard.execute(jsi);
                        JSICmdToTakeSnapshot.execute(jsi);
                        break;
                    case KeyCode.Z:
                        JSICmdToUndo.execute(jsi);
                        break;
                    case KeyCode.Y:
                        JSICmdToRedo.execute(jsi);
                        break;
                    case KeyCode.S:
                        JSICmdToSaveFile.execute(jsi);
                        break;
                    case KeyCode.O:
                        JSICmdToOpenFile.execute(jsi);
                        break;
                }
            }

            public override void handlePenDown(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                foreach (JSIStandingCard sc in 
                    jsi.getStandingCardMgr().getStandingCards()) {
                    if (jsi.getCursor().hits(sc.getStand())) {
                        JSICmdToSelectSmallestStandingCardByStand.execute(jsi);
                        XCmdToChangeScene.execute(jsi,
                            JSIEditStandingCardScenario.MoveStandingCardScene.
                            getSingleton(), this);
                        return;
                    }
                    if (jsi.getCursor().hits(sc.getScaleHandle())) {
                        JSICmdToSelectSmallestStandingCardByScaleHandle.
                            execute(jsi);
                        XCmdToChangeScene.execute(jsi,
                            JSIEditStandingCardScenario.ScaleStandingCardScene.
                            getSingleton(), this);
                        return;
                    }
                }
                JSICmdToCreateCurPtCurve2D.execute(jsi);
                XCmdToChangeScene.execute(jsi,
                    JSIDrawScenario.DrawScene.getSingleton(), this);
            }

            public override void handlePenDrag(Vector2 pt) {
            }

            public override void handlePenUp(Vector2 pt) {
            }

            public override void getReady() {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();

                // deactivate stands. 
                // activate scale handles. 
                foreach (JSIStandingCard sc in 
                    jsi.getStandingCardMgr().getStandingCards()) {
                    sc.getStand().getGameObject().SetActive(false);
                    sc.getScaleHandle().getGameObject().SetActive(true);
                    sc.highlightScaleHandle(false);
                }

                // auto save log & sketch
                // should be added to all 'M' scenes
                JSICmdToAutoSave.execute(jsi);
            }

            public override void wrapUp() {
            }
        }
    }
}