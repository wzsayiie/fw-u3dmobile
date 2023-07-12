using UnityEditor;
using UnityEngine;

namespace U3DMobileEditor
{
    [CustomPropertyDrawer(typeof(GameLanguage))]
    internal class GameLanguageDrawer : ListItemDrawer
    {
        protected override void OnDrawFirstLine(SerializedProperty property)
        {
            Label( 60, "Language");
            Field(flx, property, "language");
        }
    }

    [CustomPropertyDrawer(typeof(StoreChannel))]
    internal class StoreChannelDrawer : ListItemDrawer
    {
        protected override void OnDrawFirstLine(SerializedProperty property)
        {
            Label( 60, "Channel");
            Field(flx, property, "channel");
        }
    }

    [CustomPropertyDrawer(typeof(ChannelGateway))]
    internal class ChannelGatewayDrawer : ListItemDrawer
    {
        protected override void OnDrawFirstLine(SerializedProperty property)
        {
            Field( 90, property, "channel");
            Label( 30, "URL");
            Field(flx, property, "gateway");
        }
    }

    [CustomPropertyDrawer(typeof(ForcedURL))]
    internal class ForcedURLDrawer : ListItemDrawer
    {
        protected override void OnDrawFirstLine(SerializedProperty property)
        {
            Field( 90, property, "name");
            Label( 30, "URL");
            Field(flx, property, "url");
        }
    }

    [CustomPropertyDrawer(typeof(AssetFlavor))]
    internal class AssetFlavorDrawer : ListItemDrawer
    {
        protected override void OnDrawFirstLine(SerializedProperty property)
        {
            Label( 60, "Flavor");
            Field(flx, property, "flavor");
        }
    }

    [CustomEditor(typeof(GameOptions))]
    internal class GameOptionsInspector : Editor
    {
        private SerializedProperty _gameLanguages  ;
        private SerializedProperty _storeChannels  ;
        private SerializedProperty _channelGateways;
        private SerializedProperty _forcedAssetURLs;
        private SerializedProperty _forcedPatchURLs;
        private SerializedProperty _assetFlavors   ;

        private void OnEnable()
        {
            _gameLanguages   = serializedObject.FindProperty("_gameLanguages"  );
            _storeChannels   = serializedObject.FindProperty("_storeChannels"  );
            _channelGateways = serializedObject.FindProperty("_channelGateways");
            _forcedAssetURLs = serializedObject.FindProperty("_forcedAssetURLs");
            _forcedPatchURLs = serializedObject.FindProperty("_forcedPatchURLs");
            _assetFlavors    = serializedObject.FindProperty("_assetFlavors"   );
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_gameLanguages  , new GUIContent("Game Languages"  ));
            EditorGUILayout.PropertyField(_storeChannels  , new GUIContent("Store Channels"  ));
            EditorGUILayout.PropertyField(_channelGateways, new GUIContent("Channel Gateways"));
            EditorGUILayout.PropertyField(_forcedAssetURLs, new GUIContent("Asset URLs"      ));
            EditorGUILayout.PropertyField(_forcedPatchURLs, new GUIContent("Patch URLs"      ));
            EditorGUILayout.PropertyField(_assetFlavors   , new GUIContent("Asset Flavors"   ));

            serializedObject.ApplyModifiedProperties();
        }
    }
}