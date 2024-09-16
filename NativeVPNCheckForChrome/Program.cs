using System;
using System.IO;
using System.Text.Json;

class Program
{
    private static string logFilePath = "log.txt";

    static void Main(string[] args)
    {
        Log("Native application started.");

        try
        {
            using (var input = Console.OpenStandardInput())
            using (var output = Console.OpenStandardOutput())
            using (var reader = new BinaryReader(input))
            using (var writer = new BinaryWriter(output))
            {
                // Читаємо перші 4 байти для визначення довжини повідомлення
                byte[] lengthBytes = reader.ReadBytes(4);
                if (lengthBytes.Length == 0)
                {
                    Log("No input received. Exiting.");
                    return;
                }

                int messageLength = BitConverter.ToInt32(lengthBytes, 0);

                // Читаємо повідомлення зазначеної довжини
                byte[] messageBytes = reader.ReadBytes(messageLength);
                string message = System.Text.Encoding.UTF8.GetString(messageBytes);

                // Логуємо отримані дані
                Log($"Received input: {message}");

                // Готуємо відповідь
                var response = new
                {
                    vpnStatus = false  // Наприклад, VPN вимкнено
                };

                string jsonResponse = JsonSerializer.Serialize(response);
                byte[] jsonResponseBytes = System.Text.Encoding.UTF8.GetBytes(jsonResponse);

                // Логуємо відповідь
                Log($"Sending response: {jsonResponse}");

                // Відправляємо довжину відповіді (4 байти)
                writer.Write(jsonResponseBytes.Length);
                writer.Flush();

                // Відправляємо саму відповідь
                writer.Write(jsonResponseBytes);
                writer.Flush();
            }
        }
        catch (Exception ex)
        {
            Log($"Error: {ex.Message}");
        }

        Log("Native application finished.");
    }

    static void Log(string message)
    {
        try
        {
            File.AppendAllText(logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing to log file: {ex.Message}");
        }
    }
}