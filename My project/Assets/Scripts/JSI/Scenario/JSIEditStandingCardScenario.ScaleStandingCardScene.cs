using JSI.Cmd;
using UnityEngine;
using X;

namespace JSI.Scenario {
    public partial class JSIEditStandingCardScenario : XScenario {
        public class ScaleStandingCardScene : JSIScene {
            // singleton pattern 
            private static ScaleStandingCardScene mSingleton = null;
            public static ScaleStandingCardScene getSingleton() {
                Debug.Assert(ScaleStandingCardScene.mSingleton != null);
                return ScaleStandingCardScene.mSingleton;
            }
            public static ScaleStandingCardScene createSingleton(
                XScenario scenario) {
                Debug.Assert(ScaleStandingCardScene.mSingleton == null);
                ScaleStandingCardScene.mSingleton = new 
                    ScaleStandingCardScene(scenario);
                return ScaleStandingCardScene.mSingleton;
            }
            private ScaleStandingCardScene(XScenario scenario) : 
                base(scenario) {
            }

            // event handling methods
            public override void handleKeyDown(KeyCode kc) {
            }

            public override void handleKeyUp(KeyCode kc) {
            }
            
            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSICmdToScaleStandingCard.execute(jsi);
            }

            public override void handlePenUp(Vector2 pt) {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(jsi, this.mReturnScene, null);
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


                // activate and highlight only the selected scale handle. 
                JSIStandingCard selectedSC =
                    JSIEditStandingCardScenario.getSingleton().
                    getSelectedStandingCard();
                selectedSC.getScaleHandle().getGameObject().SetActive(true);
                selectedSC.highlightScaleHandle(true);
            }

            public override void wrapUp() {
                JSIApp jsi = (JSIApp)this.mScenario.getApp();
                JSICmdToTakeSnapshot.execute(jsi);
            }
        }
    }
}