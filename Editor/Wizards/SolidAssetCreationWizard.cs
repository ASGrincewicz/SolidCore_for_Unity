using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using SolidCore.Serialization;
using SolidCore.Collections;
using SolidCore.ECS;

namespace SolidCore.UnityIntegration.Editor.Wizards
{
    public class SolidAssetCreationWizard : EditorWindow
    {
        private Type selectedComponentType;
        private string savePath = "Assets/NewComponent.solidasset";

        [MenuItem("SolidCore/Create Component Asset")]
        public static void ShowWindow()
        {
            GetWindow<SolidAssetCreationWizard>("SolidCore Component Wizard");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("SolidCore Component Asset Wizard", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            DrawComponentTypeSelector();
            DrawSavePathField();

            EditorGUILayout.Space();

            if (selectedComponentType != null)
            {
                EditorGUILayout.LabelField("Fields Detected:", EditorStyles.boldLabel);
                DrawDetectedFields();
            }

            EditorGUILayout.Space();

            GUI.enabled = selectedComponentType != null;

            if (GUILayout.Button("Create .solidasset"))
            {
                CreateSolidAsset();
            }

            GUI.enabled = true;
        }

        private void DrawComponentTypeSelector()
        {
            EditorGUILayout.LabelField("Component Type", EditorStyles.boldLabel);

            var componentTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsValueType && typeof(IComponentData).IsAssignableFrom(t))
                .ToList();

            string[] names = componentTypes.Select(t => t.Name).ToArray();
            int index = selectedComponentType == null
                ? -1
                : componentTypes.IndexOf(selectedComponentType);

            int newIndex = EditorGUILayout.Popup("Choose Component", index, names);

            if (newIndex >= 0)
                selectedComponentType = componentTypes[newIndex];
        }

        private void DrawSavePathField()
        {
            EditorGUILayout.LabelField("Save Location", EditorStyles.boldLabel);
            savePath = EditorGUILayout.TextField("Path", savePath);
        }

        private void DrawDetectedFields()
        {
            var fields = selectedComponentType.GetFields();

            foreach (var f in fields)
            {
                EditorGUILayout.LabelField($"{f.Name} : {f.FieldType.Name}");
            }
        }

        private void CreateSolidAsset()
        {
            var def = new AssetDefinition();
            var comp = new ComponentDefinition
            {
                Type = selectedComponentType,
                TypeId = selectedComponentType.GetHashCode(), // temporary ID
                Fields = new FastList<FieldDefinition>()
            };

            foreach (var f in selectedComponentType.GetFields())
            {
                byte typeCode = GetTypeCode(f.FieldType);
                object defaultValue = GetDefaultValue(f.FieldType);

                comp.Fields.Add(new FieldDefinition(f.Name, typeCode, defaultValue));
            }

            def.Components.Add(comp);

            byte[] bytes = SolidAssetWriter.WriteAsset(def);

            File.WriteAllBytes(savePath, bytes);
            AssetDatabase.Refresh();
            Debug.Log("Wizard: Wrote " + bytes.Length + " bytes to " + savePath);

            Debug.Log($"Created SolidAsset at {savePath}");
        }

        private byte GetTypeCode(Type t)
        {
            if (t == typeof(int)) return 0;
            if (t == typeof(float)) return 1;
            if (t == typeof(bool)) return 2;
            if (t == typeof(string)) return 3;
            if (t == typeof(long)) return 4;
            if (t == typeof(double)) return 5;

            throw new Exception($"Unsupported field type: {t}");
        }

        private object GetDefaultValue(Type t)
        {
            if (t == typeof(int)) return 0;
            if (t == typeof(float)) return 0f;
            if (t == typeof(bool)) return false;
            if (t == typeof(string)) return "";
            if (t == typeof(long)) return 0L;
            if (t == typeof(double)) return 0.0;

            throw new Exception($"Unsupported field type: {t}");
        }
    }
}
