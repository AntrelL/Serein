using System;

namespace Serein
{
    public struct Contract
    {
        public Contract(
            string text, 
            MessageSeverity severity = MessageSeverity.Error,
            ConsoleOutputConfig outputConfig = default)
        {
            Text = text;
            Severity = severity;
            OutputConfig = outputConfig;
        }

        public string Text { get; private set; }

        public MessageSeverity Severity { get; private set; }

        public ConsoleOutputConfig OutputConfig { get; private set; }

        public bool CheckViolation(bool violationCondition, string prefix = null, string postfix = null)
        {
            return CheckViolation(violationCondition, text => prefix + text + postfix);
        }

        public bool CheckViolation(bool violationCondition, Func<string, string> textProcessor)
        {
            if (violationCondition)
                Console.Write(textProcessor?.Invoke(Text) ?? Text, OutputConfig, Severity);

            return violationCondition;
        }
    }
}
