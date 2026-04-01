using X;
using UnityEngine;
using System;
using System.IO;

namespace JSI.Cmd {
    public class JSICmdToAutoSave : XLoggableCmd {
        // constants
        private static readonly TimeSpan ONE_MINUTE = new TimeSpan(0, 1, 0);
        private static readonly string AUTO_SAVE_DIR_NAME = "AutoSave"; // on Desktop

        // fields
        private static DateTime lastAutoSavedTime = DateTime.Now;
        private bool mShouldSaveNow = false;
        private DateTime mCurTime;
        private string mLogFilePath = string.Empty;
        private string mSketchFilePath = string.Empty;

        // private constructor
        private JSICmdToAutoSave(XApp app, bool shouldSaveNow) : base(app) {
            this.mShouldSaveNow = shouldSaveNow;
            this.mCurTime = DateTime.Now;

            // create file paths for log and sketch
            string desktopPath = Environment.GetFolderPath(Environment.
                SpecialFolder.Desktop);
            string autoSaveDirPath = Path.Combine(desktopPath, JSICmdToAutoSave.
                AUTO_SAVE_DIR_NAME);
            string dateTime = this.mCurTime.ToString("yyyy_MMdd_HHmm_ss");
            string logFileName = $"{ dateTime }_LOG.json";
            string sketchFileName = $"{ dateTime }_SKETCH.jsi3d";
            this.mLogFilePath = Path.Combine(autoSaveDirPath, logFileName);
            this.mSketchFilePath = Path.Combine(autoSaveDirPath, sketchFileName);
        }

        // static method to construct and execute this command
        public static bool execute(XApp app, bool shouldSaveNow = false) {
            JSICmdToAutoSave cmd = new JSICmdToAutoSave(app, shouldSaveNow);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            JSIApp jsi = (JSIApp)this.mApp;
            TimeSpan timeSpan = this.mCurTime - JSICmdToAutoSave.
                lastAutoSavedTime;

            if (timeSpan > JSICmdToAutoSave.ONE_MINUTE || this.mShouldSaveNow) {
                try {
                    // ensure AutoSave directory exists
                    string desktopPath = Environment.GetFolderPath(Environment.
                        SpecialFolder.Desktop);
                    string autoSaveDirPath = Path.Combine(desktopPath, JSICmdToAutoSave.
                        AUTO_SAVE_DIR_NAME);
                    if (!Directory.Exists(autoSaveDirPath)) {
                        Directory.CreateDirectory(autoSaveDirPath);
                    }

                    JSICmdToAutoSave.writeLogFile(jsi, this.mLogFilePath);
                    JSICmdToSaveFile.writeSketchFile(jsi, this.mSketchFilePath);
                    JSICmdToAutoSave.lastAutoSavedTime = this.mCurTime;
                    Debug.Log("Autosaved.");
                    return true;
                } catch {
                    Debug.LogError("Must create 'AutoSave' folder at Desktop!");
                    return false;
                }
            } else {
                return false;
            }
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("shouldSaveNow", this.mShouldSaveNow);
            // "\" is a JSON escape character, so replace it with "/"
            data.addMember("logFilePath", this.mLogFilePath.Replace('\\', '/'));
            data.addMember("sketchFilePath", this.mSketchFilePath.Replace('\\',
                '/'));
            return data;
        }

        // private methods
        public static void writeLogFile(JSIApp jsi, string filePath) {
            XJson json = new XJson();
            json.addMember("logs", jsi.getLogMgr().getLogs());
            System.IO.File.WriteAllText(filePath, json.ToString());
        }
    }
}