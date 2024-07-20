#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ConvertMaskedDoors : Editor
{
    private static Material thinMat;
    private static bool foundThinmat; 

    [MenuItem("KOTLIN/Internal/Convert Masked Door"), Obsolete]
    public static void Convert()
    {
        foundThinmat = false; 

        foreach (DoorScript door in FindObjectsOfTypeAll(typeof(DoorScript)))
        {
            try
            {
                if (door.transform.parent.Find("Wall"))
                {
                    if (!foundThinmat)
                    {
                        thinMat = door.transform.parent.Find("Wall").GetComponent<MeshRenderer>().sharedMaterial; 
                        foundThinmat = true;
                    }

                    DestroyImmediate(door.transform.parent.Find("Wall").gameObject); //destroy the side wall 
                }

                door.transform.localPosition = new Vector3(0, 5, -5);
                door.transform.localEulerAngles = Vector3.zero; 
                door.transform.localScale = new Vector3(10, 10, 10);

                Transform inDoor = door.inside.transform;
                inDoor.transform.localPosition = new Vector3(0, 5, -5);
                inDoor.transform.localEulerAngles = new Vector3(0, 180, 0); 
                inDoor.transform.localScale = new Vector3(10, 10, 10);

                door.invisibleBarrier.transform.localPosition = inDoor.localPosition; 

            } catch
            {
                //ignore 
            }
        }

        //not the best solution but eh nobody shoulduse this tool anyways 
        foreach (MeshRenderer renderer in FindObjectsOfTypeAll(typeof(MeshRenderer)).Cast<MeshRenderer>())
        {
            if (renderer.sharedMaterial == thinMat)
            {
                DestroyImmediate(renderer.gameObject);
            }
        }
    }
}

#endif