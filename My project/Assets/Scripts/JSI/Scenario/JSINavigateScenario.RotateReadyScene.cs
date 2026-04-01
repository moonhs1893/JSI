using JSI.Cmd;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSINavigateScenario : XScenario {
        public class RotateReadyScene : JSIScene {
            // singleton pattern 
            private static RotateReadyScene mSingleton = null;
            public static RotateReadyScene getSingleton() {
                Debug.Assert(RotateReadyScene.mSingleton != null);
                return RotateReadyScene.mSingleton;
            }
            public static RotateReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(RotateReadyScene.mSingleton == null);
                RotateReadyScene.mSingleton = new RotateReadyScene(scenario);
                return RotateReadyScene.mSingleton;
            }
            private RotateReadyScene(XScenario scenario) : base(scenario) {
            }

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(jsi,
                            JSINavigateScenario.TranslateReadyScene.getSingleton(),
                            this.mReturnScene);
                        break;
                }
            }

            public override void handleKeyUp(KeyCode kc) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftControl:
                        XCmdToChangeScene.execute(jsi, this.mReturnScene, null);
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
                            JSIEditStandingCardScenario.RotateStandingCardScene.
                            getSingleton(), this.mReturnScene);
                        return;
                    }
                }

                XCmdToChangeScene.execute(jsi,
                    JSINavigateScenario.TumbleCameraScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void handlePenDrag(Vector2 pt) {
            }

            public override void handlePenUp(Vector2 pt) {
            }

            public override void getReady() {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();

                // activate stands.
                // deactivate scale handles. 
                foreach (JSIStandingCard sc in 
                    jsi.getStandingCardMgr().getStandingCards()) {
                    sc.getStand().getGameObject().SetActive(true);
                    sc.highlightStand(false);
                    sc.getScaleHandle().getGameObject().SetActive(false);
                }
            }

            public override void wrapUp() {
            }
        }
    }
}