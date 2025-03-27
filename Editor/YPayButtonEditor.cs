// Yandex Pay InApps Plugin.

using UnityEditor;
using UnityEngine;

namespace YPay.Editor
{
    [CustomEditor(typeof(YPayButton))]
    public class YPayButtonEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var button = (YPayButton)target;

            button.MerchantId = EditorGUILayout.TextField("Merchant Id", button.MerchantId);
            button.MerchantName = EditorGUILayout.TextField("Merchant Name", button.MerchantName);
            button.MerchantUrl = EditorGUILayout.TextField("Merchant Url", button.MerchantUrl);
            button.IsSandbox = EditorGUILayout.Toggle("Sandbox", button.IsSandbox);

            if (GUI.changed) {
                EditorUtility.SetDirty(target);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
