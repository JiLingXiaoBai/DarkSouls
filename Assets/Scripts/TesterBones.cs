using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesterBones : MonoBehaviour
{
    public SkinnedMeshRenderer srcMeshRenderer;
    public List<SkinnedMeshRenderer> tgtMeshRenderers;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var tgtMeshRenderer in tgtMeshRenderers)
        {
            tgtMeshRenderer.bones = srcMeshRenderer.bones;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}