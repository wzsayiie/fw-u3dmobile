using UnityEditor;
using UnityEngine;

namespace U3DMobileEditor
{
    [CustomPropertyDrawer(typeof(BundleSerial))]
    internal class BundleSerialDrawer : BaseItemDrawer
    {
        protected override void OnDrawLine(int _, SerializedProperty property)
        {
            Label(110, "Bundle Serial");
            Field(flx, property.FindPropertyRelative("serial"));
        }
    }

    [CustomPropertyDrawer(typeof(CarryOption))]
    internal class CarryOptionDrawer : ListItemDrawer
    {
        protected override void OnDrawLine(int _, SerializedProperty property)
        {
            var    option  = property.FindPropertyRelative("option");
            string curItem = option.stringValue;
            string actItem = BuildSettingsInspector.instance.activeCarry;

            string curTrim = curItem?.Trim();
            string actTrim = actItem?.Trim();
            bool   beingOn = curTrim != null && curTrim == actTrim;
            bool   afterOn ;

            Radio( 20, beingOn, out afterOn);
            Field(flx, option);

            if (!beingOn && afterOn)
            {
                BuildSettingsInspector.instance.activeCarry = curTrim;
            }
        }
    }

    [CustomPropertyDrawer(typeof(BundleEntry))]
    internal class BundleEntryDrawer : ListItemDrawer
    {
        protected override int OnGetLines()
        {
            return 3;
        }

        protected override void OnDrawLine(int line, SerializedProperty property)
        {
            switch (line)
            {
                case 0: DrawFileEntry(property); break;
                case 1: DrawCarryOpts(property); break;
                case 2: DrawFilePath (property); break;
            }
        }

        private void DrawFileEntry(SerializedProperty property)
        {
            Field( 20, property, "selected"  );
            Field(120, property, "fileObj"   );
            Field( 70, property, "packMode"  );
            Field(flx, property, "demandMode");
        }

        private void DrawCarryOpts(SerializedProperty property)
        {
            Label( 20, "");
            Field(flx, property, "carryOpts");
        }

        private void DrawFilePath(SerializedProperty property)
        {
            SerializedProperty fileObj = property.FindPropertyRelative("fileObj");
            UnityEngine.Object objRef  = fileObj.objectReferenceValue;

            if (objRef != null)
            {
                Label( 20, "");
                Label(flx, AssetDatabase.GetAssetPath(objRef));
            }
        }
    }

    [CustomPropertyDrawer(typeof(PatchEntry))]
    internal class PatchEntryDrawer : ListItemDrawer
    {
        protected override int OnGetLines()
        {
            return 2;
        }

        protected override void OnDrawLine(int line, SerializedProperty property)
        {
            switch (line)
            {
                case 0: DrawFileObj (property); break;
                case 1: DrawFilePath(property); break;
            }
        }

        private void DrawFileObj(SerializedProperty property)
        {
            Field( 20, property, "selected");
            Field(flx, property, "fileObj" );
        }

        private void DrawFilePath(SerializedProperty property)
        {
            SerializedProperty fileObj = property.FindPropertyRelative("fileObj");
            UnityEngine.Object objRef  = fileObj.objectReferenceValue;

            if (objRef != null)
            {
                Label(flx, AssetDatabase.GetAssetPath(objRef));
            }
        }
    }

    [CustomEditor(typeof(BuildSettings))]
    internal class BuildSettingsInspector : Editor
    {
        private SerializedProperty _bundleSerial ;
        private SerializedProperty _activeCarry  ;
        private SerializedProperty _carryOptions ;
        private SerializedProperty _bundleEntries;
        private SerializedProperty _bundlePatches;

        internal static BuildSettingsInspector instance;

        internal string activeCarry
        {
            get { return _activeCarry.stringValue ; }
            set { _activeCarry.stringValue = value; }
        }

        private void OnEnable()
        {
            instance = this;

            _bundleSerial  = serializedObject.FindProperty("_bundleSerial" );
            _activeCarry   = serializedObject.FindProperty("_activeCarry"  );
            _carryOptions  = serializedObject.FindProperty("_carryOptions" );
            _bundleEntries = serializedObject.FindProperty("_bundleEntries");
            _bundlePatches = serializedObject.FindProperty("_bundlePatches");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_bundleSerial, new GUIContent("Bundle Serial"));
            EditorGUILayout.PropertyField(_carryOptions, new GUIContent("Package Carry Options"));

            EditorGUILayout.PropertyField(_bundleEntries, new GUIContent("Bundle Entries"));
            if (GUILayout.Button("Pack Selected (for Android)"))
            {
            }
            if (GUILayout.Button("Pack Selected (for iOS)"))
            {
            }

            EditorGUILayout.PropertyField(_bundlePatches, new GUIContent("Patch Entries"));
            if (GUILayout.Button("Copy Selected Patches"))
            {
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
