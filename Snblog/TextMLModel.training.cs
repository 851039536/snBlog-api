using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace Snblog
{
    public partial class TextMLModel
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);
            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        private static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Text
                .FeaturizeText(inputColumnName: @"title", outputColumnName: @"title")
                .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName: @"url", outputColumnName: @"url"))
                .Append(mlContext.Transforms.Concatenate(@"Features", new[] { @"title", @"url" }))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: @"describe",
                    inputColumnName: @"describe"))
                .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                    new SdcaMaximumEntropyMulticlassTrainer.Options()
                    {
                        L1Regularization = 1F, L2Regularization = 0.1F, LabelColumnName = @"describe",
                        FeatureColumnName = @"Features"
                    }))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName: @"PredictedLabel",
                    inputColumnName: @"PredictedLabel"));

            return pipeline;
        }
    }
}