using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(CI_caller), true)]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        // Take out this if statement to set the value using setter when ever you change it in the inspector.
        // But then it gets called a couple of times when ever inspector updates
        // By having a button, you can control when the value goes through the setter and getter, your self.
        if (GUILayout.Button("Update") && EditorApplication.isPlayingOrWillChangePlaymode)
        {
            CI_caller caller = (CI_caller)target;
            caller.updateVarFromCI();
        }
    }
}