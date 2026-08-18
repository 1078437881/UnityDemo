using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class Script_03_04
        // :UnityEditor.AssetModificationProcessor
    {
        //监听打开资源事件
        public static bool IsOpenForEdit(string[] assetOrMetaFilePaths, List<string> outNotEditablePaths,
            StatusQueryOptions statusQueryOptions)
        {
            List<string> assetOnlyPaths = new List<string>();
            foreach (string path in assetOrMetaFilePaths)
            {
                // 过滤meta文件
                if (!path.EndsWith(".meta"))
                {
                    assetOnlyPaths.Add(path);
                }
            }
            // 打印纯资源路径
            string pathsLog = string.Join(" | ", assetOnlyPaths);
            Debug.Log($"打开资源路径：{pathsLog}");

            return true;
        }
        
        //监听资源将被创建事件
        public static void OnWillCreateAsset(string assetName)
        {
            Debug.LogFormat("WillCreateAsset : {0}", assetName);
        }

        //监听资源将被保存事件
        public static string[] OnWillSaveAssets(string[] paths)
        {
            if (paths != null)
            {
                Debug.LogFormat("WillSaveAssets : {0}", string.Join("",paths));
            }

            return paths;
        }
        
        //监听资源将被移动事件
        public static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
        {
            Debug.LogFormat("MoveAsset from : {0} to : {1}", sourcePath,destinationPath);
            //AssetMoveResult.DidMove表示该资源可以移动
            return AssetMoveResult.DidMove;
        }
        
        //监听资源将被删除事件
        public static AssetDeleteResult OnWillDeleteAsset(string assetPath, RemoveAssetOptions options)
        {
            Debug.LogFormat("DeleteAsset : {0}", assetPath);
            //AssetDeleteResult.DidDelete表示资源可以被删除
            return AssetDeleteResult.DidDelete;
        }
    }
}