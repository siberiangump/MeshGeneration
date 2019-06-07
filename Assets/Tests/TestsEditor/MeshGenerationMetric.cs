using NUnit.Framework;
using System.Collections;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class MeshGenerationMetric : IPrebuildSetup 
    {
        private static MeshMetricSettings MeshMetricSettings;

        private static Vector3[] Verteces;

        public void Setup()
        {
            MeshMetricSettings = AssetDatabase.LoadAssetAtPath<MeshMetricSettings>("Assets/Tests/TestSettings/MeshMetricSettings.asset");
            Verteces = MeshMetricSettings.GetVertexCalculator().GetVerteces(MeshMetricSettings.GetMeshSettings());
        }

        [UnityTest]
        public IEnumerator TestSync()
        {
            IHieghtCalculator calculator = MeshMetricSettings.GetSyncCalculator();
            yield return TestCalculatorAsync(calculator);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestTask()
        {
            IHieghtCalculator calculator = MeshMetricSettings.GetTaskCalculator();
            yield return TestCalculatorAsync(calculator);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestJob()
        {
            IHieghtCalculator calculator = MeshMetricSettings.GetJobCalculator();
            calculator.GetVerteces(Verteces, Vector3.zero);
            //yield return TestCalculatorAsync(calculator);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestJobBurst()
        {
            IHieghtCalculator calculator = MeshMetricSettings.GetJobBurstCalculator();
            calculator.GetVerteces(Verteces, Vector3.zero);
            //yield return TestCalculatorAsync(calculator);
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestCompute()
        {
            IHieghtCalculator calculator = MeshMetricSettings.GetComputeCalculator();
            calculator.GetVerteces(Verteces, Vector3.zero);
            //yield return TestCalculatorAsync(calculator);
            yield return null;
        }

        private IEnumerator TestCalculatorAsync(IHieghtCalculator calculator)
        {
            (calculator as IAllocatable)?.Allocate(Verteces, Vector3.zero);
            Task task = Task.Run(() => calculator.GetVerteces(Verteces, Vector3.zero));
            while (!task.IsCompleted)
                yield return null;
            (calculator as IAllocatable)?.Dispose();
            Debug.Log("end");
        }
    }
}
