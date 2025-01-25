using UnityEditor;
using UnityEngine;

namespace TpLab.ObjectPainter.Editor
{
    public class ObjectPainterWindow : EditorWindow
    {
        ObjectPainterSetting _setting;
        ObjectPainter _objectPainter;
        Vector2 _scrollPosition;

        [MenuItem("TpLab/ObjectPainter")]
        public static void ShowWindow()
        {
            var window = GetWindow<ObjectPainterWindow>("ObjectPainter");
            window.Initialize();
            window.minSize = new Vector2(400f, 330f);
            window.Show();
        }

        void OnDisable()
        {
            SettingRepository.Instance.SaveSetting(_setting);
            _objectPainter.Dispose();
        }

        void Initialize()
        {
            _setting ??= SettingRepository.Instance.LoadSetting();
            _objectPainter ??= new ObjectPainter(_setting);
        }
        
        void OnGUI()
        {
            Initialize();

            using var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPosition);
            _scrollPosition = scrollView.scrollPosition;
            
            GUILayout.Label("Settings", EditorStyles.boldLabel);
            using (new EditorGUI.DisabledScope(_objectPainter.IsPainting))
            {
                _setting.targetMesh = EditorGUILayout.ObjectField("Target Mesh", _setting.targetMesh, typeof(MeshFilter), true) as MeshFilter;
            }
            _setting.placeObject = EditorGUILayout.ObjectField("Place Object", _setting.placeObject, typeof(GameObject), false) as GameObject;
            _setting.parentObject = EditorGUILayout.ObjectField("Parent Object", _setting.parentObject, typeof(Transform), true) as Transform;
            _setting.brushSize = EditorGUILayout.Slider("Brush Size", _setting.brushSize, 0.1f, _setting.brushSizeLimit);
            _setting.density = EditorGUILayout.IntSlider("Density", _setting.density, 1, _setting.densityLimit);
            EditorGUILayout.Space();

            _setting.showPlacementSettings = EditorGUILayout.Foldout(_setting.showPlacementSettings, "Placement Settings");
            if (_setting.showPlacementSettings)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    RangeSlider("Rotation Range X", ref _setting.rotationX);
                    RangeSlider("Rotation Range Y", ref _setting.rotationY);
                    RangeSlider("Rotation Range Z", ref _setting.rotationZ);
                    RangeSlider("Scale Range", ref _setting.scale);
                }
            }
            EditorGUILayout.Space();
            
            _setting.showLimitSettings = EditorGUILayout.Foldout(_setting.showLimitSettings, "Limit Settings");
            if (_setting.showLimitSettings)
            {
                using (new EditorGUI.IndentLevelScope())
                {
                    _setting.brushSizeLimit = EditorGUILayout.FloatField("Brush Size Limit", _setting.brushSizeLimit);
                    _setting.densityLimit = EditorGUILayout.IntField("Density Limit", _setting.densityLimit);
                    _setting.rotationX.minLimit = EditorGUILayout.FloatField("Rotation Min Limit X", _setting.rotationX.minLimit);
                    _setting.rotationX.maxLimit = EditorGUILayout.FloatField("Rotation Max Limit X", _setting.rotationX.maxLimit);
                    _setting.rotationY.minLimit = EditorGUILayout.FloatField("Rotation Min Limit Y", _setting.rotationY.minLimit);
                    _setting.rotationY.maxLimit = EditorGUILayout.FloatField("Rotation Max Limit Y", _setting.rotationY.maxLimit);
                    _setting.rotationZ.minLimit = EditorGUILayout.FloatField("Rotation Min Limit Z", _setting.rotationZ.minLimit);
                    _setting.rotationZ.maxLimit = EditorGUILayout.FloatField("Rotation Max Limit Z", _setting.rotationZ.maxLimit);
                    _setting.scale.minLimit = EditorGUILayout.FloatField("Scale Min Limit", _setting.scale.minLimit);
                    _setting.scale.maxLimit = EditorGUILayout.FloatField("Scale Max Limit", _setting.scale.maxLimit);
                }
            }
            EditorGUILayout.Space();

            using (new EditorGUI.DisabledScope(DrawWarningMessage()))
            {
                var backgroundColor = GUI.backgroundColor;
                if (_objectPainter.IsPainting) GUI.backgroundColor = Color.green;
                if (GUILayout.Button(_objectPainter.IsPainting ? "Stop Painting" : "Start Painting"))
                {
                    _objectPainter.TogglePainting();
                }
                GUI.backgroundColor = backgroundColor;
            }
        }

        /// <summary>
        /// 範囲スライダーを描画する。
        /// </summary>
        /// <param name="label">ラベル</param>
        /// <param name="range">ランダム範囲情報</param>
        void RangeSlider(string label, ref RandomizableRange range)
        {
            const float floatFieldWidth = 50f;
            const float marginSize = 2f;
            const float sliderMarginSize = 3f;

            // Get the control rect to handle the layout properly
            var controlRect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
            var labelWidth = 150f - EditorGUI.indentLevel * 15f;
            var sliderWidth = Mathf.Max(controlRect.width - labelWidth - (floatFieldWidth * 2) - (marginSize * 3) - sliderMarginSize, 0f);

            // Calculate positions for each component (Label, Min Field, Slider, Max Field)
            var labelRect = new Rect(controlRect.x, controlRect.y, labelWidth, controlRect.height);
            var minFieldRect = new Rect(labelRect.xMax + marginSize, controlRect.y, floatFieldWidth, controlRect.height);
            var sliderRect = new Rect(minFieldRect.xMax + marginSize, controlRect.y, sliderWidth, controlRect.height);
            var maxFieldRect = new Rect(sliderRect.xMax + marginSize + sliderMarginSize, controlRect.y, floatFieldWidth, controlRect.height);

            // Draw the label and the rest of the controls
            range.isEnabled = EditorGUI.ToggleLeft(labelRect, label, range.isEnabled);
            using (new EditorGUI.DisabledScope(!range.isEnabled))
            {
                range.minValue = EditorGUI.FloatField(minFieldRect, range.minValue);
                EditorGUI.MinMaxSlider(sliderRect, ref range.minValue, ref range.maxValue, range.minLimit, range.maxLimit);
                range.maxValue = EditorGUI.FloatField(maxFieldRect, range.maxValue);
            }
        }
        
        /// <summary>
        /// 警告メッセージを表示する。
        /// </summary>
        /// <returns>警告がある場合はtrue、それ以外はfalse</returns>
        bool DrawWarningMessage()
        {
            if (!_setting.targetMesh)
            {
                EditorGUILayout.HelpBox("Target mesh is not set.", MessageType.Warning);
                return true;
            }
            if (!_setting.placeObject)
            {
                EditorGUILayout.HelpBox("Placement object is not set.", MessageType.Warning);
                return true;
            }
            return false;
        }
    }
}