using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.ShortcutManagement;
using Project.Level;
using Project.Meta;

namespace Project.Level.Editor
{
    [EditorTool(displayName: "Transform level grid", componentToolTarget: typeof(LevelGrid))]
    public class LevelGridTool : EditorTool, IDrawSelectedHandles
    {
        private const float RESIZE_HANDLES_SIZE = .8f;
        private readonly Color _gridColor = new Color32(200, 200, 200, 150);
        private readonly Color _resizeHandlesVertical = Color.blue;
        private readonly Color _resizeHandlesHorizontal = Color.red;

        public override void OnToolGUI(EditorWindow window)
        {
            foreach (var obj in targets)
                if (obj is LevelGrid grid)
                {
                    EditorGUI.BeginChangeCheck();

                    Vector2 direction = DrawResizeHandles(grid, out float input);

                    if (EditorGUI.EndChangeCheck())
                    {
                        ProcessResizeInput(grid, direction, input);
                    }
                }
        }

        private Vector2 DrawResizeHandles(LevelGrid grid, out float delta)
        {
            Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
            Color[] axesColors = { _resizeHandlesVertical, _resizeHandlesHorizontal };
            Vector2 halfSize = grid.Size.ToFloatVec2() / 2;

            Vector2 dir;
            for (int i = 0; i < directions.Length; i++)
            {
                dir = directions[i];

                Handles.color = axesColors[RepeatInt(i, axesColors.Length)];
                var input = DrawScaleHandle(grid.transform.position, halfSize, dir, RESIZE_HANDLES_SIZE);

                if (input != 0)
                {
                    delta = input;
                    return dir;
                }
            }

            delta = 0;
            return Vector2.zero;
        }
        private int RepeatInt(int value, int length) => (int)Mathf.Repeat(value, length);

        private float DrawScaleHandle(Vector3 gridCenter, Vector2 gridExtends, Vector2 direction, float handlesSize = 1)
        {
            float localSize = Dot(gridExtends, direction);
            var pos = gridCenter + (direction * localSize).XYtoXoY();

            //localSize = Mathf.Sign(localSize);
            return (Handles.ScaleSlider(localSize, pos, direction.XYtoXoY(), Quaternion.identity, handlesSize, 1) - localSize);// * handlesSize;
        }
        private float Dot(Vector2 origin, Vector2 direction) => Mathf.Abs(Vector2.Dot(origin, direction));

        private void ProcessResizeInput(ILevelGridInternal grid, Vector2 direction, float input)
        {
            if (input == 0) return;
            if (Mathf.Abs(input) < 1) return;

            Undo.RecordObject(grid as LevelGrid, "Level Grid Transformation");
            var inputInt = Mathf.RoundToInt(input);
            grid.Expand(direction, inputInt);
        }

        void IDrawSelectedHandles.OnDrawHandles()
        {
            foreach (var obj in targets)
                if (obj is LevelGrid grid) DrawGrid(grid, _gridColor, (from, to) => Handles.DrawLine(from, to));
        }
        private void DrawGrid(ILevelGridInternal grid, Color color, System.Action<Vector3, Vector3> drawLineMethod)
        {
            int width = grid.Size.x, height = grid.Size.y;

            Handles.color = color;
            for (int x = 0; x <= width; x++) DrawVerticalLine(x);
            for (int y = 0; y <= height; y++) DrawHorizontalLine(y);


            void DrawVerticalLine(int x)
            {
                Vector3 from = grid.GetGridBottomCorner() + new Vector3(x, 0, 0);
                Vector3 to = from + new Vector3(0, 0, height);

                drawLineMethod.Invoke(from, to);
            }

            void DrawHorizontalLine(int y)
            {
                Vector3 from = grid.GetGridBottomCorner() + new Vector3(0, 0, y);
                Vector3 to = from + new Vector3(width, 0, 0);

                drawLineMethod.Invoke(from, to);
            }
        }
    }
}