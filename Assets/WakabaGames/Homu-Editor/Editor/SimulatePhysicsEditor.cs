using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace WakabaGames.Editor
{
    public class SimulatePhysicsEditor : EditorWindow
    {
        // Properties
        int m_maxIterations = 1000;
        bool m_generateRigidbodies = false;
        //Vector2 m_randomAngle = Vector2.zero;
        //Vector2 m_randomForce = Vector2.zero;
        
        List<SimulatedBody> m_simulatedBodies = new List<SimulatedBody>();
        //Transform[] m_selectedObjectstemp;
        List<SelectedObject> m_selectedObjs = new List<SelectedObject>();


        #region GUI
        [MenuItem("Tools/Simulate Physics", priority = 1080)]
        public static void ShowWindow()
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(SimulatePhysicsEditor));
            window.titleContent.text = "Simulate Physics";
            window.minSize = new Vector2(200, 85);
            window.maxSize = new Vector2(200, 85);
        }

        void OnGUI()
        {
            m_maxIterations = EditorGUILayout.IntField(new GUIContent("Max Iterations", "Maximum number of ticks the simulation will run for. Default 1000."), m_maxIterations);
            m_generateRigidbodies = EditorGUILayout.Toggle(new GUIContent("Generate Rigidbodies", "Automatically add and remove Rigidbodies to selected objects that don't already have one."), m_generateRigidbodies);
            //m_randomForce = EditorGUILayout.Vector2Field("Add Force", m_randomForce);
            //m_randomAngle = EditorGUILayout.Vector2Field("Force Angle", m_randomAngle);
            
            if (GUILayout.Button("Simulate"))
            {
                RunSimulation();
            }
            if ( GUILayout.Button("Reset") )
            {
                ResetAllBodies();
            }
        }
        #endregion
        
        void RunSimulation()
        {
            Debug.Log($"Running simulations. Interations: {m_maxIterations}. Generate rigidbodies: {m_generateRigidbodies}");
            m_simulatedBodies.Clear();
            m_selectedObjs.Clear();

            // Keep a reference to all the gameobjects we have selected
            Undo.RecordObjects(Selection.transforms, "Simulate physics");
            for ( int i = 0; i < Selection.transforms.Length; i++ )                
            {
                // If the selected object already has a rigidbody, just add it to the list
                if ( Selection.transforms[i].GetComponent<Rigidbody>() )
                {
                    m_selectedObjs.Add(new SelectedObject(Selection.transforms[i], false));
                }
                // Else if auto-generate is enabled then add the component before adding to list
                else if (m_generateRigidbodies)
                {
                    // Auto-generate rigidbodies if enabled
                    Selection.transforms[i].gameObject.AddComponent<Rigidbody>();
                    m_selectedObjs.Add(new SelectedObject(Selection.transforms[i], true));
                }
                // Otherwise ignore it, it won't be simulated by the physics anyway
                else
                {
                    continue;
                }
            }

            // Keep a reference to all the objects we are about to simulate            
            Rigidbody[] rb = FindObjectsOfType<Rigidbody>();
            for ( int i = 0; i < rb.Length; i++ )
            {
                m_simulatedBodies.Add(new SimulatedBody(rb[i]));
            }

            // Simulate
            Physics.autoSimulation = false;
            for ( int i = 0; i < m_maxIterations; i++ )
            {
                Physics.Simulate(Time.fixedDeltaTime);
                // Stop simulating early if all the objects have stopped moving
                // Ignore the objects that weren't selected
                if ( m_simulatedBodies.All(body => body.rigidbody.IsSleeping()) )
                {
                    Debug.Log($"Physics Simulation ended early at iteration {i}");
                    break;
                }
            }
            Physics.autoSimulation = true;            

            // Reset all the objects that were not selected
            for (int i = 0; i < m_simulatedBodies.Count; i++ )
            {
                if (!IsSelected(m_simulatedBodies[i]))
                {
                    m_simulatedBodies[i].Reset();
                }
            }

            // Remove any generated rigidbodies
            if (m_generateRigidbodies)
            {
                for (int i = 0; i < m_selectedObjs.Count; i++ )
                {
                    if (m_selectedObjs[i].generatedRB)
                    {
                        DestroyImmediate(m_selectedObjs[i].transform.GetComponent<Rigidbody>());
                    }
                }
            }
        }
        
        // Reset all the selected objects that was previously simulated
        public void ResetAllBodies()
        {
            Debug.Log("Reset");
            
            if ( m_simulatedBodies != null )
            {
                for (int i = 0; i < m_simulatedBodies.Count; i++ )
                {
                    if ( IsSelected(m_simulatedBodies[i]) )
                    {
                        Undo.RecordObject(m_simulatedBodies[i].transform, "Reset simulated physics objects");
                        m_simulatedBodies[i].Reset();
                    }
                }
            }
        }

        // Is the given simulatedbody one of the scene selected gameobjects
        bool IsSelected(SimulatedBody sb)
        {
            return m_selectedObjs.Any(selected => selected.transform.GetInstanceID() == sb.ID);
        }

        struct SelectedObject
        {
            public readonly Transform transform;
            public bool generatedRB;

            public SelectedObject (Transform transform, bool generateRB)
            {
                this.transform = transform;
                this.generatedRB = generateRB;
            }
        }

        struct SimulatedBody
        {
            public readonly Rigidbody rigidbody;
            readonly Vector3 origPostion;
            readonly Quaternion origRotation;
            public readonly Transform transform;
            public readonly int ID;

            public SimulatedBody( Rigidbody rigidbody )
            {
                this.rigidbody = rigidbody;
                transform = rigidbody.transform;
                origPostion = rigidbody.position;
                origRotation = rigidbody.rotation;
                ID = transform.GetInstanceID();
            }

            public void Reset()
            {
                transform.position = origPostion;
                transform.rotation = origRotation;
                if ( rigidbody != null )
                {
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero;
                }
            }
        }
    }
}
