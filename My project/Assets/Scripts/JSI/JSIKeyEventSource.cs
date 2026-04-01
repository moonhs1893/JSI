using System.Collections.Generic;
using UnityEngine;

namespace JSI {
    public class JSIKeyEventSource {
        // constants
        private static readonly List<KeyCode> WATCHING_KEYCODE =
            new List<KeyCode>() {
                KeyCode.LeftControl, // for rotating
                KeyCode.LeftAlt, // for translating
                KeyCode.Return, // for creating a standing card
                KeyCode.Z, // undo
                KeyCode.Y, // redo
                KeyCode.S, // for saving file
                KeyCode.O, // for opening file
                KeyCode.N, // for moving lines in standing card
            };

        // fields
        private JSIEventListener mEventListener = null;
        public void setEventListener(JSIEventListener eventListener) {
            this.mEventListener = eventListener;
        }

        // constructor 
        public JSIKeyEventSource() {
        }

        // methods
        public void update() {
            foreach (KeyCode kc in JSIKeyEventSource.WATCHING_KEYCODE) {
                if (Input.GetKeyDown(kc)) {
                    this.mEventListener.keyPressed(kc);
                }
                if (Input.GetKeyUp(kc)) {
                    this.mEventListener.keyReleased(kc);
                }
            }
        }
    }
}