using System;
using System.Text;
using JSI.File;
using UnityEditor;
using UnityEngine;
using X;

namespace JSI.Cmd {
    public class JSICmdToSaveFile : XLoggableCmd {
        //fields
        private string mFilePath = string.Empty;

        // constructor
        private JSICmdToSaveFile(XApp app) : base(app) {}

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            JSICmdToSaveFile cmd = new JSICmdToSaveFile(app);
            return cmd.execute();
        }

        // private constructor
        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp) this.mApp;

            // open dialog at Desktop
            this.mFilePath = EditorUtility.SaveFilePanel("Save",
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                this.createDefaultFileName(), "jsi3d");

            // pressed 'SAVE' button
            if (this.mFilePath != string.Empty) {
                JSICmdToSaveFile.writeSketchFile(jsi, this.mFilePath);
                return true;

            // pressed 'CANCEL' button
            } else {
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            // "\" is a JSON escape character, so replace it with "/"
            data.addMember("filePath", this.mFilePath.Replace('\\', '/'));
            return data;
        }

        private string createDefaultFileName() {
            return DateTime.Now.ToString("yyyy_MMdd_HHmm_ss") + "_SKETCH";
        }

        public static void writeSketchFile(JSIApp jsi, string filePath) {
            // make new json file
            JSIAppData appData = new JSIAppData(DateTime.Now,
                jsi.getPerspCameraPerson().getEye(), jsi.getPerspCameraPerson().
                getView(), JSIPerspCameraPerson.FOV, jsi.getStandingCardMgr().
                getStandingCards());
            JSISerializableAppData sAppData = new JSISerializableAppData(
                appData);
            string json = JsonUtility.ToJson(sAppData);

            // write file
            System.IO.File.WriteAllText(filePath, json);
        }
    }
}