using BenchmarkDotNet.Attributes;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using UploadLargeFile.Database;

namespace UploadLargeFile.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProceedLargeFileController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<ProceedLargeFileController> logger;
        private static object locker = new object();
        public ProceedLargeFileController(AppDbContext context, IServiceProvider serviceProvider, ILogger<ProceedLargeFileController> logger)
        {
            this.context = context;
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        [HttpGet(nameof(ReadCsvFile))]
        public async Task<IActionResult> ReadCsvFile(CancellationToken cancellationToken)
        {
            //var ins = await context.Instruments.ToListAsync(default);

            var watcher = new Stopwatch();
            watcher.Start();
            await ImportCsv(cancellationToken);
            watcher.Stop();
            logger.LogInformation($"Elapsed : {watcher.Elapsed.TotalSeconds}");

            return Ok(nameof(ReadCsvFile));
        }

        [Benchmark]
        private async Task ImportCsv(CancellationToken cancellationToken)
        {
            var filename = @"C:\app\Nguyen Minh Thanh\kafka-docker\instruments.csv";
            using var reader = new StreamReader(System.IO.File.OpenRead(filename));

            string[] props = null;
            //read first row
            if (!reader.EndOfStream)
            {   
                var firstRow = reader.ReadLine();
                props = firstRow.Split(',').ToArray();
            }

            
            //read next
            var numLines = 3_000;
            var chunkSize = 1_000;
            var maxDegreeOfParallelism = 3;
            var storeLines = new List<string>();
            while (!reader.EndOfStream && props != null)
            {
                var rawLine = reader.ReadLine();
                storeLines.Add(rawLine);

                if(storeLines.Count == numLines)
                {
                    var chunks = storeLines.Chunk(chunkSize);

                    //------
                    //foreach (var chunk in chunks) await ProceedChunk(chunk, props, cancellationToken);

                    //------
                    //var tasks = new List<Task>();
                    //foreach (var chunk in chunks) tasks.Add(ProceedChunk(chunk, props, cancellationToken));
                    //await Task.WhenAll(tasks.ToArray());

                    //------
                    //var tasks = new List<Task>();
                    //foreach (var chunk in chunks) tasks.Add(ProceedChunk(chunk, props, cancellationToken));
                    //Task.WaitAll(tasks.ToArray());

                    //------
                    var actions = new List<Action>();
                    //foreach (var chunk in chunks) actions.Add(async () => await ProceedChunk(chunk, props, cancellationToken));
                    foreach (var chunk in chunks) actions.Add(() => ProceedChunk1(chunk, props, cancellationToken));

                    var paralleloptions = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = maxDegreeOfParallelism
                    };

                    Parallel.Invoke(paralleloptions, actions.ToArray());

                    //------
                    //await ParallelTasks(chunks, async (chunk) => await ProceedChunk(chunk, props, cancellationToken), maxDegreeOfParallelism, null); //Cannot handle exception

                    storeLines.Clear();
                }
            }

            await Task.CompletedTask;
        }

        private Task ParallelTasks<T>(
            IEnumerable<T> source, 
            Action<T> body, 
            int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded,
            TaskScheduler taskScheduler = null)
        {
            var option = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            if (taskScheduler != null)
            {
                option.TaskScheduler = taskScheduler;
            }

            var actionBlock = new ActionBlock<T>(body);
            foreach (var chunk in source)
                actionBlock.Post(chunk);
            actionBlock.Complete();

            actionBlock.Completion.Wait();
            return Task.CompletedTask;
        }

        private async Task ProceedChunk(IEnumerable<string> chunk, string[] props, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{DateTime.Now.ToString("F")} - chunk {chunk.Count().ToString()} - Thread {Thread.CurrentThread.ManagedThreadId}");
            var intruments = new List<Instrument>();
            foreach (var line in chunk)
            {
                var lineValues = line.Split(',');
                var instrument = new Instrument();
                for (int i = 0; i < lineValues.Length; i++)
                {
                    var propName = props[i];
                    var val = lineValues[i];
                    var propInfo = typeof(Instrument).GetProperty(propName);
                    if (propInfo != null)
                    {
                        propInfo.SetValue(instrument, ParseObject(val, propInfo.PropertyType, line), null);
                    }

                }
                intruments.Add(instrument);
            }

            var bulkConfig = new BulkConfig
            {
                PreserveInsertOrder = true,
                SetOutputIdentity = true,
                UseTempDB = true,
                SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers
            };

            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetService<AppDbContext>();
            using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await context.BulkInsertAsync(intruments, bulkConfig, cancellationToken: cancellationToken);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError($"{ex.Message} - Thread {Thread.CurrentThread.ManagedThreadId}", ex);
            }
            finally
            {
                transaction.Dispose();
                context.Dispose();
                scope.Dispose();
            }

            intruments.Clear();
            await Task.CompletedTask;
        }

        private Task ProceedChunk1(IEnumerable<string> chunk, string[] props, CancellationToken cancellationToken)
        {
            logger.LogInformation($"{DateTime.Now.ToString("F")} - chunk {chunk.Count().ToString()} - Thread {Thread.CurrentThread.ManagedThreadId}");
            var intruments = new List<Instrument>();
            foreach (var line in chunk)
            {
                var lineValues = line.Split(',');
                var instrument = new Instrument();
                for (int i = 0; i < lineValues.Length; i++)
                {
                    var propName = props[i];
                    var val = lineValues[i];
                    var propInfo = typeof(Instrument).GetProperty(propName);
                    if (propInfo != null)
                    {
                        propInfo.SetValue(instrument, ParseObject(val, propInfo.PropertyType, line), null);
                    }

                }
                intruments.Add(instrument);
            }

            var bulkConfig = new BulkConfig
            {
                PreserveInsertOrder = true,
                SetOutputIdentity = true,
                UseTempDB = true,
                SqlBulkCopyOptions = SqlBulkCopyOptions.CheckConstraints | SqlBulkCopyOptions.FireTriggers
            };

            using var scope = serviceProvider.CreateScope();
            using var context = scope.ServiceProvider.GetService<AppDbContext>();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                context.BulkInsert(intruments, bulkConfig);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                logger.LogError($"{ex.Message} - Thread {Thread.CurrentThread.ManagedThreadId}", ex);
            }
            finally
            {
                transaction.Dispose();
                context.Dispose();
                scope.Dispose();
            }

            intruments.Clear();
            return Task.CompletedTask;
        }

        private object ParseObject(string value, Type type, string line)
        {
            if (string.IsNullOrEmpty(value) || value.Trim().ToLower() == "null") 
                return null;
            try
            {
                return type.Name switch
                {
                    "Int32" => int.Parse(value),
                    "Int64" => long.Parse(value),
                    "Decimal" => decimal.Parse(value),
                    "DateTime" => DateTime.Parse(value),
                    "DateTimeOffset" => DateTimeOffset.Parse(value),
                    "Boolean" => value == "1" ? true : false,
                    "Guid" => Guid.Parse(value),
                    "String" => value,
                    "Nullable`1" => ParseNullableObject(value, type),
                    _ => throw new NotSupportedException()
                };
            }
            catch
            {
                throw new Exception($"{type.Name} not support value {value}");
            }
        }

        private object ParseNullableObject(string value, Type type)
        {
            if (type.FullName.Contains("Int32"))
                return int.Parse(value);
            else if (type.FullName.Contains("Int64"))
                return long.Parse(value);
            else if (type.FullName.Contains("Decimal"))
                return decimal.Parse(value);
            else if (type.FullName.Contains("Boolean"))
                return value == "1" ? true : false;
            else if (type.FullName.Contains("DateTimeOffset"))
                return DateTimeOffset.Parse(value);
            else if (type.FullName.Contains("DateTime"))
                return DateTime.Parse(value);
            else if (type.FullName.Contains("Guid"))
                return Guid.Parse(value);
            else 
                return value;
        }
    }
}
