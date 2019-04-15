//////////////////////////////////////////////////////
// MicroSplat
// Copyright (c) Jason Booth
//////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using JBooth.MicroSplat;


public partial class MicroSplatTerrainEditor : Editor 
{
   public enum BakingResolutions
   {
      k256 = 256,
      k512 = 512,
      k1024 = 1024,
      k2048 = 2048, 
      k4096 = 4096, 
      k8192 = 8192
   };

   public enum BakingPasses
   {
      Albedo = 1,
      Height = 2,
      Normal = 4,
      Metallic = 8,
      Smoothness = 16,
      AO = 32,
      Emissive = 64,
#if __MICROSPLAT_PROCTEX__
      ProceduralSplatOutput0 = 128,
      ProceduralSplatOutput1 = 256,
      ProceduralSplatOutput2 = 1024,
      ProceduralSplatOutput3 = 2048,
      ProceduralSplatOutput4 = 4096,
      ProceduralSplatOutput5 = 8192,
      ProceduralSplatOutput6 = 16384,
      ProceduralSplatOutput7 = 32768,
#endif
   };

   public BakingPasses passes = 0;
   public BakingResolutions res = BakingResolutions.k1024;

#if __MICROSPLAT_PROCTEX__
   public bool bakeSplats = false;
#endif

   bool needsBake = false;
   public void BakingGUI(MicroSplatTerrain t)
   {
      if (needsBake && Event.current.type == EventType.Repaint)
      {
         needsBake = false;
         Bake(t);
      }
#if __MICROSPLAT_PROCTEX__
      if (bakeSplats && Event.current.type == EventType.Repaint)
      {
         bakeSplats = false;
         
         int alphaLayerCount = t.terrain.terrainData.alphamapLayers;
         int splatRes = t.terrain.terrainData.alphamapResolution;
         int splatCount = t.terrain.terrainData.splatPrototypes.Length;
         float[,,] splats = new float[splatRes, splatRes, splatCount];

         for (int i = 0; i < alphaLayerCount; i=i+4)
         {
            Texture2D tex = Texture2D.blackTexture;

            if (i == 0)
            {
               tex = Bake(t, BakingPasses.ProceduralSplatOutput0, splatRes);
            }
            if (i == 4)
            {
               DestroyImmediate(tex);
               tex = Bake(t, BakingPasses.ProceduralSplatOutput1, splatRes);
            }
            else if (i == 8)
            {
               DestroyImmediate(tex);
               tex = Bake(t, BakingPasses.ProceduralSplatOutput2, splatRes);
            }
            else if (i == 12)
            {
               DestroyImmediate(tex);
               tex = Bake(t, BakingPasses.ProceduralSplatOutput3, splatRes);
            }
            else if (i == 16)
            {
               DestroyImmediate(tex);
               tex = Bake(t, BakingPasses.ProceduralSplatOutput4, splatRes);
            }
            else if (i == 20)
            {
               DestroyImmediate(tex);
               tex = Bake(t, BakingPasses.ProceduralSplatOutput5, splatRes);
            }
            else if (i == 24)
            {
               DestroyImmediate(tex);
               tex = Bake(t, BakingPasses.ProceduralSplatOutput6, splatRes);
            }
            else if (i == 28)
            {
               DestroyImmediate(tex);
               tex = Bake(t, BakingPasses.ProceduralSplatOutput7, splatRes);
            }


            for (int x = 0; x < splatRes; ++x)
            {
               for (int y = 0; y < splatRes; ++y)
               {
                  Color c = tex.GetPixel(x, y);
                  if (i < splatCount)
                  {
                     splats[x, y, i] = c.r;
                  }
                  if (i + 1 < splatCount)
                  {
                     splats[x, y, i + 1] = c.g;
                  }
                  if (i + 2 < splatCount)
                  {
                     splats[x, y, i + 2] = c.b;
                  }
                  if (i + 3 < splatCount)
                  {
                     splats[x, y, i + 3] = c.a;
                  }
               }
            }
         }
         t.terrain.terrainData.SetAlphamaps(0, 0, splats);

      }
#endif

      if (MicroSplatUtilities.DrawRollup("Render Baking", false))
      {
         res = (BakingResolutions)EditorGUILayout.EnumPopup(new GUIContent("Resolution"), res);

         #if UNITY_2017_3_OR_NEWER
            passes = (BakingPasses)EditorGUILayout.EnumFlagsField(new GUIContent("Features"), passes);
         #else
            passes = (BakingPasses)EditorGUILayout.EnumMaskPopup(new GUIContent("Features"), passes);
         #endif

         if (GUILayout.Button("Export Selected"))
         {
            needsBake = true;
         }

#if __MICROSPLAT_PROCTEX__
         if (t.templateMaterial != null && t.keywordSO != null && t.keywordSO.IsKeywordEnabled("_PROCEDURALTEXTURE"))
         {
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            if (GUILayout.Button("Bake Procedural to Terrain"))
            {
               bakeSplats = true;
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
         }
#endif

      }
   }


   bool IsEnabled(BakingPasses p)
   {
      return ((int)passes & (int)p) == (int)p;
   }
      


   static MicroSplatBaseFeatures.DefineFeature FeatureFromOutput(MicroSplatBaseFeatures.DebugOutput p)
   {
      if (p == MicroSplatBaseFeatures.DebugOutput.Albedo)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_ALBEDO;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.AO)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_AO;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.Emission)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_EMISSION;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.Height)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_HEIGHT;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.Metallic)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_METAL;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.Normal)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_NORMAL;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.Smoothness)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SMOOTHNESS;
      }
