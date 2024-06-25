﻿namespace YoloDotNet.Benchmarks.PoseEstimationTests
{
    using System.Collections.Generic;

    using SixLabors.ImageSharp;
    using BenchmarkDotNet.Attributes;
    using SixLabors.ImageSharp.PixelFormats;

    using YoloDotNet.Enums;
    using YoloDotNet.Models;
    using YoloDotNet.Benchmarks;
    using YoloDotNet.Test.Common.Enums;

    [MemoryDiagnoser]
    public class SimplePostEstimationTests
    {
        #region Fields

        private static string model = SharedConfig.GetTestModel(modelType: ModelType.PoseEstimation);
        private static string testImage = SharedConfig.GetTestImage(imageType: ImageType.Crosswalk);

        private Yolo cudaYolo;
        private Yolo cpuYolo;
        private Image image;

        #endregion Fields

        #region Methods

        [GlobalSetup]
        public void GlobalSetup()
        {
            cudaYolo = new Yolo(onnxModel: model, cuda: true);
            cpuYolo = new Yolo(onnxModel: model, cuda: false);
            image = Image.Load<Rgba32>(path: testImage);
        }

        [Benchmark]
        public List<PoseEstimation> RunSimplePoseEstimationGpu()
        {
            return cudaYolo.RunPoseEstimation(img: image, confidence: 0.25, iou: 0.45);
        }

        [Benchmark]
        public List<PoseEstimation> RunSimplePoseEstimationCpu()
        {
            return cpuYolo.RunPoseEstimation(img: image, confidence: 0.25, iou: 0.45);
        }

        #endregion Methods
    }
}
