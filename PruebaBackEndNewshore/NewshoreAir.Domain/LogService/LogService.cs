using NLog;
using System.Collections.Generic;
using System.Text;

namespace NewshoreAir.Domain.LogService
{
    public class LogService : ILogService
    {
        #region Private Fields
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private IList<KeyValuePair<LogKey, string>> Values { get; set; } =
         new List<KeyValuePair<LogKey, string>>();
        #endregion

        #region Methods
        public void Add(LogKey logKey, string msg)
        {
            Values.Add(new KeyValuePair<LogKey, string>(logKey, msg));
        }

        public void Generate()
        {
            var log = new StringBuilder();
            log.AppendLine();
            var isError = false;
            foreach (var item in Values)
            {
                var text = item.Value;
                switch (item.Key)
                {
                    case LogKey.Begin:
                        log.AppendLine(
                            $"========================================= BEGIN {text} =========================================");
                        break;
                    case LogKey.End:
                        log.AppendLine(
                            $"========================================= END {text} =========================================");
                        break;
                    case LogKey.Error:
                        isError = true;
                        log.AppendLine($"{item.Key}: {text}");
                        break;
                    default:
                        log.AppendLine($"{item.Key}: {text}");
                        break;
                }
            }
            if (!isError)
                _logger.Trace(log);
            else
                _logger.Error(log);

            Values = new List<KeyValuePair<LogKey, string>>();
        }
        #endregion
    }
}