#if __MICROSPLAT_PROCTEX__
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput0)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT0;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput1)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT1;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput2)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT2;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput3)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT3;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput4)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT4;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput5)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT5;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput6)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT6;
      }
      else if (p == MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput7)
      {
         return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_SPLAT7;
      }
#endif
      return MicroSplatBaseFeatures.DefineFeature._DEBUG_OUTPUT_ALBEDO;
   }

   static MicroSplatBaseFeatures.DebugOutput OutputFromPass(BakingPasses p)
   {
      if (p == BakingPasses.Albedo)
      {
         return MicroSplatBaseFeatures.DebugOutput.Albedo;
      }
      else if (p == BakingPasses.AO)
      {
         return MicroSplatBaseFeatures.DebugOutput.AO;
      }
      else if (p == BakingPasses.Emissive)
      {
         return MicroSplatBaseFeatures.DebugOutput.Emission;
      }
      else if (p == BakingPasses.Height)
      {
         return MicroSplatBaseFeatures.DebugOutput.Height;
      }
      else if (p == BakingPasses.Metallic)
      {
         return MicroSplatBaseFeatures.DebugOutput.Metallic;
      }
      else if (p == BakingPasses.Normal)
      {
         return MicroSplatBaseFeatures.DebugOutput.Normal;
      }
      else if (p == BakingPasses.Smoothness)
      {
         return MicroSplatBaseFeatures.DebugOutput.Smoothness;
      }
#if __MICROSPLAT_PROCTEX__
      else if (p == BakingPasses.ProceduralSplatOutput0)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput0;
      }
      else if (p == BakingPasses.ProceduralSplatOutput1)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput1;
      }
      else if (p == BakingPasses.ProceduralSplatOutput2)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput2;
      }
      else if (p == BakingPasses.ProceduralSplatOutput3)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput3;
      }
      else if (p == BakingPasses.ProceduralSplatOutput4)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput4;
      }
      else if (p == BakingPasses.ProceduralSplatOutput5)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput5;
      }
      else if (p == BakingPasses.ProceduralSplatOutput6)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput6;
      }
      else if (p == BakingPasses.ProceduralSplatOutput7)
      {
         return MicroSplatBaseFeatures.DebugOutput.ProceduralSplatOutput7;
      }

