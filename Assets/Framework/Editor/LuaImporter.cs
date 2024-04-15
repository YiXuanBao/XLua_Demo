using System.IO;
using UnityEditor.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, ".lua")]
public class LuaImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        var luaTxt = File.ReadAllText(ctx.assetPath);
        var assetText = new TextAsset(luaTxt);
        ctx.AddObjectToAsset("main obj", assetText);
        //将对象assetText作为导入操作的主要对象。
        ctx.SetMainObject(assetText);
    }
}