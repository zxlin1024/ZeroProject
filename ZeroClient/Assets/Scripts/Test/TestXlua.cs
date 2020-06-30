using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[Hotfix]
public class TestXlua : MonoBehaviour
{
    LuaEnv luaEnv = new LuaEnv();
    private void Update() {
        Debug.Log("<<<<<<<<<");
    }

    private void OnGUI() {
        if(GUILayout.Button("XLua")){
            luaEnv.DoString(@"
                xlua.hotfix(CS.TestXlua, 'Update', function(self)
                    print('>>>>>>>>>>>>')
                end)"
            );
        }
    } 
}
