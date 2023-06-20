using System;
using Arden.Event;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

namespace Arden.CustomEditorTools
{
    [CustomEditor(typeof(CorruptedTextTrigger))]
    public class CorruptedWordEditorTool : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CorruptedTextTrigger textTrigger = (CorruptedTextTrigger) target;
            if (GUILayout.Button("Set Word Platform Objects"))
            {
                textTrigger.SetPlatforms();
            }
        }
    }
}
