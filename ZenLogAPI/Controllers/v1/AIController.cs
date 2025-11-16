using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using ZenLogAPI.Application.DTOs;
using ZenLogAPI.Application.DTOs.AI;
using ZenLogAPI.Application.Interfaces;
using ZenLogAPI.Utils.Doc;
using ZenLogAPI.Utils.Samples.AI;

namespace ZenLogAPI.Controllers.v1
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
        private readonly ILogger<AIController> _logger;

        public AIController(ILogEmocionalApplicationService logEmocionalService, ILogger<AIController> logger)
        {
            _mlContext = new MLContext();
            _logEmocionalService = logEmocionalService;
            _logger = logger;
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
        [SwaggerOperation(
            Summary = ApiDoc.AITreinarModeloSummary,
            Description = ApiDoc.AITreinarModeloDescription
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Modelo treinado com sucesso", typeof(MessageResponseDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Nenhum log encontrado", typeof(MessageResponseDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno", typeof(MessageResponseDto))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TreinarModeloResponseSample))]
        public async Task<IActionResult> TreinarModelo()
        {
            var traceId = HttpContext.TraceIdentifier;

            try
            {
                var result = await _logEmocionalService.ListarTodosAsync();

                if (!result.IsSuccess) return StatusCode(result.StatusCode, result.Error);

                if(!result.Value.Any())
                    return NotFound("Nenhum log emocional encontrado para treinamento.");

                var trainingData = result.Value.Select(log => new LogEmocionalTrainingData
                {
                    FezExercicios = log.FezExercicios ? 1 : 0,
                    HorasDescanso = log.HorasDescanso,
                    LitrosAgua = log.LitrosAgua,
                    NivelEmocional = (float)log.NivelEmocional
                });

                IDataView dataView = _mlContext.Data.LoadFromEnumerable(trainingData);

                var pipeline = _mlContext.Transforms
                    .Concatenate("Features", new[] { "FezExercicios", "HorasDescanso", "LitrosAgua" })
                    .Append(_mlContext.Regression.Trainers.Sdca());

                var model = pipeline.Fit(dataView);

                string caminhoDiretorio = Path.GetDirectoryName(_caminhoModelo);

                if (!Directory.Exists(caminhoDiretorio))
                {
                    Directory.CreateDirectory(caminhoDiretorio);
                }

                _mlContext.Model.Save(model, dataView.Schema, _caminhoModelo);

                return Ok("Modelo treinado e salvo com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = ApiDoc.AIPredicaoSummary,
            Description = ApiDoc.AIPredicaoDescription
        )]
        [SwaggerRequestExample(typeof(LogEmocionalTrainingData), typeof(PredicaoRequestSample))]
        [SwaggerResponse(StatusCodes.Status200OK, "Predição realizada com sucesso", typeof(LogEmocionalTrainingData))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Modelo não treinado", typeof(MessageResponseDto))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro interno", typeof(MessageResponseDto))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(PredicaoResponseSample))]
        public async Task<IActionResult> Predicao([FromBody] LogEmocionalTrainingData input)
        {
            var traceId = HttpContext.TraceIdentifier;

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
                _logger.LogError(ex, $"[TraceId: {traceId}] Erro inesperado.");

                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
