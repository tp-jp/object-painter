using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TpLab.ObjectPainter.Editor
{
    public class ObjectPainter : IDisposable
    {
        readonly ObjectPainterSetting _setting;
        readonly RaycastHit[] _hits = new RaycastHit[100];
        Vector3 _lastPaintPosition;
        MeshCollider _targetCollider;
        
        public bool IsPainting { get; private set; }
        
        public Action OnBrushSizeChanged;

        public ObjectPainter(ObjectPainterSetting setting)
        {
            _setting = setting;
            SceneView.duringSceneGui += OnSceneGUI;
        }
        
        public void Dispose()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }
        
        public void TogglePainting()
        {
            IsPainting = !IsPainting;
            if (IsPainting)
            {
                AddMeshCollider();
            }
            else
            {
                RemoveMeshCollider();
            }
        }
        
        void AddMeshCollider()
        {
            if (!_setting.targetMesh) return;

            // すでにコライダーがある場合は処理しない
            var defaultCollider = _setting.targetMesh.gameObject.GetComponent<MeshCollider>();
            if (defaultCollider && !defaultCollider.isTrigger) return;

            _targetCollider = _setting.targetMesh.gameObject.AddComponent<MeshCollider>();
            _targetCollider.convex = true;
        }

        void RemoveMeshCollider()
        {
            if (!_targetCollider) return;

            UnityEngine.Object.DestroyImmediate(_targetCollider);
            _targetCollider = null;
        }

        void OnSceneGUI(SceneView sceneView)
        {
            if (!IsPainting) return;
            if (!_setting) return;
            if (!_setting.targetMesh) return;
            if (!_setting.placeObject) return;

            // マウスの位置からレイを飛ばす
            var e = Event.current;
            var ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            
            // 衝突したオブジェクトを取得
            var hitCount = Physics.RaycastNonAlloc(ray, _hits);
            if (hitCount == 0) return;
            var hit = _hits.Take(hitCount)
                .Where(x => x.collider.gameObject == _setting.targetMesh.gameObject)
                .OfType<RaycastHit?>()
                .FirstOrDefault();
            if (!hit.HasValue) return;
            
            var brushCenter = hit.Value.point;
            var normal = hit.Value.normal;
            
            // ブラシの位置と範囲を描画
            Handles.color = new Color(0, 1, 0, 0.5f);
            Handles.DrawWireDisc(brushCenter, normal, _setting.brushSize);
            Handles.color = new Color(0, 1, 0, 0.1f);
            Handles.DrawSolidDisc(brushCenter, normal, _setting.brushSize);
            
            // 左クリック長押し中にペイント
            if (e.type == EventType.MouseDown && e.button == 0)
            {
                _lastPaintPosition = brushCenter;
                PlacementObject(brushCenter, normal);
                e.Use();
            }
            else if (e.type == EventType.MouseDrag && e.button == 0)
            {
                // クリック中のドラッグでペイント
                if (Vector3.Distance(_lastPaintPosition, brushCenter) >= _setting.brushSize)
                {
                    _lastPaintPosition = brushCenter;
                    PlacementObject(brushCenter, normal);
                }
            
                e.Use();
            }
        }
        
        /// <summary>
        /// オブジェクトを配置する。
        /// </summary>
        /// <param name="brushCenter">ブラシの中央位置</param>
        /// <param name="normal">法線</param>
        void PlacementObject(Vector3 brushCenter, Vector3 normal)
        {
            var rotationX = _setting.rotationX;
            var rotationY = _setting.rotationY;
            var rotationZ = _setting.rotationZ;
            var scale = _setting.scale;

            for (var i = 0; i < _setting.density; i++)
            {
                var randomCircle = UnityEngine.Random.insideUnitCircle * _setting.brushSize;
                var randomOffset = new Vector3(randomCircle.x, 0, randomCircle.y);
                var spawnPosition = brushCenter + randomOffset;

                // メッシュの表面に再スナップ
                if (!Physics.Raycast(spawnPosition + normal * 10, -normal, out var hit)) continue;
                if (hit.collider.gameObject != _setting.targetMesh.gameObject) continue;

                // プレハブを配置
                var instance = (GameObject)PrefabUtility.InstantiatePrefab(_setting.placeObject);
                instance.transform.position = hit.point;
                instance.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

                // ランダムな回転を適用
                var randomRotation = new Vector3(
                    rotationX.isEnabled ? rotationX.RandomValue : instance.transform.rotation.x,
                    rotationY.isEnabled ? rotationY.RandomValue : instance.transform.rotation.y,
                    rotationZ.isEnabled ? rotationZ.RandomValue : instance.transform.rotation.z
                );
                instance.transform.Rotate(randomRotation);

                // ランダムなスケールを適用
                if (scale.isEnabled)
                {
                    instance.transform.localScale *= scale.RandomValue;
                }

                if (_setting.parentObject)
                {
                    instance.transform.SetParent(_setting.parentObject, true);
                    instance.name = $"{_setting.placeObject.name}_{_setting.parentObject.childCount}";
                }

                Undo.RegisterCreatedObjectUndo(instance, "Paint Object");
            }
        }
     }
}
