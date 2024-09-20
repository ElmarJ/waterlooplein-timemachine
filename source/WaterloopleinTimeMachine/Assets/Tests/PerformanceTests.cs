
using System.Collections;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests
{
    public class PerformanceTests
    {
        [UnityTest, Performance]
        public IEnumerator BasicPerformanceTest()
        {
            // load scene
            using (Measure.Scope("Scene Loading Time"))
            {
                SceneManager.LoadScene("Assets/Scenes/MainScene.unity");
            }

            for (int i = 0; i < QualitySettings.names.GetLength(0); i++)
            {
                SceneManager.LoadScene("Assets/Scenes/MainScene.unity");
                QualitySettings.SetQualityLevel(i, true);

                Measure
                    .Frames()
                    .WarmupCount(10)
                    .MeasurementCount(100)
                    .SampleGroup("Frame execution time on quality:" + QualitySettings.names[i])
                    .Run();
            }

            // Measure the execution time of all frames
            string[] markers =
            {
                "Instantiate",
                "Instantiate.Copy",
                "Instantiate.Produce",
                "Instantiate.Awake"
            };

            using (Measure.ProfilerMarkers(markers))
            {
                for (var i = 0; i < 60; i++)
                {
                    yield return null;
                }
            }

            // measure memory
            var allocated = new SampleGroup("TotalAllocatedMemory", SampleUnit.Megabyte);
            var reserved = new SampleGroup("TotalReservedMemory", SampleUnit.Megabyte);
            for (var i = 0; i < 60; i++)
            {
                Measure.Custom(allocated, Profiler.GetTotalAllocatedMemoryLong() / 1048576f);
                Measure.Custom(reserved, Profiler.GetTotalReservedMemoryLong() / 1048576f);
                yield return null;
            }

            yield return null;
        }
    }
}
