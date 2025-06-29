using System.Data;
using System.Net;
using System.Text.Json;
using Application.Core.Exceptions;

namespace Business_Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción no controlada:{Message}",ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode;
            object response;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new {
                        title = "Error de Validación",
                        status = (int)statusCode,
                        errors = validationException.Errors
                    };
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response = new
                    {
                        title = "Elemento no encontrado",
                        status = (int)statusCode,
                        errors = notFoundException.Message
                    };
                    break;
                case Infrastructure.Core.InfrastructureExceptions.ConflictException conflictException:
                    statusCode = HttpStatusCode.Conflict;
                    response = new { 
                        title = "Conflicto con la Base de Datos",
                        status = (int)statusCode,
                        errors = conflictException.Message
                    };
                    break;
                case Application.Core.Exceptions.ConflictException conflictException:
                    statusCode = HttpStatusCode.Conflict;
                    response = new
                    {
                        title = "Conflicto con la Base de Datos",
                        status = (int)statusCode,
                        errors = conflictException.Message
                    };
                    break;
                case DataException dataException:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = new { 
                        title = "Error en la Base de Datos",
                        status = (int)statusCode,
                        errors = dataException.Message
                    };
                    break;
                case OperationCanceledException operationCanceledException:
                    statusCode = HttpStatusCode.Forbidden;
                    response = new
                    {
                        title = "Transacción en la base de datos cancelada",
                        status = (int)statusCode,
                        errors = operationCanceledException
                    };
                    break;

                case ArgumentNullException argumentNullException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new
                    {
                        title = "Argumentos nulos al intentar crear una entidad",
                        status = (int)statusCode,
                        errors = argumentNullException.Message
                    };
                    break;

                case ArgumentException argumentException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new
                    {
                        title = "Argumentos invalidos al intentar crear una entidad",
                        status = (int)statusCode,
                        errors = argumentException.Message
                    };
                    break;
                case InvalidOperationException invalidOperationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new
                    {
                        title = "Operaciones invalidas",
                        status = (int)statusCode,
                        errors = invalidOperationException.Message
                    };
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = new
                    {
                        title = "Error Interno del Servidor",
                        status = (int)statusCode,
                        detail = "Ocurrió un error inesperado. Por favor, intente de nuevo más tarde."
                    };
                    break;

            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        }
    }
}
