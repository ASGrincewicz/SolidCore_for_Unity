using UnityEditor;
using UnityEngine;
using SolidCore.UnityIntegration.Runtime.Assets;
using SolidCore.Serialization;
using System.IO;

namespace SolidCore.UnityIntegration.Editor.Inspectors
{
    [CustomEditor(typeof(SolidAssetSO))]
    public class SolidAssetSOInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var so = (SolidAssetSO)target;

            // Re-enable GUI for imported assets
            GUI.enabled = true;

            // Re-parse definition if needed
            if (so.definition == null && so.rawBytes != null)
                so.definition = SolidAssetReader.Read(so.rawBytes);

            //Debug.Log("Inspector: definition is " + (so.definition == null ? "NULL" : "NOT NULL"));

            if (so.definition == null)
            {
                EditorGUILayout.HelpBox("No parsed asset definition found.", MessageType.Warning);
                return;
            }

            EditorGUI.BeginChangeCheck();

            // Draw fields
            foreach (var comp in so.definition.Components)
            {
                EditorGUILayout.LabelField(comp.Type.Name, EditorStyles.boldLabel);
                EditorGUI.indentLevel++;

                foreach (var field in comp.Fields)
                    DrawFieldEditor(field);

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(so);
            }

            // Save button
            if (GUILayout.Button("Save .solidasset"))
            {
                SaveAsset(so);
            }
        }


        private void DrawFieldEditor(FieldDefinition field)
        {
            switch (field.TypeCode)
            {
                case 0: // int
                    field.Value = EditorGUILayout.IntField(field.Name, (int)field.Value);
                    break;

                case 1: // float
                    field.Value = EditorGUILayout.FloatField(field.Name, (float)field.Value);
                    break;

                case 2: // bool
                    field.Value = EditorGUILayout.Toggle(field.Name, (bool)field.Value);
                    break;

                case 3: // string
                    field.Value = EditorGUILayout.TextField(field.Name, (string)field.Value);
                    break;

                case 4: // long
                    field.Value = EditorGUILayout.LongField(field.Name, (long)field.Value);
                    break;

                case 5: // double
                    field.Value = EditorGUILayout.DoubleField(field.Name, (double)field.Value);
                    break;

                default:
                    EditorGUILayout.LabelField($"{field.Name} (Unsupported type {field.TypeCode})");
                    break;
            }
        }

        private void SaveAsset(SolidAssetSO so)
        {
            string path = AssetDatabase.GetAssetPath(so);

            // Write updated binary
            byte[] bytes = SolidAssetWriter.WriteAsset(so.definition);
            File.WriteAllBytes(path, bytes);

            // Force Unity to reimport
            AssetDatabase.ImportAsset(path);

            Debug.Log($"Saved and reimported .solidasset: {path}");
        }
    }
}
