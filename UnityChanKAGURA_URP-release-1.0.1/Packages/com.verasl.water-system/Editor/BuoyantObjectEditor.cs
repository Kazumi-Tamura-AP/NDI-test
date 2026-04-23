using UnityEditor;
using System.Reflection;

namespace WaterSystem
{
    [CustomEditor(typeof(BuoyantObject))]
    public class BuoyantObjectEditor : Editor
    {
        private static readonly FieldInfo HeightsField = typeof(BuoyantObject).GetField("Heights");
        private BuoyantObject obj;
        private bool _heightsDebugBool;
        private bool _generalSettingsBool;

        private void OnEnable()
        {
            obj = serializedObject.targetObject as BuoyantObject;
        }

        public override void OnInspectorGUI()
        {
            _generalSettingsBool = EditorGUILayout.BeginFoldoutHeaderGroup(_generalSettingsBool, "General Settings");
            if (_generalSettingsBool)
            {
                base.OnInspectorGUI();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            _heightsDebugBool = EditorGUILayout.BeginFoldoutHeaderGroup(_heightsDebugBool, "Height Debug Values");
            if (_heightsDebugBool)
            {
                var heights = HeightsField?.GetValue(obj) as System.Array;
                if (heights != null)
                {
                    for (var i = 0; i < heights.Length; i++)
                    {
                        var h = heights.GetValue(i);
                        var hType = h?.GetType();
                        var x = hType?.GetField("x")?.GetValue(h);
                        var y = hType?.GetField("y")?.GetValue(h);
                        var z = hType?.GetField("z")?.GetValue(h);
                        EditorGUILayout.LabelField($"{i})Wave(heights):", $"X:{x:0.00} Y:{y:0.00} Z:{z:0.00}");
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("Height debug info only available in playmode.", MessageType.Info);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
