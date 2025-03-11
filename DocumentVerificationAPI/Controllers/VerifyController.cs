using Dapper;
using DocumentVerificationAPI.Data;
using DocumentVerificationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DocumentVerificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly string _connectionString;
        private readonly AppDbContext _db;
        private readonly ILogger<VerifyController> _logger;

        public VerifyController(AppDbContext db, ILogger<VerifyController> logger)
        {
            _connectionString = db.Database.GetDbConnection().ConnectionString;
            _db = db;
            _logger = logger;
        }

        // POST: api/verify
        [HttpPost]
        public async Task<ActionResult> VerifyDocument([FromBody] VerificationLogDto logDto)
        {
            try
            {
                if (logDto == null)
                {
                    return BadRequest(new { Message = "Verification log data is null." });
                }

                if (string.IsNullOrEmpty(logDto.Status))
                {
                    return BadRequest(new { Message = "The Status field is required." });
                }

                var log = new VerificationLog
                {
                    DocumentId = logDto.DocumentId,
                    VerifiedByUserId = logDto.VerifiedByUserId,
                    Timestamp = logDto.Timestamp,
                    Status = logDto.Status
                };

                long dapperTime, efTime;

                // Dapper Execution
                var stopwatchDapper = Stopwatch.StartNew();
                using (var dbConnection = new SqlConnection(_connectionString))
                {
                    var query = @"
                        INSERT INTO VerificationLogs (DocumentId, VerifiedByUserId, Timestamp, Status)
                        VALUES (@DocumentId, @VerifiedByUserId, @Timestamp, @Status)";

                    await dbConnection.ExecuteAsync(query, log);
                }
                stopwatchDapper.Stop();
                dapperTime = stopwatchDapper.ElapsedMilliseconds;

                // EF Core Execution
                var stopwatchEf = Stopwatch.StartNew();
                _db.VerificationLogs.Add(log);
                await _db.SaveChangesAsync();
                stopwatchEf.Stop();
                efTime = stopwatchEf.ElapsedMilliseconds;

                _logger.LogInformation("Dapper Execution Time: {DapperTime} ms", dapperTime);
                _logger.LogInformation("EF Core Execution Time: {EfCoreTime} ms", efTime);

                return Ok(new
                {
                    Message = "Document verified successfully.",
                    DapperTime = dapperTime,
                    EfCoreTime = efTime
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying document.");

                return StatusCode(500, new { Message = "An error occurred while verifying the document.", Error = ex.Message });
            }
        }
    }
}


