//----- using ----------------------------------------------
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;
//-----
namespace LIW.Tools.Common.Scene {
    //----- Запуск создания станлартной структуры на сцене 
    public class LIWtSceneFolders{
        
        #if UNITY_EDITOR
            [MenuItem ("Life is Wolf/Tools/Common/Scene/Create Standart Folders")]
            static void CreateStdFolders(){
				
                Debug.LogFormat("Life is Wolf => Start Create STD Folders");

                //----- Править список для изменения структуры         
                string[] folders=new string[] {
					"0.Common","1.Environment","2.Network",
                    "3.Scene","4.Camera","5.Post Processing",
					"6.Characters","7.UI","10.Other","11.__Trash"
					};
                            
                foreach(string f in folders){
                    GameObject go=GameObject.Find(f);
                    if (go==null){
                        Debug.LogFormat("\t Create STD Folders ===> <color= magenta> Create {0} </color>",f);
                        go=new GameObject();
                        go.name=f;
                        if(f.Contains("__"))
                            go.AddComponent<HideOnRun>();
                    }                    
                }    
            }
        #endif
    }//-----
    
    public class HideOnRun:MonoBehaviour{
        private void Start() {
            gameObject.SetActive(false);
        }
   }
    
}//-----