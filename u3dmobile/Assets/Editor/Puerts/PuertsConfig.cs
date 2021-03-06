//use the menu item "U3DMOBILE/Install Puerts" to install puerts,
//and add "U3DMOBILE_USE_PUERTS" on the project setting "Scripting Define Symbols".
#if U3DMOBILE_USE_PUERTS

using Puerts;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace U3DMobile.Editor
{
    [Configure]
    static class PuertsConfig
    {
        private static readonly List<Type> specialCollection = new List<Type>
        {
            typeof(UnityEngine.Object),

            typeof(GameObject    ),
            typeof(Component     ),
            typeof(Transform     ),
            typeof(Behaviour     ),
            typeof(Animator      ),
            typeof(Camera        ),
            typeof(Renderer      ),
            typeof(MeshRenderer  ),
            typeof(SpriteRenderer),
            
            typeof(Vector3  ),
            typeof(Texture  ),
            typeof(Texture2D),
            typeof(Sprite   ),
            typeof(Mesh     ),
            typeof(Material ),
        };

        private static readonly HashSet<string> namespaceCollection = new HashSet<string>
        {
            "FairyGUI" ,
            "U3DMobile",
        };

        [Binding]
        public static IEnumerable<Type> specialTypes
        {
            get { return specialCollection; }
        }

        [Binding]
        public static IEnumerable<Type> namespaceTypes
        {
            get { return GetNamespaceTypes(); }
        }

        private static IEnumerable<Type> GetNamespaceTypes()
        {
            var types = new List<Type>();

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                Type[] candidateTypes = assembly.GetTypes();
                foreach (Type type in candidateTypes)
                {
                    if (!type.IsPublic)
                    {
                        continue;
                    }
                    if (!namespaceCollection.Contains(type.Namespace))
                    {
                        continue;
                    }

                    types.Add(type);
                }
            }

            return types;
        }
    }
}

#endif
