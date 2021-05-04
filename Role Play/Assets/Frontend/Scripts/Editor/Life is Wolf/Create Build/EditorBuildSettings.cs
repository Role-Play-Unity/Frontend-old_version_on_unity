using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace LIW.Tools.Common.Build
{
    [CustomEditor(typeof(BuildSettings))]
    public class EditorBuildSettings : Editor
    {

        //AttackType type;
        BuildSettings MainBuildSettings;

        private void OnEnable()
        {
            MainBuildSettings = (BuildSettings)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space(10);

            EditorGUILayout.TextArea("BUILD PLATFORM SETTINGS");
            MainBuildSettings.Main.WINDOWS = EditorGUILayout.Toggle("BUILD FOR Windows platform?", MainBuildSettings.Main.WINDOWS);
            MainBuildSettings.Main.LINUX = EditorGUILayout.Toggle("BUILD FOR Linux platform?", MainBuildSettings.Main.LINUX);
            EditorGUILayout.Space(10);

            EditorGUILayout.TextArea("BUILD CONFIG");
            MainBuildSettings.Build.BuildOptions = (BuildOptions)EditorGUILayout.EnumPopup("BUILD OPTIONS", MainBuildSettings.Build.BuildOptions);
            EditorGUILayout.TextArea("COMPANY NAME");
            MainBuildSettings.Build.COMPANY_NAME = EditorGUILayout.TextField("COMPANY NAME", MainBuildSettings.Build.COMPANY_NAME);
            EditorGUILayout.Space(5);
            EditorGUILayout.TextArea("BUILD VERSION");
            MainBuildSettings.Build.VERSION_MASTER_SERVER = EditorGUILayout.TextField("VERSION MASTER SERVER", MainBuildSettings.Build.VERSION_MASTER_SERVER);
            MainBuildSettings.Build.VERSION_GAME_SERVER = EditorGUILayout.TextField("VERSION GAME SERVER", MainBuildSettings.Build.VERSION_GAME_SERVER);
            MainBuildSettings.Build.VERSION_GAME_CLIENT = EditorGUILayout.TextField("VERSION GAME CLIENT", MainBuildSettings.Build.VERSION_GAME_CLIENT);
            EditorGUILayout.Space(5);
            EditorGUILayout.TextArea("APPLICATION NAME");
            MainBuildSettings.Build.NAME_BUILD_MASTER_SERVER = EditorGUILayout.TextField("MASTER SERVER NAME", MainBuildSettings.Build.NAME_BUILD_MASTER_SERVER);
            MainBuildSettings.Build.NAME_BUILD_GAME_SERVER = EditorGUILayout.TextField("GAME SERVER NAME", MainBuildSettings.Build.NAME_BUILD_GAME_SERVER);
            MainBuildSettings.Build.NAME_BUILD_GAME_CLIENT = EditorGUILayout.TextField("GAME CLIENT NAME", MainBuildSettings.Build.NAME_BUILD_GAME_CLIENT);
            EditorGUILayout.Space(10);

            EditorGUILayout.TextArea("BUILD SCENE");
            MainBuildSettings.Main.MASTER_SERVER = EditorGUILayout.Toggle("BUILD MASTER SERVER?", MainBuildSettings.Main.MASTER_SERVER);
            MainBuildSettings.Main.GAME_SERVER = EditorGUILayout.Toggle("BUILD GAME SERVER?", MainBuildSettings.Main.GAME_SERVER);
            MainBuildSettings.Main.GAME_CLIENT = EditorGUILayout.Toggle("BUILD GAME CLIENT?", MainBuildSettings.Main.GAME_CLIENT);
            EditorGUILayout.Space(10);

            EditorGUILayout.TextArea("BUILD SCENE PATH");

            MainBuildSettings.Main.SCENE_MASTER_SERVER_PATH = EditorGUILayoutArrays.StringArrayField(
                new EditorGUILayoutArrays.ArrayFieldSettings() { label = "PATH TO MASTER SERVER", open = true },
                MainBuildSettings.Main.SCENE_MASTER_SERVER_PATH);

            MainBuildSettings.Main.SCENE_GAME_SERVER_PATH = EditorGUILayoutArrays.StringArrayField(
                new EditorGUILayoutArrays.ArrayFieldSettings() { label = "PATH TO GAME SERVER", open = true },
                MainBuildSettings.Main.SCENE_GAME_SERVER_PATH);

            MainBuildSettings.Main.SCENE_GAME_CLIENT_PATH = EditorGUILayoutArrays.StringArrayField(
                new EditorGUILayoutArrays.ArrayFieldSettings() { label = "PATH TO GAME CLIENT", open = true },
                MainBuildSettings.Main.SCENE_GAME_CLIENT_PATH);

            EditorGUILayout.Space(10);

            EditorGUILayout.TextArea("PLATFORM SETTINGS");
            if (MainBuildSettings.Main.WINDOWS)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.TextArea("Windows Settings");
                MainBuildSettings.Windows.ARCHITECTURE = (BuildSettings.WindowsConfig.ArchitectureType)EditorGUILayout.EnumPopup("ARCHITECTURE:", MainBuildSettings.Windows.ARCHITECTURE);
                MainBuildSettings.Windows.BUILD_MASTER_SERVER_PATH = EditorGUILayout.TextField("PATH TO THE MASTER SERVER BUILD", MainBuildSettings.Windows.BUILD_MASTER_SERVER_PATH);
                MainBuildSettings.Windows.BUILD_GAME_SERVER_PATH = EditorGUILayout.TextField("PATH TO THE GAME SERVER BUILD", MainBuildSettings.Windows.BUILD_GAME_SERVER_PATH);
                MainBuildSettings.Windows.BUILD_GAME_CLIENT_PATH = EditorGUILayout.TextField("PATH TO THE GAME CLIENT BUILD", MainBuildSettings.Windows.BUILD_GAME_CLIENT_PATH);
            }
            if (MainBuildSettings.Main.LINUX)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.TextArea("Linux Settings");
                MainBuildSettings.Linux.BUILD_MASTER_SERVER_PATH = EditorGUILayout.TextField("PATH TO THE MASTER SERVER BUILD", MainBuildSettings.Linux.BUILD_MASTER_SERVER_PATH);
                MainBuildSettings.Linux.BUILD_GAME_SERVER_PATH = EditorGUILayout.TextField("PATH TO THE GAME SERVER BUILD", MainBuildSettings.Linux.BUILD_GAME_SERVER_PATH);
                MainBuildSettings.Linux.BUILD_GAME_CLIENT_PATH = EditorGUILayout.TextField("PATH TO THE GAME CLIENT BUILD", MainBuildSettings.Linux.BUILD_GAME_CLIENT_PATH);
            }
            EditorGUILayout.Space(10);
            EditorGUILayout.EndVertical();
        }

    }
}