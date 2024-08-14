using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[RequireComponent(typeof(NavMeshSurface))]
public class AutomaticNavAndOcclusion : Singleton<AutomaticNavAndOcclusion>
{
    [SerializeField] private GameObject AiParent;
    [SerializeField] private bool BakeOnStart;

    private void Start()
    {
        if (BakeOnStart)
        {
            Generate(true, true); 
        }
    }
    public void Generate(bool occlusion, bool navmesh)
    {
        if (navmesh)
        {
            NavMeshSurface navMeshSurface = GetComponent<NavMeshSurface>();
            if (navMeshSurface != null)
            {
                bool disableAI = !AiParent.activeSelf;
                AiParent.SetActive(true);

                navMeshSurface.BuildNavMesh();
                if (disableAI)
                    AiParent.SetActive(false);
            }
            else
            {
                Debug.LogError("navMeshSurface component not found");
            }
        }

        #if UNITY_EDITOR
        if (occlusion)
        {
            StaticOcclusionCulling.Compute();
        }
        #endif
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(AutomaticNavAndOcclusion))]
public class AutomaticNavAndOcclusionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AutomaticNavAndOcclusion automaticNavAndOcclusion = (AutomaticNavAndOcclusion)target;

        if (GUILayout.Button("Bake Nav Mesh"))
        {
            automaticNavAndOcclusion.Generate(navmesh: true, occlusion: false);
        }

        if (GUILayout.Button("Bake Occlusion"))
        {
            automaticNavAndOcclusion.Generate(navmesh: false, occlusion: true);
        }

        if (GUILayout.Button("Bake BOTH!!!"))
        {
            automaticNavAndOcclusion.Generate(navmesh: true, occlusion: true);
        }
    }
}
#endif 