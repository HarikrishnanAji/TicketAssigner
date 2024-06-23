using Microsoft.EntityFrameworkCore;
using Microsoft.ML;
using System.Data;
using Ticket_Assigner_API.Model;

namespace Ticket_Assigner_API.TicketAssigner_ML
{
    public class PredictionModel
    {
        private readonly MLContext _mlContext;
        private ITransformer _trainedModel;

        public PredictionModel()
        {
            _mlContext = new MLContext(seed: 0);
        }

        public void Train(string datasetPath)
        {
            // Load data
            IDataView dataView = _mlContext.Data.LoadFromTextFile<Ticket>(datasetPath, separatorChar: ',');

            // Data preprocessing and feature engineering
            var dataProcessPipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(Ticket.AssigneeId))
                .Append(_mlContext.Transforms.Concatenate("Features", nameof(Ticket.Priority), nameof(Ticket.Category), nameof(Ticket.TotalEstimate), nameof(Ticket.LoggedEstimate)));

            // Choose a learning algorithm
            var trainer = _mlContext.MulticlassClassification.Trainers.LightGbm();

            // Build and train the model
            var trainingPipeline = dataProcessPipeline.Append(trainer)
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            _trainedModel = trainingPipeline.Fit(dataView);
        }

        public int Predict(Ticket ticket)
        {
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<Ticket, PredictionResult>(_trainedModel);
            var prediction = predictionEngine.Predict(ticket);

            return (int)prediction.PredictedAssigneeId;
        }

        public void SaveModel(string modelPath)
        {
            _mlContext.Model.Save(_trainedModel, null, modelPath);
        }

        public void LoadModel(string modelPath)
        {
            _trainedModel = _mlContext.Model.Load(modelPath, out _);
        }
    }
}
