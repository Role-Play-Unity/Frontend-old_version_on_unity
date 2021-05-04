using UnityEditor;
using UnityEngine;

namespace LIW.Tools.Common.Build
{
    public class BuildExtentions : MonoBehaviour
    {
        static BuildSettings build;

        static string WIN_MASTER_SERVER;
        static string WIN_GAME_SERVER;
        static string WIN_GAME_CLIENT;
        enum ThisBuild { MASTER_SERVER, GAME_SERVER, GAME_CLIENT }
        ThisBuild thisBuild;

        [MenuItem("Life is Wolf/Tools/Common/Build/Start Build")]
        static void Build()
        {
            #region INIT
            build = AssetDatabase.LoadAssetAtPath<BuildSettings>("Assets/Frontend/Config/EDITOR/Build Settings.asset");
            WIN_MASTER_SERVER = GetWindowsPath($"{build.Windows.BUILD_MASTER_SERVER_PATH}",/*Path*/ build.Build.VERSION_MASTER_SERVER /*version*/);
            WIN_GAME_SERVER = GetWindowsPath($"{build.Windows.BUILD_GAME_SERVER_PATH}",/*Path*/ build.Build.VERSION_GAME_SERVER /*version*/);
            WIN_GAME_CLIENT = GetWindowsPath($"{build.Windows.BUILD_GAME_CLIENT_PATH}",/*Path*/ build.Build.VERSION_GAME_CLIENT /*version*/);

            MasterServerSetting.MasterServer masterServerSettings = new MasterServerSetting.MasterServer()
            {
                MASTER_SERVER_PATH = WIN_MASTER_SERVER,
                GAME_SERVER_PATH = WIN_GAME_SERVER,

                PUBLIC_IP = false,
                HOST = "121.0.0.1",
                PORT = 13647
            };

            #endregion
            #region MAIN BUILD SETTINGS
            PlayerSettings.companyName = build.Build.COMPANY_NAME;
            #endregion

            #region WINDOWS BUILD
            if (build.Main.WINDOWS)
            {
                CommonSetupWindows();

                #region MASTER SERVER
                if (build.Main.MASTER_SERVER)
                {
                    try
                    {
                        #region BUILD SETTINGS
                        PlayerSettings.productName = build.Build.NAME_BUILD_MASTER_SERVER;
                        PlayerSettings.bundleVersion = build.Build.VERSION_MASTER_SERVER;
                        #endregion

                        WindowsBuildArchitecture(
                                           build.Main.SCENE_MASTER_SERVER_PATH, /*scene*/
                                           WIN_MASTER_SERVER,/*Path*/
                                           $"{build.Build.NAME_BUILD_MASTER_SERVER}", /*name*/
                                           build.Build.VERSION_MASTER_SERVER, /*version*/
                                           //BuildOptions.EnableHeadlessMode, /*Console Mode*/
                                           BuildOptions.None, /*Console Mode*/
                                           ThisBuild.MASTER_SERVER/* BUILD OPTIONS */
                                           );
                    }
                    catch { }
                }
                #endregion
                #region GAME SERVER
                if (build.Main.GAME_SERVER)
                {
                    try
                    {
                        #region BUILD SETTINGS
                        PlayerSettings.productName = build.Build.NAME_BUILD_GAME_SERVER;
                        PlayerSettings.bundleVersion = build.Build.VERSION_GAME_SERVER;
                        #endregion

                        WindowsBuildArchitecture(
                                           build.Main.SCENE_GAME_SERVER_PATH, /*scene*/
                                           WIN_GAME_SERVER,/*Path*/
                                           $"{build.Build.NAME_BUILD_GAME_SERVER}", /*name*/
                                           build.Build.VERSION_GAME_SERVER, /*version*/
                                           build.Build.BuildOptions, /* BUILD OPTIONS */
                                           ThisBuild.GAME_SERVER);
                    }
                    catch { }
                }
                #endregion
                #region GAME CLIENT
                if (build.Main.GAME_CLIENT)
                {
                    try
                    {
                        #region BUILD SETTINGS
                        PlayerSettings.productName = build.Build.NAME_BUILD_GAME_CLIENT;
                        PlayerSettings.bundleVersion = build.Build.VERSION_GAME_CLIENT;
                        #endregion

                        WindowsBuildArchitecture(
                                           build.Main.SCENE_GAME_CLIENT_PATH, /*scene*/
                                           WIN_GAME_CLIENT,/*Path*/
                                           $"{build.Build.NAME_BUILD_GAME_CLIENT}", /*name*/
                                           build.Build.VERSION_GAME_CLIENT, /*version*/
                                           build.Build.BuildOptions, /* BUILD OPTIONS */
                                           ThisBuild.GAME_CLIENT
                                           );
                    }
                    catch { }
                }
                #endregion

                MasterServerSetting.Build(masterServerSettings);
            }
            #endregion
        }

        static string GetWindowsPath(string app_path, string version)
        {
            string WIN_MASTER_SERVER = $"{app_path}/{version}/{build.Windows.ARCHITECTURE}/";
            return WIN_MASTER_SERVER;
        }


        static void WindowsBuildArchitecture(string[] scene, string app_path, string app_name, string version, BuildOptions buildOptions, ThisBuild buildName)
        {
            string APP_PATH_EXE = $"{app_path}/{app_name}.exe";

            switch (buildName)
            {
                case ThisBuild.MASTER_SERVER:
                    MasterServerSetting.Setting.MASTER_SERVER_PATH = APP_PATH_EXE;
                    CommonSetupMasterServer(APP_PATH_EXE);
                    break;
                case ThisBuild.GAME_SERVER:
                    MasterServerSetting.Setting.GAME_SERVER_PATH = APP_PATH_EXE;
                    break;
            }
            switch (build.Windows.ARCHITECTURE)
            {
                case BuildSettings.WindowsConfig.ArchitectureType.All:
                    #region x86
                    app_path = $"{app_path}/x86_AND_x86_64/x86/";
                    APP_PATH_EXE = $"{app_path}/{app_name}.exe";
                    if (buildName == ThisBuild.MASTER_SERVER) CommonSetupMasterServer(APP_PATH_EXE);
                    BuildPipeline.BuildPlayer(scene, APP_PATH_EXE, BuildTarget.StandaloneWindows, buildOptions);
                    #endregion
                    #region x86_64
                    app_path = $"{app_path}/x86_AND_x86_64/x86_64/";
                    APP_PATH_EXE = $"{app_path}/{app_name}.exe";
                    if (buildName == ThisBuild.MASTER_SERVER) CommonSetupMasterServer(APP_PATH_EXE);
                    BuildPipeline.BuildPlayer(scene, APP_PATH_EXE, BuildTarget.StandaloneWindows64, buildOptions);
                    #endregion
                    break;

                case BuildSettings.WindowsConfig.ArchitectureType.x86:
                    BuildPipeline.BuildPlayer(scene, APP_PATH_EXE, BuildTarget.StandaloneWindows, buildOptions);
                    break;
                case BuildSettings.WindowsConfig.ArchitectureType.x86_64:
                    BuildPipeline.BuildPlayer(scene, APP_PATH_EXE, BuildTarget.StandaloneWindows64, buildOptions);
                    break;
            }
        }

        static void CommonSetupMasterServer(string path)
        {
            //MasterServerToolkit.MasterServer.SpawnerBehaviour.SetPathFromGameServer(path);
        }
        static void CommonSetupWindows()
        {
        }

    }
}