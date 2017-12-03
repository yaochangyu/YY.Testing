using System;
using System.Diagnostics.Contracts;
using System.Text;

namespace YY.Testing
{
    public class GreateCommand
    {
        public static string DropTable(params string[] tableNames)
        {
            Contract.Requires(tableNames != null, "tableNames!=null");
            var commandBuilder = new StringBuilder();

            foreach (var tableName in tableNames)
            {
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    continue;
                }

                var dropCommand = $@"
IF OBJECT_ID('dbo.{tableName}', 'U') IS NOT NULL
    DROP TABLE dbo.{tableName};
";

                commandBuilder.AppendLine(dropCommand);
            }

            return commandBuilder.ToString();
        }

        public static string TruncateTable(params string[] tableNames)
        {
            Contract.Requires(tableNames != null, "tableNames!=null");
            var commandBuilder = new StringBuilder();

            foreach (var tableName in tableNames)
            {
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    continue;
                }

                var truncateCommand = $@"
IF OBJECT_ID('dbo.{tableName}', 'U') IS NOT NULL
    TRUNCATE TABLE dbo.{tableName};
";

                commandBuilder.AppendLine(truncateCommand);
            }

            return commandBuilder.ToString();
        }

        public static string DeleteTable(string condation,
                                         string columnName = "Remark",
                                         params string[] tableNames)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(condation), "!string.IsNullOrWhiteSpace(testData)");
            Contract.Requires(!string.IsNullOrWhiteSpace(columnName), "!string.IsNullOrWhiteSpace(columnName)");
            Contract.Requires(tableNames != null, "tableNames!=null");

            var commandBuilder = new StringBuilder();
            foreach (var tableName in tableNames)
            {
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    continue;
                }

                var deleteCommand = $@"
delete from [{tableName}]
    where {columnName} = N'{condation}'
";

                commandBuilder.AppendLine(deleteCommand);
            }

            return commandBuilder.ToString();
        }

        public static Guid Parse(string id)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(id), "!string.IsNullOrWhiteSpace(id)");
            var guidFormat = "{0}-0000-0000-0000-000000000000";
            var guidText = string.Format(guidFormat, id.PadRight(8, '0'));
            var key = Guid.Parse(guidText);
            return key;
        }
    }
}