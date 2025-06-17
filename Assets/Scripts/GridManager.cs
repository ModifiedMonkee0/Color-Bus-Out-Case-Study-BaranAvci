using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridX = 5;
    public int gridY = 5;
    public float spacing = 0.2f;
    public GameObject cellPrefab;

    [Header("Sidewalk Prefabs")]
    public GameObject straightSidePrefab;
    public GameObject cornerPrefab;

    public void GenerateGrid()
    {
        ClearChildren();

        float offset = (1 + spacing) / 2f;

        for (int x = 0; x < gridX; x++)
        {
            for (int y = 0; y < gridY; y++)
            {
                var cell = Instantiate(cellPrefab, transform);
                cell.transform.localPosition = new Vector3(
                    x * (1 + spacing) - (gridX - 1) * offset,
                    0,
                    y * (1 + spacing) - (gridY - 1) * offset
                );
                cell.name = $"Cell_{x}_{y}";
            }
        }

        GenerateSidewalk();
    }

    void GenerateSidewalk()
    {
        float offset = (1 + spacing) / 2f;
        float w = (gridX - 1) * (1 + spacing);
        float h = (gridY - 1) * (1 + spacing);

        // Üst ve Alt kenarlar
        for (int x = 0; x < gridX; x++)
        {
            float px = x * (1 + spacing) - (gridX - 1) * offset;

            // Üst
            var top = Instantiate(straightSidePrefab, transform);
            top.transform.localPosition = new Vector3(px, 0, h / 2f + (1 + spacing));
            top.transform.localRotation = Quaternion.Euler(0, 0, 0);

            // Alt
            var bottom = Instantiate(straightSidePrefab, transform);
            bottom.transform.localPosition = new Vector3(px, 0, -h / 2f - (1 + spacing));
            bottom.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        // Sol ve Sað kenarlar
        for (int y = 0; y < gridY; y++)
        {
            float py = y * (1 + spacing) - (gridY - 1) * offset;

            // Sol
            var left = Instantiate(straightSidePrefab, transform);
            left.transform.localPosition = new Vector3(-w / 2f - (1 + spacing), 0, py);
            left.transform.localRotation = Quaternion.Euler(0, 90, 0);

            // Sað
            var right = Instantiate(straightSidePrefab, transform);
            right.transform.localPosition = new Vector3(w / 2f + (1 + spacing), 0, py);
            right.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }

        // Köþe kaldýrým pozisyonlarý
        Vector3[] corners = new Vector3[]
        {
            new Vector3(-w / 2f - (1 + spacing), 0, h / 2f + (1 + spacing)),     // Sol Üst
            new Vector3(w / 2f + (1 + spacing), 0, h / 2f + (1 + spacing)),      // Sað Üst
            new Vector3(w / 2f + (1 + spacing), 0, -h / 2f - (1 + spacing)),     // Sað Alt
            new Vector3(-w / 2f - (1 + spacing), 0, -h / 2f - (1 + spacing)),    // Sol Alt
        };

        float[] rotations = new float[] { 270, 0, 90, 180 };

        for (int i = 0; i < 4; i++)
        {
            var corner = Instantiate(cornerPrefab, transform);
            corner.transform.localPosition = corners[i];
            corner.transform.localRotation = Quaternion.Euler(0, rotations[i], 0);
        }
    }

    public void ClearChildren()
    {
#if UNITY_EDITOR
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
#endif
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GridManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridManager manager = (GridManager)target;
        if (GUILayout.Button("Generate Grid"))
        {
            manager.GenerateGrid();
        }
    }
}
#endif
