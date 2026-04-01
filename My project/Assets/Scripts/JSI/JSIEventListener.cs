using UnityEngine;

namespace JSI {
    public class JSIEventListener {
        // fields
        private JSIApp mJSI = null;

        // constructor 
        public JSIEventListener(JSIApp jsi) {
            this.mJSI = jsi;
        }

        // methods
        public void keyPressed(KeyCode kc) {
            //Debug.Log("keyPressed: " + kc);
            JSIScene curScene = 
                (JSIScene)this.mJSI.getScenarioMgr().getCurScene();
            curScene.handleKeyDown(kc);
        }
        public void keyReleased(KeyCode kc) {
            //Debug.Log("keyReleased: " + kc);
            JSIScene curScene =
                (JSIScene)this.mJSI.getScenarioMgr().getCurScene();
            curScene.handleKeyUp(kc);
        }
        public void mouseMoved(Vector2 pt) {
            //Debug.Log("mouseMoved: " + pt);
            this.mJSI.getCursor().getGameObject().transform.position = pt;
        }
        public void mouseLeftPressed(Vector2 pt) {
            //Debug.Log("mouseLeftPressed: " + pt);
            this.mJSI.getCursor().getGameObject().transform.position = pt;
            if (this.mJSI.getPenMarkMgr().penDown(pt)) {
                JSIScene curScene =
                    (JSIScene)this.mJSI.getScenarioMgr().getCurScene();
                curScene.handlePenDown(pt);
            }
        }
        public void mouseLeftDragged(Vector2 pt) {
            //Debug.Log("mouseLeftDragged: " + pt);
            this.mJSI.getCursor().getGameObject().transform.position = pt;
            if (this.mJSI.getPenMarkMgr().penDrag(pt)) {
                JSIScene curScene =
                    (JSIScene)this.mJSI.getScenarioMgr().getCurScene();
                curScene.handlePenDrag(pt);
            }
        }
        public void mouseLeftReleased(Vector2 pt) {
            //Debug.Log("mouseLeftReleased: " + pt);
            this.mJSI.getCursor().getGameObject().transform.position = pt;
            if (this.mJSI.getPenMarkMgr().penUp(pt)) {
                JSIScene curScene =
                    (JSIScene)this.mJSI.getScenarioMgr().getCurScene();
                curScene.handlePenUp(pt);
            }
        }
        public void mouseRightPressed(Vector2 pt) {
            //Debug.Log("mouseRightPressed: " + pt);
        }
        public void mouseRightDragged(Vector2 pt) {
            //Debug.Log("mouseRightDragged: " + pt);
        }
        public void mouseRightReleased(Vector2 pt) {
            //Debug.Log("mouseRightReleased: " + pt);
        }
    }
}