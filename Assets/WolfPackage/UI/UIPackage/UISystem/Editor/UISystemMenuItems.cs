#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System;

namespace WolfUISystem
{
    public class UISystemMenuItems : MonoBehaviour
    {
        [MenuItem("GameObject/Wolf UI/Initialize Basics", false, 0)]
        private static void CreateBasecUIComponents()
        {
            UISystemInitializationPopup.ShowWindow();
        }

    }

    public class UISystemInitializationPopup : EditorWindow
    {
        private static string[] initialScreenOptions = new string[] {
            "IntroScreen",
            "HudScreen",
            "TransitionScreen",
            "CreditScreen",
            "MainMenuScreen",
            "SettingScreen"
        };

        bool IntroScreen;
        bool HudScreen;
        bool TransitionScreen;
        bool CreditScreen;
        bool MainMenuScreen;
        bool SettingScreen;

        string folderName = "UI System";

        public UnityEngine.Object screenBasePrefab;
        public static void ShowWindow()
        {
            GetWindow<UISystemInitializationPopup>();
        }

        void InitializeUIComponents()
        {
            UIManager uiroot = GetUIManagerInTheScene();
            string scriptFolderPath = CreateScriptFolder();
            bool addedNewScript = false;
            if(IntroScreen)
            {
                GameObject go = CreateScreenComponent("Intro Screen", uiroot.gameObject);
                addedNewScript = CreateScreenScript(scriptFolderPath, "IntroScreen");
                //AssetDatabase.Refresh();
                //Type t = Type.GetType("IntroScreen", true);
                //go.AddComponent(t);
            }

            if(HudScreen)
            {
                CreateScreenComponent("Hud Screen", uiroot.gameObject);
                addedNewScript = CreateScreenScript(scriptFolderPath, "HudScreen");
            }

            if(TransitionScreen)
            {
                CreateScreenComponent("Transition Screen", uiroot.gameObject);
                addedNewScript = CreateScreenScript(scriptFolderPath, "TransitionScreen");
            }

            if(CreditScreen)
            {
                CreateScreenComponent("Credit Screen", uiroot.gameObject);
                addedNewScript = CreateScreenScript(scriptFolderPath, "CreditScreen");
            }
            if(MainMenuScreen)
            {
                CreateScreenComponent("Main Menu Screen", uiroot.gameObject);
                addedNewScript = CreateScreenScript(scriptFolderPath, "MainMenuScreen");
            }

            if(SettingScreen)
            {
                CreateScreenComponent("Setting Screen", uiroot.gameObject);
                addedNewScript = CreateScreenScript(scriptFolderPath, "SettingScreen");
            }
            if(addedNewScript)
            {
                AssetDatabase.Refresh();
            }
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

            // Local Functions
            UIManager GetUIManagerInTheScene()
            {
                UIManager uiroot = FindObjectOfType<UIManager>(true);
                if(!uiroot)
                {
                    GameObject uirootGO = new GameObject("Wolf UI System");
                    uirootGO.transform.position = Vector3.zero;
                    uiroot = uirootGO.AddComponent<UIManager>();
                }
                return uiroot;
            }

            string CreateScriptFolder()
            {
                string uiRootFolder = Path.Combine("Assets", folderName);
                if(!AssetDatabase.IsValidFolder(uiRootFolder))
                {
                    AssetDatabase.CreateFolder("Assets", folderName);
                }

                string scriptFolder = Path.Combine(uiRootFolder, "Scripts");
                if(!AssetDatabase.IsValidFolder(scriptFolder))
                {
                    AssetDatabase.CreateFolder(uiRootFolder, "Scripts");
                }
                return scriptFolder;
            }

            string CreatePrefabsFolder()
            {
                string uiRootFolder = Path.Combine("Assets", folderName);
                if(!AssetDatabase.IsValidFolder(uiRootFolder))
                {
                    AssetDatabase.CreateFolder("Assets", folderName);
                }

                string prefabsFolder = Path.Combine(uiRootFolder, "Prefabs");
                if(!AssetDatabase.IsValidFolder(prefabsFolder))
                {
                    AssetDatabase.CreateFolder(uiRootFolder, "Prefabs");
                }
                return prefabsFolder;
            }

            static bool CreateScreenScript(string scriptFolderPath, string className)
            {
                string fileName = className + ".cs";
                if(!File.Exists(Path.Combine(scriptFolderPath, fileName)))
                {
                    string template = WolfyUIScriptTemplate.ScriptFileTemplate.Replace("{0}", className);
                    File.WriteAllText($"{scriptFolderPath}/{fileName}", template);
                    return true;
                }
                return false;

            }

            GameObject CreateScreenComponent(string screenName, GameObject root)
            {
                GameObject screenGO;
                if(screenBasePrefab)
                {
                    GameObject tmp = (GameObject) PrefabUtility.InstantiatePrefab(screenBasePrefab);
                    GameObject tmpPre = PrefabUtility.SaveAsPrefabAsset(tmp, Path.Combine(CreatePrefabsFolder(), screenName + ".prefab"));
                    DestroyImmediate(tmp);
                    screenGO = (GameObject) PrefabUtility.InstantiatePrefab(tmpPre);
                }
                else
                {
                    screenGO = new GameObject();
                }
                screenGO.name = screenName;
                screenGO.transform.parent = root.transform;
                return screenGO;
            }
        }



        private void OnGUI()
        {

            EditorGUILayout.LabelField("Initialize UI System", EditorStyles.boldLabel);
            // screen options
            IntroScreen = EditorGUILayout.Toggle("Intro Screen", IntroScreen);
            HudScreen = EditorGUILayout.Toggle("Hud Screen", HudScreen);
            TransitionScreen = EditorGUILayout.Toggle("Transition Screen", TransitionScreen);
            CreditScreen = EditorGUILayout.Toggle("Credit Screen", CreditScreen);
            MainMenuScreen = EditorGUILayout.Toggle("MainMenu Screen", MainMenuScreen);
            SettingScreen = EditorGUILayout.Toggle("Setting Screen", SettingScreen);
            EditorGUILayout.LabelField("Screen base prefab");
            screenBasePrefab = EditorGUILayout.ObjectField(screenBasePrefab, typeof(GameObject), true);
            // target folder
            folderName = EditorGUILayout.TextField("UI Root Folder: ", folderName);
            this.Repaint();
            if(GUILayout.Button("Conform"))
            {
                InitializeUIComponents();
            }

            if(GUILayout.Button("Close"))
            {
                Close();
            }
        }
    }

    internal static class WolfyUIScriptTemplate
    {
        internal static string ScriptFileTemplate = @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class {0} : ScreenBase
{
    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }
}
";
    }
}
#endif