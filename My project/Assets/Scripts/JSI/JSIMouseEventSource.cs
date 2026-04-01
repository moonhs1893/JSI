using UnityEngine;

namespace JSI {
    public class JSIMouseEventSource {
        // constants
        private static readonly int LEFT_BUTTON = 0;
        private static readonly int RIGHT_BUTTON = 1;

        // fields
        private JSIEventListener mEventListener = null;
        public void setEventListener(JSIEventListener eventListener) {
            this.mEventListener = eventListener;
        }
        private bool mWasLeftPressed = false;
        private bool mIsLeftPressed = false;
        private bool mWasRightPressed = false;
        private bool mIsRightPressed = false;
        private Vector2 mPrevPt = Vector2.zero;
        private Vector2 mCurPt = Vector2.zero;

        // constructor
        public JSIMouseEventSource() {
        }

        // methods
        public void update() {
            this.mIsLeftPressed = 
                Input.GetMouseButton(JSIMouseEventSource.LEFT_BUTTON);
            this.mIsRightPressed =
                Input.GetMouseButton(JSIMouseEventSource.RIGHT_BUTTON);
            this.mCurPt = Input.mousePosition;

            // move
            if (!this.mIsLeftPressed && !this.mIsRightPressed &&
                this.mPrevPt != this.mCurPt) {
                this.mEventListener.mouseMoved(this.mCurPt);
            }
            // left press
            if (!this.mWasLeftPressed && this.mIsLeftPressed) {
                this.mEventListener.mouseLeftPressed(this.mCurPt);
            }
            // left drag
            if (this.mWasLeftPressed && this.mIsLeftPressed &&
                this.mPrevPt != this.mCurPt) {
                this.mEventListener.mouseLeftDragged(this.mCurPt);
            }
            // left release
            if (this.mWasLeftPressed && !this.mIsLeftPressed) {
                this.mEventListener.mouseLeftReleased(this.mCurPt);
            }
            // right press
            if (!this.mWasRightPressed && this.mIsRightPressed) {
                this.mEventListener.mouseRightPressed(this.mCurPt);
            }
            // right drag
            if (this.mWasRightPressed && this.mIsRightPressed &&
                this.mPrevPt != this.mCurPt) {
                this.mEventListener.mouseRightDragged(this.mCurPt);
            }
            // right release
            if (this.mWasRightPressed && !this.mIsRightPressed) {
                this.mEventListener.mouseRightReleased(this.mCurPt);
            }

            // updates the previous data
            this.mWasLeftPressed = this.mIsLeftPressed;
            this.mWasRightPressed = this.mIsRightPressed;
            this.mPrevPt = this.mCurPt;
        }
    }
}