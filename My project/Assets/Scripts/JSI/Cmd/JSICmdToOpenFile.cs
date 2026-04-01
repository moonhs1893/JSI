using System;
using JSI.AppObject;
using JSI.File;
using UnityEditor;
using UnityEngine;
using X;

namespace JSI.Cmd {
    public class JSICmdToOpenFile : XLoggableCmd {
        //fields
        private string mFilePath = string.Empty;

        // constructor
        private JSICmdToOpenFile(XApp app) : base(app) { }

        // static method to construct and execute this method
        public static bool execute(XApp app) {
            JSICmdToOpenFile cmd = new JSICmdToOpenFile(app);
            return cmd.execute();
        }

        // private constructor
        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;

            // open dialog at Desktop
            this.mFilePath = EditorUtility.OpenFilePanel("Open",
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "jsi3d");

            // pressed 'OPEN' button
            if (this.mFilePath != string.Empty) {
                if (this.readFile(this.mFilePath)) {
                    jsi.getSnapshotMgr().restartHistory();
                    return true;
                } else {
                    return false;
                }
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

        private bool readFile(string filePath) {
            JSIApp jsi = (JSIApp)this.mApp;

            // parse json file (it may fail if format is incorrect)
            try {
                string json = System.IO.File.ReadAllText(filePath);
                JSISerializableAppData sAppData = JsonUtility.
                    FromJson<JSISerializableAppData>(json);
                JSIAppData appData = sAppData.toAppData();
                this.clearAppData();
                this.loadAppData(appData);
                return true;
            } catch {
                return false;
            }
        }

        private void clearAppData() {
            JSIApp jsi = (JSIApp)this.mApp;

            // remove all standing cards
            foreach (JSIStandingCard sc in jsi.getStandingCardMgr().
                getStandingCards()) {

                sc.destroyGameObject();
            }
            jsi.getStandingCardMgr().getStandingCards().Clear();

            // reset camera
            jsi.getPerspCameraPerson().setEye(JSIPerspCameraPerson.HOME_EYE);
            jsi.getPerspCameraPerson().setView(JSIPerspCameraPerson.HOME_VIEW);
            jsi.getPerspCameraPerson().setPivot(JSIPerspCameraPerson.HOME_PIVOT);

            // remove all pt curve 2Ds
            foreach (JSIAppPolyline2D polyline in jsi.getPtCurve2DMgr().
                getPtCurve2Ds()) {

                polyline.destroyGameObject();
            }
            jsi.getPtCurve2DMgr().getPtCurve2Ds().Clear();
        }

        private void loadAppData(JSIAppData appData) {
            JSIApp jsi = (JSIApp)this.mApp;

            jsi.getPerspCameraPerson().setEye(appData.getEye());
            jsi.getPerspCameraPerson().setView(appData.getView());
            foreach (JSIStandingCard sc in appData.getStandingCards()) {
                jsi.getStandingCardMgr().getStandingCards().Add(sc);
            }
        }
    }
}