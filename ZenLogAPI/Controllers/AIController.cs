using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System.Net;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Application.Interfaces;

namespace ZenLogAPI.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly MLContext _mlContext;
        private readonly string _caminhoModelo =
            Path.Combine(Environment.CurrentDirectory, "Treinamento", "ModeloPredicao.zip");
        private readonly ILogEmocionalApplicationService _logEmocionalService;

        public AIController(ILogEmocionalApplicationService logEmocionalService)
        {
            _mlContext = new MLContext();
            _logEmocionalService = logEmocionalService;
        }


        public class LogEmocionalTrainingData
        {
            [LoadColumn(0)]
            public float FezExercicios { get; set; }

            [LoadColumn(1)]
            public float HorasDescanso { get; set; }

            [LoadColumn(2)]
            public float LitrosAgua { get; set; }

            [LoadColumn(3), ColumnName("Label")]
            public float NivelEmocional { get; set; }
        }

        public class LogEmocionalPrediction
        {
            [ColumnName("Score")]
            public float PredictedNivelEmocional { get; set; }
        }

        [HttpGet]
        public async Task<IActionResult> TreinarModelo()
        {
            try
            {
                var result = await _logEmocionalService.ListarTodosAsync();

                if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

                if(!result.Value.Any())
                    return NotFound("Nenhum log emocional encontrado para treinamento.");

                var trainingData = result.Value.Select(log => new LogEmocionalTrainingData
                {
                    FezExercicios = log.FezExercicios ? (float)1 : (float)0,
                    HorasDescanso = log.HorasDescanso,
                    LitrosAgua = log.LitrosAgua,
                    NivelEmocional = (float)log.NivelEmocional
                });

                IDataView dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

                var pipeline = _mlContext.Transforms
                    .Concatenate("Features", new[] { "FezExercicios", "HorasDescanso", "LitrosAgua" })
                    .Append(_mlContext.Regression.Trainers.Sdca());

                var model = pipeline.Fit(dataView);

                _mlContext.Model.Save(model, dataView.Schema, _caminhoModelo);

                return Ok("Modelo treinado e salvo com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Predicao([FromBody] LogEmocionalTrainingData input)
        {
            if (!System.IO.File.Exists(_caminhoModelo))
                return BadRequest("Treine o modelo primeiro.");

            input.NivelEmocional = 0;

            try
            {
                ITransformer loadedModel;
                using (var stream = new FileStream(_caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    loadedModel = _mlContext.Model.Load(stream, out var modelInputSchema);
                }
                var predEngine = _mlContext.Model.CreatePredictionEngine<LogEmocionalTrainingData, LogEmocionalPrediction>(loadedModel);
                var inputData = new LogEmocionalTrainingData
                {
                    FezExercicios = input.FezExercicios,
                    HorasDescanso = input.HorasDescanso,
                    LitrosAgua = input.LitrosAgua
                };
                var prediction = predEngine.Predict(inputData);


                var score = prediction.PredictedNivelEmocional;
                var roundedScore = (int)Math.Round(score);
                input.NivelEmocional = roundedScore;

                return Ok(input);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