#endif
      return MicroSplatBaseFeatures.DebugOutput.Albedo;
   }

   static void RemoveKeyword(List<string> keywords, string keyword)
   {
      if (keywords.Contains(keyword))
      {
         keywords.Remove(keyword);
      }
   }

   static Material SetupMaterial(MicroSplatKeywords kwds, Material mat, MicroSplatBaseFeatures.DebugOutput debugOutput)
   {
      MicroSplatShaderGUI.MicroSplatCompiler comp = new MicroSplatShaderGUI.MicroSplatCompiler();

      List<string> keywords = new List<string>(kwds.keywords);

      RemoveKeyword(keywords, "_SNOW");
      RemoveKeyword(keywords, "_TESSDISTANCE");
      RemoveKeyword(keywords, "_WINDPARTICULATE");
      RemoveKeyword(keywords, "_SNOWPARTICULATE");
      RemoveKeyword(keywords, "_GLITTER");
      RemoveKeyword(keywords, "_SNOWGLITTER");

      keywords.Add(FeatureFromOutput(debugOutput).ToString());

      string shader = comp.Compile(keywords.ToArray(), "RenderBake_" + debugOutput.ToString());
      Shader s = ShaderUtil.CreateShaderAsset(shader);
      Material renderMat = new Material(mat);
      renderMat.shader = s;
      return renderMat;
   }


   public static Texture2D Bake(MicroSplatTerrain mst, BakingPasses p, int resolution)
   {
      Camera cam = new GameObject("cam").AddComponent<Camera>();
      cam.orthographic = true;
      cam.orthographicSize = 0.5f;
      cam.transform.position = new Vector3(0.5f, 10000.5f, -1);
      cam.nearClipPlane = 0.1f;
      cam.farClipPlane = 2.0f;
      cam.enabled = false;
      cam.depthTextureMode = DepthTextureMode.None;
      cam.clearFlags = CameraClearFlags.Color;
      cam.backgroundColor = Color.grey;

      var debugOutput = OutputFromPass(p);
      var readWrite = (debugOutput == MicroSplatBaseFeatures.DebugOutput.Albedo || debugOutput == MicroSplatBaseFeatures.DebugOutput.Emission) ?
         RenderTextureReadWrite.sRGB : RenderTextureReadWrite.Linear;

      RenderTexture rt = RenderTexture.GetTemporary(resolution, resolution, 0, RenderTextureFormat.ARGB32, readWrite);
      RenderTexture.active = rt;
      cam.targetTexture = rt;

      GameObject go = GameObject.CreatePrimitive(PrimitiveType.Quad);
      go.transform.position = new Vector3(0, 10000, 0);
      cam.transform.position = new Vector3(0, 10000, -1);
      Material renderMat = SetupMaterial(MicroSplatUtilities.FindOrCreateKeywords(mst.templateMaterial), mst.matInstance, debugOutput);
      go.GetComponent<MeshRenderer>().sharedMaterial = renderMat;
      bool fog = RenderSettings.fog;
      if (p == BakingPasses.Normal)
      {
         cam.backgroundColor = Color.blue;
      }
      else
      {
         cam.backgroundColor = Color.gray;
      }
      var ambInt = RenderSettings.ambientIntensity;
      var reflectInt = RenderSettings.reflectionIntensity;
      RenderSettings.ambientIntensity = 0;
      RenderSettings.reflectionIntensity = 0;
      Unsupported.SetRenderSettingsUseFogNoDirty(false);
      cam.Render();
      Unsupported.SetRenderSettingsUseFogNoDirty(fog);

      RenderSettings.ambientIntensity = ambInt;
      RenderSettings.reflectionIntensity = reflectInt;
      Texture2D tex = new Texture2D(resolution, resolution, TextureFormat.ARGB32, false);
      tex.ReadPixels(new Rect(0, 0, resolution, resolution), 0, 0);
      RenderTexture.active = null;
      RenderTexture.ReleaseTemporary(rt);

      tex.Apply();


      MeshRenderer mr = go.GetComponent<MeshRenderer>();
      if (mr != null)
      {
         if (mr.sharedMaterial != null)
         {
            if (mr.sharedMaterial.shader != null)
               GameObject.DestroyImmediate(mr.sharedMaterial.shader);
            GameObject.DestroyImmediate(mr.sharedMaterial);
         }
      }

      GameObject.DestroyImmediate(go);
      GameObject.DestroyImmediate(cam.gameObject);
      return tex;
   }

   void Bake(MicroSplatTerrain mst)
   {
      
      // for each pass
      int pass = 1;
      while (pass <= (int)(BakingPasses.Emissive))
      {
         BakingPasses p = (BakingPasses)pass;
         pass *= 2;
         if (!IsEnabled(p))
         {
            continue;
         }
         var debugOutput = OutputFromPass(p);
         var tex = Bake(mst, p, (int)res);
         var bytes = tex.EncodeToPNG();
         
         string texPath = MicroSplatUtilities.RelativePathFromAsset(mst.terrain) + "/" + mst.terrain.name + "_" + debugOutput.ToString();
         System.IO.File.WriteAllBytes(texPath + ".png", bytes);

      }

      AssetDatabase.Refresh();
   }


}
