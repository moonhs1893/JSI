using JSI.Cmd;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario : XScenario {
        public class RotateStandingCardScene : JSIScene {
            // singleton pattern 
            private static RotateStandingCardScene mSingleton = null;
            public static RotateStandingCardScene getSingleton() {
                Debug.Assert(RotateStandingCardScene.mSingleton != null);
                return RotateStandingCardScene.mSingleton;
            }
            public static RotateStandingCardScene createSingleton(
                XScenario scenario) {
                Debug.Assert(RotateStandingCardScene.mSingleton == null);
                RotateStandingCardScene.mSingleton = new 
                    RotateStandingCardScene(scenario);
                return RotateStandingCardScene.mSingleton;
            }
            private RotateStandingCardScene(XScenario scenario) : 
                base(scenario) {
            }

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                switch (kc) {
                    case KeyCode.LeftAlt:
                        XCmdToChangeScene.execute(jsi,
                            JSIEditStandingCardScenario.MoveStandingCardScene.
                            getSingleton(), this.mReturnScene);
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
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSICmdToRotateStandingCard.execute(jsi);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(jsi,
                    JSINavigateScenario.RotateReadyScene.getSingleton(),
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