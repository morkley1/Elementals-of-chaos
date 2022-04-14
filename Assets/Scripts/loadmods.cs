using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.Reflection;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class loadmods : MonoBehaviour
{
    void Start()
    {
        Debug.Log(Application.dataPath.Replace("/Assets", "/Modstufftoload").Replace("/Elements of chaos_Data", "/Modstufftoload") + @"/UnityEngine.dll");
        //Debug.Log(Directory.GetFiles(Application.dataPath.Replace("/Assets", "/Modstufftoload").Replace("/Elements of chaos_Data", "/Modstufftoload") + @"/UnityEngine.dll"));
        var assembly = Compile(@"
        using UnityEngine;

        public class RuntimeCompiled : MonoBehaviour
        {
            public static RuntimeCompiled AddYourselfTo(GameObject host)
            {
                return host.AddComponent<RuntimeCompiled>();
            }

            void Start()
            {
                Debug.Log(""The runtime compiled component was successfully attached to "" + gameObject.name);
            }
        }");

        var runtimeType = assembly.GetType("RuntimeCompiled");
        var method = runtimeType.GetMethod("AddYourselfTo");
        var del = (Func<GameObject, MonoBehaviour>)
                      Delegate.CreateDelegate(
                          typeof(Func<GameObject, MonoBehaviour>),
                          method
                  );

        del(gameObject);

        // We ask the compiled method to add its component to this.gameObject
        var addedComponent = del.Invoke(gameObject);

        // The delegate pre-bakes the reflection, so repeated calls don't
        // cost us every time, as long as we keep re-using the delegate.
    }

    public static Assembly Compile(string source)
    {
        // Replace this Compiler.CSharpCodeProvider wth aeroson's version
        // if you're targeting non-Windows platforms:
        var provider = new CSharpCodeProvider();
        var param = new CompilerParameters();

        /*
        string[] files = Directory.GetFiles(Application.dataPath + @"\Modstufftoload", "*.dll");
        foreach (var assembly in files)
        {
            Debug.Log(assembly);
            //Assembly.LoadFile();
        }//*/

        // Add ALL of the assembly references
        /*foreach (var assembly in Assembly.LoadFile)
        {
            Debug.Log(assembly);
            if (!(assembly.IsDynamic))
            {
                try
                {
                    param.ReferencedAssemblies.Add(assembly.Location);
                }
                catch (Exception ex)
                {
                    Debug.Log(ex);
                }
            }
        }*/

        // Or, uncomment just the assemblies you need...

        // System namespace for common types like collections.
        //param.ReferencedAssemblies.Add("System.dll");

        // This contains methods from the Unity namespaces:
        param.ReferencedAssemblies.Add(Application.dataPath.Replace("/Assets", "/Modstufftoload").Replace("/Elements of chaos_Data", "/Modstufftoload") + @"/UnityEngine.dll"/*@"C:\Program Files\Unity\Hub\Editor\2021.1.12f1\Editor\Data\Managed\UnityEngine.dll"*/);

        // This assembly contains runtime C# code from your Assets folders:
        // (If you're using editor scripts, they may be in another assembly)
        //param.ReferencedAssemblies.Add("CSharp.dll");


        // Generate a dll in memory
        param.GenerateExecutable = false;
        param.GenerateInMemory = true;

        // Compile the source
        var result = provider.CompileAssemblyFromSource(param, source);

        if (result.Errors.Count > 0)
        {
            var msg = new StringBuilder();
            foreach (CompilerError error in result.Errors)
            {
                msg.AppendFormat("Error ({0}): {1}\n",
                    error.ErrorNumber, error.ErrorText);
            }
            throw new Exception(msg.ToString());
        }

        // Return the assembly
        return result.CompiledAssembly;
    }
    /*void LogCallback(string condition, string stackTrace, LogType type)
    {
        
    }*/
}