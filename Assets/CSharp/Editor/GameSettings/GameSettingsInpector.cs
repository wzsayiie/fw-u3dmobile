using U3DMobile;
using UnityEditor;
using UnityEngine;

namespace U3DMobileEditor
{
    [CustomPropertyDrawer(typeof(PackageIdentifier))]
    internal class PackageIdentifierDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect r, SerializedProperty property, GUIContent label)
        {
            var _1 = new Rect(r.x      , r.y, 110          , EditorGUIUtility.singleLineHeight);
            var _2 = new Rect(r.x + 110, r.y, r.width - 110, EditorGUIUtility.singleLineHeight);

            EditorGUI.LabelField(_1, "Package Identifier");
            EditorGUI.PropertyField(_2, property.FindPropertyRelative("iden"), GUIContent.none);
        }
    }

    [CustomPropertyDrawer(typeof(StoreChannel))]
    internal class StoreChannelDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect r, SerializedProperty property, GUIContent label)
        {
            var _1 = new Rect(r.x     , r.y, 20          , EditorGUIUtility.singleLineHeight);
            var _2 = new Rect(r.x + 20, r.y, r.width - 20, EditorGUIUtility.singleLineHeight);

            SerializedProperty channel = property.FindPropertyRelative("channel");

            string cursor = channel.stringValue;
            string active = GameSettingsInspector.instance.activeChannel;
            bool oldIsOn = (
                !string.IsNullOrWhiteSpace(cursor) &&
                !string.IsNullOrWhiteSpace(active) &&
                cursor.Trim() == active.Trim()
            );

            //use bold style to remind users that this is a "radio" toggle.
            bool newIsOn = EditorGUI.Toggle(_1, oldIsOn, GUI.skin.GetStyle("BoldToggle"));
            if (!oldIsOn && newIsOn)
            {
                GameSettingsInspector.instance.activeChannel = cursor;
            }
            EditorGUI.PropertyField(_2, channel, GUIContent.none);
        }
    }

    [CustomPropertyDrawer(typeof(ChannelGateway))]
    internal class ChannelGatewayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect r, SerializedProperty property, GUIContent label)
        {
            var _1 = new Rect(r.x      , r.y, 20           , EditorGUIUtility.singleLineHeight);
            var _2 = new Rect(r.x +  20, r.y, 90           , EditorGUIUtility.singleLineHeight);
            var _3 = new Rect(r.x + 120, r.y, 60           , EditorGUIUtility.singleLineHeight);
            var _4 = new Rect(r.x + 180, r.y, r.width - 180, EditorGUIUtility.singleLineHeight);

            SerializedProperty channel = property.FindPropertyRelative("channel");

            string cursor = channel.stringValue;
            string active = GameSettingsInspector.instance.activeGateway;
            bool oldIsOn = (
                !string.IsNullOrWhiteSpace(cursor) &&
                !string.IsNullOrWhiteSpace(active) &&
                cursor.Trim() == active.Trim()
            );

            //use bold style to remind users that this is a "radio" toggle.
            bool newIsOn = EditorGUI.Toggle(_1, oldIsOn, GUI.skin.GetStyle("BoldToggle"));
            if (!oldIsOn && newIsOn)
            {
                GameSettingsInspector.instance.activeGateway = cursor;
            }
            EditorGUI.PropertyField(_2, channel, GUIContent.none);

            EditorGUI.LabelField(_3, "Gateway");
            EditorGUI.PropertyField(_4, property.FindPropertyRelative("gateway"), GUIContent.none);
        }
    }

    [CustomPropertyDrawer(typeof(ForcedUrlItem))]
    internal class ForcedUrlsDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect r, SerializedProperty property, GUIContent label)
        {
            var _1 = new Rect(r.x     , r.y, 20          , EditorGUIUtility.singleLineHeight);
            var _2 = new Rect(r.x + 20, r.y, 80          , EditorGUIUtility.singleLineHeight);
            var _3 = new Rect(r.x + 90, r.y, r.width - 90, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(_1, property.FindPropertyRelative("enabled"), GUIContent.none);

            switch (property.name)
            {
                case "asset": EditorGUI.LabelField(_2, "Asset URL"); break;
                case "patch": EditorGUI.LabelField(_2, "Patch URL"); break;
            }
            EditorGUI.PropertyField(_3, property.FindPropertyRelative("url"), GUIContent.none);
        }
    }

    [CustomPropertyDrawer(typeof(AssetFlavor))]
    internal class AssetFlavorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect r, SerializedProperty property, GUIContent label)
	    {
            var _1 = new Rect(r.x     , r.y, 20          , EditorGUIUtility.singleLineHeight);
            var _2 = new Rect(r.x + 20, r.y, 40          , EditorGUIUtility.singleLineHeight);
            var _3 = new Rect(r.x + 60, r.y, r.width - 60, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(_1, property.FindPropertyRelative("enabled"), GUIContent.none);

            EditorGUI.LabelField(_2, "Flavor");
            EditorGUI.PropertyField(_3, property.FindPropertyRelative("name"), GUIContent.none);
        }
    }

    [CustomPropertyDrawer(typeof(UserFlag))]
    internal class UserFlagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect r, SerializedProperty property, GUIContent label)
        {
            var _1 = new Rect(r.x      , r.y, 90           , EditorGUIUtility.singleLineHeight);
            var _2 = new Rect(r.x + 100, r.y, 60           , EditorGUIUtility.singleLineHeight);
            var _3 = new Rect(r.x + 170, r.y, 20           , EditorGUIUtility.singleLineHeight);
            var _4 = new Rect(r.x + 170, r.y, r.width - 170, EditorGUIUtility.singleLineHeight);
            var _5 = new Rect(r.x + 170, r.y, r.width - 170, EditorGUIUtility.singleLineHeight);

            EditorGUI.PropertyField(_1, property.FindPropertyRelative("name"), GUIContent.none);

            SerializedProperty type = property.FindPropertyRelative("type");
            EditorGUI.PropertyField(_2, type, GUIContent.none);

            switch ((UserFlagType)type.enumValueFlag)
            {
                case UserFlagType.Bool: {
                    EditorGUI.PropertyField(_3, property.FindPropertyRelative("boolValue"), GUIContent.none);
                    break;
                }
                case UserFlagType.Int: {
                    EditorGUI.PropertyField(_4, property.FindPropertyRelative("intValue"), GUIContent.none);
                    break;
                }
                case UserFlagType.String: {
                    EditorGUI.PropertyField(_5, property.FindPropertyRelative("stringValue"), GUIContent.none);
                    break;
                }
            }
        }
    }

    [CustomEditor(typeof(GameSettings))]
    internal class GameSettingsInspector : Editor
    {
        private SerializedProperty _identifier;
        private SerializedProperty _activeChannel;
        private SerializedProperty _activeGateway;
        private SerializedProperty _channels;
        private SerializedProperty _gateways;
        private SerializedProperty _forcedUrls;
        private SerializedProperty _flavors;
        private SerializedProperty _flags;

        internal static GameSettingsInspector instance;

        internal string activeChannel
        {
            get { return _activeChannel.stringValue ; }
            set { _activeChannel.stringValue = value; }
        }

        internal string activeGateway
        {
            get { return _activeGateway.stringValue ; }
            set { _activeGateway.stringValue = value; }
        }

        private void OnEnable()
        {
            instance = this;

            _identifier    = serializedObject.FindProperty("_identifier");
            _activeChannel = serializedObject.FindProperty("_activeChannel");
            _activeGateway = serializedObject.FindProperty("_activeGateway");
            _channels      = serializedObject.FindProperty("_channels");
            _gateways      = serializedObject.FindProperty("_gateways");
            _forcedUrls    = serializedObject.FindProperty("_forcedUrls");
            _flavors       = serializedObject.FindProperty("_flavors");
            _flags         = serializedObject.FindProperty("_flags");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginDisabledGroup(EditorApplication.isPlayingOrWillChangePlaymode);

            //package identifier.
            EditorGUILayout.PropertyField(_identifier, new GUIContent("Package Identifier"));

            //store channels.
            EditorGUILayout.PropertyField(_channels, new GUIContent("Store Channels"));
            EditorGUILayout.PropertyField(_gateways, new GUIContent("Channel Gateways"));

            //forced urls.
            EditorGUILayout.PropertyField(_forcedUrls, new GUIContent("Forced URLs"));

            //asset flavors.
            EditorGUILayout.PropertyField(_flavors, new GUIContent("Asset Flavors"));
            if (GUILayout.Button("Switch Selected Flavors"))
            {
            }

            //user flags.
            EditorGUILayout.PropertyField(_flags, new GUIContent("User Flags"));

            EditorGUI.EndDisabledGroup();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
