using UnityEditor;
using UnityEngine;
using System.IO;

namespace GameFlow {

[InitializeOnLoad]	
class Setup {
	
static Setup() {
	// start the install automatically once the initial import is done
	AssetDatabase.importPackageCompleted += AutoInstall;
}

static void AutoInstall(string packageName) {
	AssetDatabase.importPackageCompleted -= AutoInstall;
	// cancel install if a different package was imported
	if (!packageName.StartsWith("GameFlow")) {
		return;
	}
	CopyDLLs();
}

[MenuItem("Tools/GameFlow/Reinstall", false, 3200)]
static void Reinstall() {
	CopyDLLs();
}

// ------------------------------------------------------------------------------------------------

static string uv;

static bool SupportedUnityVersion() {
	string[] parts = Application.unityVersion.Split("."[0]);
	uv = parts[0] + "." + parts[1];
	int uvInt = int.Parse(parts[0]) * 100 + int.Parse(parts[1]);
	bool supported = uvInt >= 2018 * 100 + 4;
	if (!supported) {
		Info($"The minimum supported Unity version is 2018.4.0.\n\n" + 
		  "Please upgrade your Unity and reimport the GameFlow package.\n");
	}
	return supported;
}

static void CopyDLLs() {
	if (!SupportedUnityVersion() || uv.Equals("2018.4")) {
		return;
	}
	// locate the GameFlow/Install/Versions folder
	string path = AssetDatabase.GUIDToAssetPath("a239dc7256cfb467c82cfa28712fc103");
	string cd = Directory.GetCurrentDirectory();
	bool exists = path != "" && Directory.Exists($"{cd}/{path}");
	if (!exists) {
		Info($"ERROR: Unable to locate the GameFlow versions folder. " + 
		  "Please reimport the GameFlow package.\n");
		return;
	}
	// check source files
	string ext = ".bak";
	string src1 = $"{path}/{uv}/GameFlow{ext}";
	string src2 = $"{path}/{uv}/GameFlowEditor{ext}";
 	if (!File.Exists($"{cd}/{src1}") || !File.Exists($"{cd}/{src2}")) {
		Info($"ERROR: Unable to locate the GameFlow {ext} files for Unity {uv}. " + 
		  "Please reimport the GameFlow package.\n");
		return;
	}
	// locate GameFlow.dll and GameFlowEditor.dll
	string dst1 = AssetDatabase.GUIDToAssetPath("20990ec6b45e544b0b5f28b1e0c1c96b");
	string dst2 = AssetDatabase.GUIDToAssetPath("72b2acbe68e9a4cd4bb8e898cd9a2514");
 	if (dst1 == "" || !File.Exists($"{cd}/{dst1}") || dst2 == "" || !File.Exists($"{cd}/{dst2}")) {
		Info($"ERROR: Unable to reinstall the GameFlow package. " + 
		  "Please reimport the GameFlow package.\n");
		return;
	}
	// overwrite with the versioned .dll files
	File.Copy(src1, dst1, true);
 	File.Copy(src2, dst2, true);
	AssetDatabase.Refresh();
	Info($"GameFlow for Unity {uv} successfully imported.");
}

// ------------------------------------------------------------------------------------------------

static string title = "GameFlow Installer";

static void Info(string message) {
	EditorUtility.DisplayDialog(title, message, "Ok");
}

static bool Dialog(string message) {
	return EditorUtility.DisplayDialog(title, message, "Yes", "No");
}

}

}
