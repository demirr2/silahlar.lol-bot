using System;
using System.Net.Http;
using System.Threading.Tasks;

// dioxtra orospu evladıdr <3333
class Program
{
    static string GenerateRandomIp()
    {
        Random random = new Random();
        return $"{random.Next(1, 256)}.{random.Next(1, 256)}.{random.Next(1, 256)}.{random.Next(1, 256)}";
    }

    static async Task SendRequest(string ip, string url)
    {
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("X-Forwarded-For", ip);
            client.Timeout = TimeSpan.FromSeconds(5);

            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://silahlar.lol/{url}");
                Console.WriteLine($"IP {ip}: durum: {response.StatusCode}");
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine($"IP {ip}: basarisiz! tekrar deneniyor...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"IP {ip} icin hata: {ex.Message}");
            }
        }
    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("silahlar.lol kullanici adinizi girin:         |        bir hatayla karsilasirsaniz discord: tuccarironnn");
    }

    static async Task Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            string userInput = Console.ReadLine();

            Console.WriteLine("gonderiliyor...");

           
            var tasks = new Task[20];

            for (int i = 0; i < 20; i++)
            {
                string randomIp = GenerateRandomIp();
                tasks[i] = SendRequest(randomIp, userInput);
            }

            // Tüm görevlerin tamamlanmasını bekle
            await Task.WhenAll(tasks);

            Console.WriteLine("tamamlandi");
            Console.WriteLine("anamenuye donmek istermisiniz? (evet/hayır): ");
            string response = Console.ReadLine()?.ToLower();

            if (response != "evet")
                break;
        }

        Console.WriteLine("cikiliyor...");
    }
}
