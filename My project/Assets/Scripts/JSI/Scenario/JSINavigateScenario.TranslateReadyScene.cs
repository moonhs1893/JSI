using JSI.Cmd;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSINavigateScenario : XScenario {
        public class TranslateReadyScene : JSIScene {
            // singleton pattern 
            private static TranslateReadyScene mSingleton = null;
            public static TranslateReadyScene getSingleton() {
                Debug.Assert(TranslateReadyScene.mSingleton != null);
                return TranslateReadyScene.mSingleton;
            }
            public static TranslateReadyScene createSingleton(XScenario scenario) {
                Debug.Assert(TranslateReadyScene.mSingleton == null);
                TranslateReadyScene.mSingleton = new TranslateReadyScene(scenario);
                return TranslateReadyScene.mSingleton;
            }
            private TranslateReadyScene(XScenario scenario) : base(scenario) {
            }

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
                            JSINavigateScenario.RotateReadyScene.getSingleton(),
                            this.mReturnScene);
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
                            getSingleton(), this.mReturnScene);
                        return;
                    }
                }

                XCmdToChangeScene.execute(jsi,
                    JSINavigateScenario.DollyCameraScene.getSingleton(),
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